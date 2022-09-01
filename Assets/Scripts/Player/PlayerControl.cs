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
    Transform logCompareObj;
    [SerializeField] Transform playerEye;
    Vector3 movePos;
    Vector3 m_LogOffsetPos;
    Vector3 offsetPos;
    public float jumpForce;
    bool moveCool;
    bool isMove;
    float moveTime;
    float moveDelay;
    float moveCoolTime;
    string[] layerName = new string[] { "Obstacle", "Log","StayObj" };
    int layerCount;
    Log logOb;
    
    

    private void Awake()
    {
        moveDelay = 0f;
        moveTime = 0.2f;
        isMove = false;
        moveCoolTime = 0.5f;
        moveCool = false;
        jumpForce = 30f;
    }
    private void Start()
    {
        playerDic = new Dictionary<KeyCode, PlayerDic>();
        PlayerCheak();
    }
    private void Update()
    {
        Action();
        UpdateLog();
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
                if (Input.GetKeyDown(pdic.Key) && !moveCool)
                {
                    InputMoveDir(pdic.Value);
                    Debug.Log(pdic.Value);
                    DirCheak(pdic.Value);
                }
            }
        }
        if (moveCool) 
            MoveDelay();
        if (isMove) 
            IsMoveTime();
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
    private void InputMoveDir(PlayerDic playerDic)
    {
        offsetPos = transform.position;
        
        switch (playerDic)
        {
            case PlayerDic.UP:
                offsetPos = Vector3.forward; moveCool = true; isMove = true;
                break;                                       
            case PlayerDic.Down:                             
                offsetPos = Vector3.back;    moveCool = true; isMove = true;
                break;                                       
            case PlayerDic.Left:                             
                offsetPos = Vector3.left;    moveCool = true; isMove = true;
                break;                                       
            case PlayerDic.Right:                            
                offsetPos = Vector3.right;   moveCool = true; isMove = true;
                break;
            default:
                Debug.Log("Error");
                break;
        }
        RaycastHit hitObj;
        layerCount = LayerMask.GetMask(layerName[2]);
        if (Physics.Raycast(playerEye.position, offsetPos, out hitObj,1f, layerCount)){
            movePos = transform.position;
        }
        else { 
        movePos = transform.position + offsetPos;
        }
        m_LogOffsetPos += offsetPos;
    }
    private void IsMoveTime()
    {

            if(Vector3.Distance(transform.position, movePos) > 0.1f && isMove) { 
                transform.position = Vector3.Lerp(transform.position, movePos, 0.1f);
            }
            else
            {
                transform.position = movePos;
                isMove = false;
                
            }
       // if (moveDelay >= moveTime)
            
    }
    private void MoveDelay()
    {
        moveDelay += Time.deltaTime;
        if (moveDelay >= moveCoolTime)
        {
            moveCool = false;
            moveDelay = 0f;
        }
    }
    private void UpdateLog()
    {
        if (logOb == null) { 
            return;
        }
        Vector3 actorPos = logOb.transform.position + m_LogOffsetPos;
        transform.position = actorPos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(layerName[0]))
            other.gameObject.GetComponent<IDie>().Die();
        if (other.gameObject.layer == LayerMask.NameToLayer(layerName[1]))
        {
            logOb = other.GetComponent<Log>();
            if(logOb != null)
            {
                logCompareObj = logOb.transform;
                m_LogOffsetPos = transform.position - logOb.transform.position;
            }
            return;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(logCompareObj == other.transform)
        {
            logCompareObj = null;
            logOb = null;
            m_LogOffsetPos = Vector3.zero;
        }
    }
}
