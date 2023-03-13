using AudioScripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using AudioType = AudioScripts.AudioType;

public class PlayerPaddle : MonoBehaviour {

	[Header("Paddle Config")]
	[SerializeField, Tooltip("Movement Speed")] float _paddleSpeed = 20f;
	[SerializeField, Tooltip("Max Movement Delta")] float _maxMovementDelta = 0.4f;
	[SerializeField, Tooltip("Boost delay")] float _boostDelay = 0.16f;
	[SerializeField, Tooltip("Boost Period")] float _boostPeriod = 0.25f;
	[SerializeField, Tooltip("Horizontal Bounds")] float BoundX = 11.5f;

	[SerializeField] Rigidbody _rigidbody;
	[SerializeField] CameraMove _camera;
	[SerializeField] Transform _mesh;
	[SerializeField] ParticleSystem _hayParticles;
	[SerializeField] AudioController _audioController;

	PlayerControls controls;

	private bool _boosted;
	private float _defaulCameraSpeed;
	private float _timer;

	private void Awake() {
		_rigidbody.GetComponent<Rigidbody>();
		_defaulCameraSpeed = _camera.CameraMoveSpeed;
		Time.timeScale = 1f;
		//_audioController.Instance.PlayAudio(AudioType.SFX_SOUNDTRACK);
		StopHay();

		controls = new PlayerControls();
		controls.Player.Bounce.performed += context => Boost();

	}

	private void OnEnable() {
		controls.Player.Enable();
	}

	private void OnDisable() {
		controls.Player.Disable();
	}


	private void FixedUpdate() {
		Move();
		_timer += Time.deltaTime;
	}

	private void OnCollisionEnter(Collision collision) {

		if (collision.gameObject.TryGetComponent(out Ball ball)) {
			PlayHay();
			if (_boosted) {
				ball.BoostedBounce();
			} else 
				ball.Bounce();
		}

	}




	private void Move() {

		float movementInput = controls.Player.Movement.ReadValue<float>();

		if (movementInput > 0 && _rigidbody.position.x <= BoundX || movementInput < 0 && _rigidbody.position.x >= - BoundX) {

			Vector3 target = Vector3.Lerp(_rigidbody.position, _rigidbody.position + Vector3.right * movementInput * _paddleSpeed, _maxMovementDelta);
			_rigidbody.MovePosition(target);
		}

	}

	private void Boost() {
		
		if(_timer > _boostPeriod) {
			_timer = 0;
			_boosted = true;
			//_rigidbody.MovePosition(_rigidbody.position + Vector3.up * 0.1f);
			//_mesh.position += Vector3.up * 0.2f;
			_mesh.localScale = new Vector3(_mesh.localScale.x, _mesh.localScale.y * 1.3f, _mesh.localScale.z);
			//_rigidbody.position = new Vector3(_rigidbody.position.x, _rigidbody.position.y + 0.3f, _rigidbody.position.z);
			Invoke(nameof(StopBoost), _boostDelay);
		}

	}

	private void StopBoost() {
		_boosted = false;
		_rigidbody.MovePosition(_rigidbody.position - Vector3.up * 0.1f);
		//_mesh.position -= Vector3.up * 0.2f;
		_mesh.localScale = Vector3.one;
		//_rigidbody.position = new Vector3(_rigidbody.position.x, _rigidbody.position.y - 0.3f, _rigidbody.position.z);
	}

	public void PlayHay() {
		_hayParticles.Play();
		Invoke(nameof(StopHay), 0.5f);
	}

	public void StopHay() {
		_hayParticles.Stop();
	}

	public void PlayCamera() {
		if (_camera.CameraMoveSpeed == 0.001f) return;
		else _camera.CameraMoveSpeed = _defaulCameraSpeed;
	}

	public void StopCamera() {
		_camera.CameraMoveSpeed = 0;
	}


}
