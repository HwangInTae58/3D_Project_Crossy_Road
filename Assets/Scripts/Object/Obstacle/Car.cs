using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    CarData data;
    private void Start()
    {
        moveSpeed = Random.RandomRange(2f, 10f);
    }
    private void Update()
    {
        Move();
    }
    private void Move()//여기에 왼쪽을 갈지 오른쪽 갈지 정하자
    {
        float moveX = moveSpeed * Time.deltaTime;
        transform.Translate(moveX, 0, 0);
    }
}
