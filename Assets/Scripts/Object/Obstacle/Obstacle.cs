using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 0;
    public float randomMin;
    public float randomMax;
    virtual public float SpeedGet()
    {
        if(moveSpeed == 0)
            moveSpeed = Random.Range(randomMin, randomMax);
        return moveSpeed;
    }
    virtual protected void Move(float moveX)
    {
        moveX = moveSpeed * Time.deltaTime;
        transform.Translate(moveX, 0, 0);

    }
}
