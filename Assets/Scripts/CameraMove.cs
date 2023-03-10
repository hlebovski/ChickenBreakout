using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

	[Header("Camera Config")]
	[SerializeField, Tooltip("Movement Speed")] float _cameraMoveSpeed;


	private void Awake() {

	}

	private void Update() {
		transform.Translate(-Vector3.right * _cameraMoveSpeed * Time.deltaTime, Space.World);
		//_rigidbody.MovePosition(transform.position - Vector3.right * _cameraMoveSpeed *Time.deltaTime);
	}

}
