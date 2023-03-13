using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel2Start : MonoBehaviour
{
    public GameObject Ball;
    public GameObject Platform;
    private void OnEnable()
    {
        Ball.SetActive(false);
        Platform.SetActive(false);
        Time.timeScale = 0f;

    }
}
