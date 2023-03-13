using UnityEngine;
using UnityEngine.UI;
public class Puddle_Ball : MonoBehaviour
{
    public GameObject Ball;
    public GameObject Platform;
    private Puddle_Ball puddleBallComponent;
    public GameObject panel;
    public int collisionCount = 0;
    private const int maxCollisions =5;
    private bool IsPaused = false;

    private void Start()
    {
        puddleBallComponent = GetComponent<Puddle_Ball>();
    }
    private void Update()
    {
        if (IsPaused)
        {
            PauseLevel();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == Ball)
        {
            collisionCount++;

            if (collisionCount >= maxCollisions)
            {
               
                IsPaused = true;
                Time.timeScale = 0f;
                Debug.Log("Yo what's up?");
            }
        }
    }
   public void CLosePuddleBall()
    {
        Destroy(puddleBallComponent);

	}
    private void PauseLevel()
    {
        panel.SetActive(true);
    }
}
