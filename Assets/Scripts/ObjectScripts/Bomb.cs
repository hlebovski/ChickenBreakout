using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public List<GameObject> objectsToToggle;
	public float ignoreCollisionTime = 1f;
	public PhysicMaterial Material;
	private int collisionCount = 0;
	private int currentObjectIndex = 0;
	private bool ignoreCollisions = false;

	public void OnHitBomb() {
		if (ignoreCollisions) return;

		if (collisionCount < objectsToToggle.Count) {
			objectsToToggle[currentObjectIndex].SetActive(false);
			currentObjectIndex++;
			objectsToToggle[currentObjectIndex].SetActive(true);
			collisionCount++;
			StartCoroutine(IgnoreCollisionsForTime(ignoreCollisionTime));
			
		}
	}

	private IEnumerator<WaitForSeconds> IgnoreCollisionsForTime(float time) {
		ignoreCollisions = true;
		var collider = GetComponent<Collider>();
		collider.material = Material;
		yield return new WaitForSeconds(time);
		ignoreCollisions = false;
		collider.material = null;
	}

	public void DestroyBlock() {
		Destroy(gameObject);
	}

}
