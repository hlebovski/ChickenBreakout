using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part2Start : MonoBehaviour
{
    public GameObject Ball;
    public Transform PointBall;
    public GameObject Platform;
    public Transform PointPlatform;
    private void OnEnable()
    {
       
        
        Ball.transform.position = PointBall.position;
        Platform.transform.position = PointPlatform.position;

    }
}
