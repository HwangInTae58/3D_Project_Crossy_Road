using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Obstacle
{
    private void Start()
    {
        randomMin = 1.3f;
        randomMax = 4f;
        moveSpeed = Random.Range(randomMin, randomMax);
    }
    private void Update()
    {
        base.Move();
    }
}
