using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

	[Header("Camera Config")]
	[SerializeField, Tooltip("Movement Speed")] public float CameraMoveSpeed;


	private void Awake() {

	}

	private void Update() {
		transform.Translate(-Vector3.right * CameraMoveSpeed * Time.deltaTime, Space.World);
		//_rigidbody.MovePosition(transform.position - Vector3.right * _cameraMoveSpeed *Time.deltaTime);
	}

}
