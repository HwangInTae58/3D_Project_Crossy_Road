using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    enum PlayerDic
    {
        UP,
        Down,
        Left,
        Right,
    }
    Dictionary<KeyCode, PlayerDic> playerDic;
    PlayerDic prePlayerDir;
    Rigidbody rigid;
    Collider coll;
    Vector3 movePos;
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
        moveDelay = 0f;
        jumpForce = 3f;
    }
    private void Start()
    {
        prePlayerDir = PlayerDic.UP;
        playerDic = new Dictionary<KeyCode, PlayerDic>();
        rigid = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        PlayerCheak();
    }
    private void Update()
    {
        Action();
    }
   private void PlayerCheak()
    {
        playerDic.Add(KeyCode.W, PlayerDic.UP);
        playerDic.Add(KeyCode.S, PlayerDic.Down);
        playerDic.Add(KeyCode.A, PlayerDic.Left);
        playerDic.Add(KeyCode.D, PlayerDic.Right);
    }
    private void Action()
    {
        if (Input.anyKeyDown)
        {
            foreach(KeyValuePair<KeyCode, PlayerDic> pdic in playerDic)
            {
                if (Input.GetKeyDown(pdic.Key) && !isMove)
                {
                    Move(pdic.Value);
                    Debug.Log(pdic.Value);
                    prePlayerDir = pdic.Value;
                    DirCheak(pdic.Value);
                }
            }
        }
        if (isMove)
            MoveDelay();
        transform.position = Vector3.Lerp(transform.position, movePos, 0.1f);
    }
    private void DirCheak(PlayerDic dir)
    {
        if (dir == PlayerDic.UP)
            transform.rotation = Quaternion.Euler(0,0,0);
        if (dir == PlayerDic.Down)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        if (dir == PlayerDic.Left)
            transform.rotation = Quaternion.Euler(0, -90, 0);
        if (dir == PlayerDic.Right)
            transform.rotation = Quaternion.Euler(0, 90, 0);
    }
    private void Move(PlayerDic playerDic)
    {
        Vector3 offsetPos = transform.position;
        switch (playerDic)
        {
            case PlayerDic.UP:
                offsetPos = Vector3.forward; isMove = true;
                break;
            case PlayerDic.Down:
                offsetPos = Vector3.back;    isMove = true;
                break;
            case PlayerDic.Left:
                offsetPos = Vector3.left;    isMove = true;
                break;
            case PlayerDic.Right:
                offsetPos = Vector3.right;   isMove = true;
                break;
            default:
                Debug.Log("Error");
                break;
        }
        movePos = transform.position + offsetPos;
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
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            collision.gameObject.GetComponent<IDie>().Die();
    }
}
