using UnityEngine;
using System.Collections.Generic;

namespace ObjectScripts
{
    public class BoxesBehavior : MonoBehaviour
    {
        public List<GameObject> objectsToToggle;
        public float ignoreCollisionTime = 1f;
        public PhysicMaterial Material;
        private int collisionCount = 0;
        private int currentObjectIndex = 0;
        private bool ignoreCollisions = false;

        public void OnHitBox()
        {
            if (ignoreCollisions) return;

            if (collisionCount < objectsToToggle.Count)
            {
                objectsToToggle[currentObjectIndex].SetActive(false);
                currentObjectIndex++;
                objectsToToggle[currentObjectIndex].SetActive(true);
                collisionCount++;
                StartCoroutine(IgnoreCollisionsForTime(ignoreCollisionTime));
            }
        }

        private IEnumerator<WaitForSeconds> IgnoreCollisionsForTime(float time)
        {
            ignoreCollisions = true;
            var collider = GetComponent<Collider>();
            collider.material = Material;
            yield return new WaitForSeconds(time);
            ignoreCollisions = false;
            collider.material = null;
        }
    }
}