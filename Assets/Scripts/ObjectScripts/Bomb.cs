using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	private Rigidbody _rigidbody;


	public void OnHitBlock() {

		DestroyBlock();

	}

	public void DestroyBlock() {
		Destroy(gameObject);
	}

}
