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
    PlayerIdle idle;
    Vector3 curPos;
    Vector3 dir;
    Rigidbody rigid;
    Collider coll;
    int disX;
    int disZ;
    public float jumpForce;
    float x;
    float y;

    bool isMove;
    float moveDelay;
    float moveTime;
    private void Awake()
    {
        moveDelay = 0f;
        moveTime = 0.2f;
        isMove = false;
        disX = 0;
        disZ = 0;
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
        if(!isMove)
            Move();
        else
            MoveDelay();
        PlayerCameraView();
        curPos = transform.position;
        transform.position = Vector3.Lerp(curPos, dir, 0.2f);
    }
    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.W)) { disZ++; isMove = true; }
        if (Input.GetKeyDown(KeyCode.S)) { disZ--; isMove = true; }
        if (Input.GetKeyDown(KeyCode.D)) { disX++; isMove = true; }
        if (Input.GetKeyDown(KeyCode.A)) { disX--; isMove = true; }
        //MoveJump();
        dir = new Vector3(disX, dir.y, disZ);
        
    }
    private void MoveDelay()
    {
        moveDelay += Time.deltaTime;
        if(moveDelay >= moveTime)
        {
            isMove = false;
            moveDelay = 0f;
        }
    }
    private void MoveJump()
    {
        rigid.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
    }
    private void PlayerCameraView()
    {
        Camera.main.transform.SetParent(transform);
    }
}
