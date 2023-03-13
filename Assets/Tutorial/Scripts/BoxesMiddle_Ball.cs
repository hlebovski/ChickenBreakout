using UnityEngine;
using UnityEngine.UI;

public class BoxesMiddle_Ball : MonoBehaviour
{
    public GameObject Ball;
    public GameObject Platform;
    public GameObject panel;
    private int ball2DestroyedCount = 0;
    private const int maxBall2DestroyedCount = 4;
    private bool IsPaused = false;
    private BoxesMiddle_Ball puddleBallComponent;

    private void Start()
    {
        puddleBallComponent = GetComponent<BoxesMiddle_Ball>();
    }
    private void Update()
    {
        if (IsPaused)
        {
            Ball.SetActive(false);
            Platform.SetActive(false);
            PauseLevel();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Box2"))
        {
            ball2DestroyedCount++;

            if (ball2DestroyedCount >= maxBall2DestroyedCount)
            {

                IsPaused = true;
               
                Time.timeScale = 0f;
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
