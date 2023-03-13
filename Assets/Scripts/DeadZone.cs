using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour {

	[SerializeField] CameraMove _camera;
	[SerializeField] GameObject _plate;

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.TryGetComponent(out EndZone endzone)) {
			_camera.CameraMoveSpeed = 0.001f;
			Invoke(nameof(FinishScreen), 30f);
		}
	}

	private void OnTriggerStay(Collider other) {
		if (other.gameObject.TryGetComponent(out EndZone endzone)) {
			_camera.CameraMoveSpeed = 0.001f;
		}
	}

	private void FinishScreen() {
		_plate.SetActive(true);
	}

}
