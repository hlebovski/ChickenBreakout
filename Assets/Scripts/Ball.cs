using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Ball : MonoBehaviour {

	[Header("Ball Config")]
	[SerializeField, Tooltip("Bounciness")] float _bounciness = 15f;
	[SerializeField, Tooltip("Boosted Bounciness")] float _boostedBounciness = 22f;
	[SerializeField, Tooltip("Max Velocity")] float _maxVelocity = 22f;
	[SerializeField, Tooltip("Max Angular Velocity")] float _maxAngularVelocity = 20f;
	[SerializeField] TrailRenderer _trail;
	[SerializeField] GameObject _playerPaddle;

	private Rigidbody _rigidbody;

	private void Awake() {
		_rigidbody = GetComponent<Rigidbody>();
		_rigidbody.maxAngularVelocity= _maxAngularVelocity;
	}

	private void FixedUpdate() {

		if (_rigidbody.position.y < 0.5) {
			BallFall();
		}

		if (_rigidbody.velocity.magnitude >= _maxVelocity) {
			_rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _maxVelocity);
		}

	}

	private void OnCollisionEnter(Collision collision) {

		if (collision.gameObject.TryGetComponent(out Block block)) {
			block.OnHitBlock();
		}

		if (collision.gameObject.TryGetComponent(out Bomb bomb)) {

			Explode(1, transform.position - bomb.gameObject.transform.position);
			bomb.OnHitBlock();
		}

	}




	public void Bounce() {
		
		_rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _bounciness, _rigidbody.velocity.z);
		_trail.enabled = false;
	}

	public void BoostedBounce() {
		
		_rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _boostedBounciness, _rigidbody.velocity.z);
		_trail.enabled = true;
	}

	public void BallFall() {

		_rigidbody.useGravity = false;
		_rigidbody.velocity = Vector3.zero;
		GetComponent<SphereCollider>().enabled = false;
		_trail.enabled = false;

		Invoke(nameof(ReturnBall), 1f);
	}

	public void Explode(int damage, Vector3 position) {

		_rigidbody.AddForceAtPosition(position * 200f * damage, transform.position);

	}

	public void PickUpItem() {



	}

	public void ReturnBall() {

		_rigidbody.useGravity = true;
		_rigidbody.AddForceAtPosition(Vector3.up  * 15f, transform.position);
		
		GetComponent<SphereCollider>().enabled = true;
		_trail.enabled = false;
	}



}
