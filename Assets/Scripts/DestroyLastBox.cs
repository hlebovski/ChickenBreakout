using AudioScripts;
using UnityEngine;
public class DestroyLastBox : MonoBehaviour
{
    public GameObject[] Chunks;
    public GameObject ParentObj;
    public float ExplosionForce = 10;
    public float ChunksRotation = 10;
    public float time = 1;
    public float chunkDestroyTime = 2f;
    public GameObject explosionPoint;
    private bool isActivated = false;
    private bool hasExploded = false;
    private AudioSource _audioSource;

	private void Awake() {
		_audioSource = GetComponent<AudioSource>();
	}

	void Update()
    {
        if (gameObject.activeSelf && !isActivated)
        {
            Crushing();
            isActivated = true;
        }
    }

    void Crushing()
    {
        if (!hasExploded)
        {
            hasExploded = true;
        }

        foreach (GameObject chunk in Chunks)
        {
            _audioSource.Play();
			chunk.SetActive(true);
            chunk.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * -ExplosionForce);
            chunk.GetComponent<Rigidbody>().AddRelativeTorque(Vector3.forward * -ChunksRotation * Random.Range(-5f, 5f));
            //chunk.GetComponent<Rigidbody>().AddRelativeTorque(Vector3.right * -ChunksRotation * Random.Range(-5f, 5f));
            Destroy(chunk, chunkDestroyTime);
        }
		var collider = ParentObj.GetComponent<Collider>();
		collider.enabled = false;
		Invoke("DestructObject", time);

        if (explosionPoint)
        {
            explosionPoint.SetActive(true);
            Invoke("DisableExplosionPoint", 0.5f);
        }
    }
    void DestructObject()
    {
        Destroy(ParentObj);
    }
    void DisableExplosionPoint()
    {
        explosionPoint.SetActive(false);
    }
}
