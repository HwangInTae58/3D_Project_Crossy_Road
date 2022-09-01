using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    public float randomMin;
    public float randomMax;
    virtual protected void Move()//여기에 왼쪽을 갈지 오른쪽 갈지 정하자
    {
        float moveX = moveSpeed * Time.deltaTime;
        transform.Translate(moveX, 0, 0);
    }
}
