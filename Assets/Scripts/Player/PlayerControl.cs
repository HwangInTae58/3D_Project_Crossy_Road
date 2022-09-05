using System;
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
    public GameObject pig;
    Dictionary<KeyCode, PlayerDic> playerDic;
    Transform logCompareObj;
    [SerializeField] Transform playerEye;
    Vector3 movePos;
    Vector3 m_LogOffsetPos;
    Vector3 offsetPos;
    public float jumpForce;
    bool moveCool;
    bool isMove;
    bool isDie;
    bool isRiderLog;
    float moveDelay;
    float moveCoolTime;
    string[] layerName = new string[] { "Obstacle", "Log","StayObj" };
    int layerCount;
    int backMove;
    int score;
    Log logOb;


    private void Awake()
    {
        moveDelay = 0f;
        isMove = false;
        isDie = false;
        isRiderLog = false;
        moveCoolTime = 0.25f;
        moveCool = false;
        jumpForce = 30f;
        backMove = 0;
        score = 0;
    }
    private void Start()
    {
        RoadManager.instance.UpdateGetPlayerPos((int)transform.position.z);
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
        if (Input.anyKeyDown && !isDie)
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
        if(isDie)
            GameManager.instance.GameOver(isDie);
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
        RaycastHit hitObj;
        layerCount = LayerMask.GetMask(layerName[2]);
        switch (playerDic)
        {
            case PlayerDic.UP:
                offsetPos = Vector3.forward; moveCool = true; isMove = true; backMove -= 1; 
                if(!Physics.Raycast(playerEye.position, offsetPos, out hitObj, 1f, layerCount)){ 
                score++;
                GameManager.instance.UpScore(score);
                }
                break;                                                      
            case PlayerDic.Down:                             
                offsetPos = Vector3.back;    moveCool = true; isMove = true; backMove += 2; score--;
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
        if (Physics.Raycast(playerEye.position, offsetPos, out hitObj, 1f , layerCount))
            movePos = transform.position;
        else 
        movePos = transform.position + offsetPos;
        if (backMove >= 5)
            Die(transform);
    }
    private void IsMoveTime()
    {

       if(Vector3.Distance(transform.position, movePos) > 0.1f && isMove) {
            MySLerp(transform.position, movePos);
            RoadManager.instance.UpdateGetPlayerPos((int)movePos.z);
       }
       else
       {
           transform.position = movePos;
           isMove = false;
       }
            
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
        if (!Input.anyKey)
            transform.position = actorPos;
        else
        {
            foreach (KeyValuePair<KeyCode, PlayerDic> pdic in playerDic)
            {
                if (Input.GetKeyDown(pdic.Key) && !moveCool && isRiderLog)
                    MySLerp(transform.position, movePos);
            }
        }
           
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(layerName[0])) { 
            other.gameObject.GetComponent<IDie>().Die(transform);
            isDie = true;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer(layerName[1]))
        {
            logOb = other.GetComponent<Log>();
            if(logOb != null)
            {
                isRiderLog = true;
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
            isRiderLog = false;
            logCompareObj = null;
            logOb = null;
            m_LogOffsetPos = Vector3.zero;
        }
    }
    public void Die(Transform player)
    {
        Debug.Log("너무 뒤로 가서 사망");
        isDie = true;
        var ob = Instantiate(pig);
        ob.SetActive(true);
        ob.transform.position = new Vector3(movePos.x, 10, movePos.z);
    }
    private void MySLerp(Vector3 start, Vector3 end)// 포물선 이동 코드 이거 이해하기
    {
        Vector3 center = (start + end) * 0.5f;
        center -= new Vector3(0, 0.4f, 0);
        Vector3 riseRelCenter = start - center;
        Vector3 setRelCenter = end - center;
        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, 0.05f);
        transform.position += center;
    }
}
