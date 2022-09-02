using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    public float randomMin;
    public float randomMax;
    virtual public float SpeedGet()
    {
        return moveSpeed = Random.Range(randomMin, randomMax);
    }
    virtual protected void Move(float moveX)
    {
        moveX = moveSpeed * Time.deltaTime;
        transform.Translate(moveX, 0, 0);
    }
}
