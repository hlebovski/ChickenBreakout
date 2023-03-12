using UnityEngine;
using System.Collections.Generic;
public class BoxesBehavior : MonoBehaviour
{
    public List<GameObject> objectsToToggle;
    public GameObject ball;
    public float ignoreCollisionTime = 1f;
    public PhysicMaterial Material;
    public AudioClip collisionSound; 
    public float volume = 0.5f;
    public AudioSource audioSource; 
    private int collisionCount = 0;
    private int currentObjectIndex = 0;
    private bool ignoreCollisions = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (ignoreCollisions)
        {
            return;
        }
        if (collision.gameObject == ball && collisionCount < objectsToToggle.Count)
        {
            objectsToToggle[currentObjectIndex].SetActive(false);
            currentObjectIndex++;
            objectsToToggle[currentObjectIndex].SetActive(true);
            collisionCount++;
            StartCoroutine(IgnoreCollisionsForTime(ignoreCollisionTime));
            audioSource.PlayOneShot(collisionSound, volume);
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
