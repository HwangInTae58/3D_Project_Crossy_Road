using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    enum PlayerIdle
    {
        Ible,
        Move,
        Block,
        Die,
    }
    Vector3 pPos;
    Vector3 dir;
    Rigidbody rigid;
    Collider coll;
    int disX;
    int disY;
    float jumpForce;
    float x;
    float y;

    bool isMove;
    float moveDelay;
    float moveTime;
    private void Awake()
    {
        moveDelay = 0f;
        moveTime = 0.4f;
        isMove = false;
        disX = 0;
        disY = 0;
        moveDelay = 0f;
        jumpForce = 3f;
    }
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }
    private void Update()
    {
        Action();
    }
    private void Action()
    {
        Move();
    }
    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.W))
            disY++;
        if (Input.GetKeyDown(KeyCode.S))
            disY--;
        if (Input.GetKeyDown(KeyCode.D))
            disX++;
        if (Input.GetKeyDown(KeyCode.A))
            disX--;
        dir = new Vector3(disX, 0, disY);
        pPos = transform.position;
        transform.position = Vector3.Lerp(pPos, dir, 0.2f);
        MoveDelay();
    }
    private void MoveDelay()
    {
        isMove = true;
        if (isMove)
        {
            moveDelay += Time.deltaTime;
        }
        if(moveDelay >= moveTime)
        {
            isMove = false;
            moveDelay = 0f;
        }
    }
}
