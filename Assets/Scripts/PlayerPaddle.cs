using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPaddle : MonoBehaviour {

	[Header("Paddle Config")]
	[SerializeField, Tooltip("Movement Speed")] float _paddleSpeed = 10f;
	[SerializeField, Tooltip("Boost delay")] float _boostDelay = 0.125f;
	[SerializeField, Tooltip("Horizontal Bounds")] float BoundX = 11.5f;

	[SerializeField] Rigidbody _rigidbody;
	[SerializeField] Camera _camera;

	PlayerControls controls;

	private bool _boosted;



	private void Awake() {
		_rigidbody.GetComponent<Rigidbody>();
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
	}

	private void OnCollisionEnter(Collision collision) {

		if (collision.gameObject.TryGetComponent(out Ball ball)) {
			if (_boosted) {
				ball.BoostedBounce();
			} else ball.Bounce();

		}

	}




	private void Move() {

		float movementInput = controls.Player.Movement.ReadValue<float>();

		if (movementInput > 0 && _rigidbody.position.x <= BoundX || movementInput < 0 && _rigidbody.position.x >= - BoundX) {

			_rigidbody.MovePosition(transform.position + Vector3.right * movementInput * _paddleSpeed * Time.deltaTime);

		}

	}

	private void Boost() {
		_boosted = true;
		_rigidbody.position = new Vector3(_rigidbody.position.x, _rigidbody.position.y + 0.3f, _rigidbody.position.z);
		Invoke(nameof(StopBoost), _boostDelay);

	}

	private void StopBoost() {
		_boosted = false;
		_rigidbody.position = new Vector3(_rigidbody.position.x, _rigidbody.position.y - 0.3f, _rigidbody.position.z);
	}


}
