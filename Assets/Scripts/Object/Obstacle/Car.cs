using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Obstacle, IDie
{
    CarData data;
    private void Start()
    {
        randomMin = 2f;
        randomMax = 10f;
        moveSpeed = Random.Range(randomMin, randomMax);
    }
    private void Update()
    {
        base.Move();
    }
    public void Die()
    {
        Debug.Log("차에 치여 사망");
    }
}
