using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

	[SerializeField] int _healthPoints;
	private Rigidbody _rigidbody;
	[SerializeField] private Transform _intactMesh;

	private void Awake() {
		//_rigidbody = GetComponent<Rigidbody>();
	}

	public void OnHitBlock() {
		_healthPoints--;

		if (_healthPoints <= 1) {
			_intactMesh.gameObject.SetActive(false);
		} else _intactMesh.gameObject.SetActive(true);

		if (_healthPoints == 0) {
			DestroyBlock();
		}

	}

	public void DestroyBlock() {
		Destroy(gameObject);
	}

}
