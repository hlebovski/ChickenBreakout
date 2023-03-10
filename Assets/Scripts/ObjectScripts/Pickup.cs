using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	[SerializeField] private float _rotatingPeriod = 100f;
	[SerializeField] private Transform _mesh;

	void Update() {
		_mesh.Rotate(0, _rotatingPeriod * Time.deltaTime, 0);
	}



	private void OnTriggerEnter(Collider other) {

		if (other.gameObject.TryGetComponent(out Ball ball)) {

			ball.PickUpItem();
			Destroy(gameObject);
		}

	}

}
