using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Obstacle
{
    private void Start()
    {
        moveSpeed = Random.Range(randomMin, randomMax);
    }
    private void Update()
    {
        base.Move();
    }
}
