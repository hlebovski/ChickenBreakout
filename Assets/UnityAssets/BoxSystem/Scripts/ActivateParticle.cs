using UnityEngine;
public class ActivateParticle : MonoBehaviour
{
    public GameObject particleObject; 
    private bool hasPlayed = false; 
    private void OnEnable()
    {
        if (particleObject != null && !hasPlayed)
        {
            particleObject.SetActive(true);
            hasPlayed = true;
            Invoke("DeactivateParticleEffect", particleObject.GetComponent<ParticleSystem>().main.duration); 
        }
    }
    private void DeactivateParticleEffect()
    {
        if (particleObject != null)
        {
            particleObject.SetActive(false); 
        }
    }
}
