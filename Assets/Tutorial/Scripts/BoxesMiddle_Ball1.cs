using UnityEngine;
using UnityEngine.UI;

public class BoxesMiddle_Ball1 : MonoBehaviour
{
    public GameObject Ball;
    public GameObject Platform;
    public GameObject panel;
    private int ball2DestroyedCount = 0;
    private const int maxBall2DestroyedCount = 10;
    private bool IsPaused = false;

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

        if (collision.gameObject.CompareTag("Box3"))
        {
            ball2DestroyedCount++;

            if (ball2DestroyedCount >= maxBall2DestroyedCount)
            {

                IsPaused = true;
                Time.timeScale = 0f;
            }
        }
    }

    private void PauseLevel()
    {
        
        panel.SetActive(true);
    }
}
