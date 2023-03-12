using System.Collections;
using System.Collections.Generic;
using AudioScripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using AudioType = AudioScripts.AudioType;

public class Ball : MonoBehaviour {

	[Header("Ball Config")]
	[SerializeField, Tooltip("Bounciness")] float _bounciness = 15f;
	[SerializeField, Tooltip("Boosted Bounciness")] float _boostedBounciness = 22f;
	[SerializeField, Tooltip("Max Velocity")] float _maxVelocity = 22f;
	[SerializeField, Tooltip("Max Angular Velocity")] float _maxAngularVelocity = 20f;
	[SerializeField] PlayerPaddle _playerPaddle;
	[SerializeField] Material _trailMaterial;
	[SerializeField] Material _trailBoostMaterial;
	[SerializeField] AudioController _audioController;

	private Rigidbody _rigidbody;
	private TrailRenderer _trail;
	private float _timer = 0f;

	private void Awake() {
		_rigidbody = GetComponent<Rigidbody>();
		_rigidbody.maxAngularVelocity= _maxAngularVelocity;
		_trail = GetComponent<TrailRenderer>();
	}

	private void FixedUpdate() {

		if (_rigidbody.velocity.magnitude >= _maxVelocity) {
			_rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _maxVelocity);
		}

	}

	private void OnCollisionStay(Collision collision) {
		
		_timer += Time.deltaTime;
		if (_timer > 0.33f && collision.gameObject.TryGetComponent(out Block block)) {
			_timer = 0;
			block.OnHitBlock();
		}

	}

	private void OnCollisionEnter(Collision collision) {

		if (collision.gameObject.TryGetComponent(out DeadZone deadZone)) {
			BallFall();
		}

		if (collision.gameObject.TryGetComponent(out Block block)) {
			_audioController.Instance.PlayAudio(AudioType.SFX_HIT_BOX);
			block.OnHitBlock();
		}

		if (collision.gameObject.TryGetComponent(out Bomb bomb)) {
			_audioController.Instance.PlayAudio(AudioType.SFX_HIT_BOMB);

			Explode(1, transform.position - bomb.gameObject.transform.position);
			bomb.OnHitBlock();
		}

		if (collision.gameObject.TryGetComponent<ObjectScripts.BoxesBehavior>(out var boxesBehavior))
		{
			_audioController.Instance.PlayAudio(AudioType.SFX_HIT_BOX);
			boxesBehavior.OnHitBox();
		}

	}




	public void Bounce() {
		_audioController.Instance.PlayAudio(AudioType.SFX_HIT_PADDLE);
		_rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _bounciness, _rigidbody.velocity.z);
		
		_trail.time = 0.15f;
	}

	public void BoostedBounce() {
		_audioController.Instance.PlayAudio(AudioType.SFX_HIT_BOOST);
		_rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _boostedBounciness, _rigidbody.velocity.z);
		transform.localScale *= 0.9f;
		Invoke(nameof(ResetSize), 0.05f);
		_trail.time = 0.3f;
	}

	public void ResetSize() {
		transform.localScale = Vector3.one;
	}

	public void Explode(int damage, Vector3 position) {

		_rigidbody.AddForceAtPosition(position * 200f * damage, transform.position);

	}

	public void PickUpItem() {
		_audioController.Instance.PlayAudio(AudioType.SFX_COLLECT_EGG);
	}

	public void BallFall() {

		_rigidbody.position = new Vector3(_rigidbody.position.x, transform.localScale.y / 2, _rigidbody.position.z);
		_rigidbody.isKinematic = true;
		//_rigidbody.useGravity = false;
		_playerPaddle.StopCamera();
		_rigidbody.gameObject.layer = LayerMask.NameToLayer("Camera");

		Invoke(nameof(ReturnBall), 1.15f);
	}


	public void ReturnBall() {

		_trail.time = 0.15f;
		_rigidbody.isKinematic = false;
		//_rigidbody.useGravity = true;
		_playerPaddle.PlayCamera();
		_rigidbody.AddForce(Vector3.up * 13f + Vector3.right * 4f * Mathf.Clamp(_playerPaddle.transform.position.x - transform.position.x, -1, 1), ForceMode.VelocityChange);
		_rigidbody.AddRelativeTorque((-Vector3.up + Vector3.right) * 100f);
		Invoke(nameof(ChangeBallLayerBack), 0.15f);

	}

	private void ChangeBallLayerBack() {
		_rigidbody.gameObject.layer = LayerMask.NameToLayer("Ball");
	}


}
