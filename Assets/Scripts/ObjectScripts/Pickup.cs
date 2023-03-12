using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	[SerializeField] private float _rotatingPeriod = 100f;
	[SerializeField] private Transform _mesh;
	private ObjectWithScore _objectWithScore;

	private void Awake()
	{
		_objectWithScore = GetComponent<ObjectWithScore>();
	}

	void Update() {
		_mesh.Rotate(0, _rotatingPeriod * Time.deltaTime, 0);
	}



	private void OnTriggerEnter(Collider other) {

		if (other.gameObject.TryGetComponent(out Ball ball))
		{
			var score = _objectWithScore?.GetScoreCount() ?? 0;
			ball.PickUpItem(score);
			Destroy(gameObject);
		}

	}

}
