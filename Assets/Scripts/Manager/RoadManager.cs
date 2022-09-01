using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public static RoadManager instance;
    enum RoadType
    {
        Grass = 0,
        Road,
        River,
        Rail,

        Max
    }
    public List<GameObject> roadPrefabs = new List<GameObject>();
    [Header("복제용 길")]
    public Transform roadParent;
    int roadMinPos;
    int roadMaxPos;
    int frontOffsetPosZ = 20;
    int backOffsetPosZ = 10;
    
   
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        roadMinPos = -5;
        roadMaxPos = 20;
    }
    private Dictionary<int, Transform> m_LineMapDic = new Dictionary<int, Transform>();
    private int lastLinePos = 0;
    
    public void UpdateGetPlayerPos(int playerPos)
    {
        if(roadParent.childCount <= 0)
        {
            //처음 라인 세팅
            int i = 0;
            for (i = roadMinPos; i < roadMaxPos; i++)
            {
                int ranRoad = Random.Range(0, 4);
                if (i == 0)
                    continue;
                else if (i <= 3)
                    RoadCreate(i, 0);
                else if(i > 3 && i < roadMaxPos)
                {
                    RoadCreate(i, ranRoad);
                }
            }
            lastLinePos = i;
            Debug.Log(lastLinePos);
        }
        //새롭게 생성
        if(lastLinePos < playerPos + frontOffsetPosZ)
        {
            int ranRoad = Random.Range(0, 4);
            int offsetVal = lastLinePos;
            RoadCreate(offsetVal, ranRoad);
            offsetVal++;
            lastLinePos = offsetVal;
        }
        //많이 지나가면 지우기
    }
    #region GroupRoad
    public int GroupGrass(int playerPos)
    {
        int ranCount = Random.Range(1, 4);
        for (int i = 0; i < ranCount; i++)
        {
            RoadCreate(playerPos + i, 0);
        }
        return ranCount;
    }
    public int GroupRoad(int playerPos)
    {
        int ranCount = Random.Range(1, 1);
        for (int i = 0; i < ranCount; i++)
        {
            RoadCreate(playerPos + i, 1);
        }
        return ranCount;
    }
    public int GroupRiver(int playerPos)
    {
        int ranCount = Random.Range(1, 1);
        for (int i = 0; i < ranCount; i++)
        {
            RoadCreate(playerPos + i, 2);
        }
        return ranCount;
    }
    public int GroupRail(int playerPos)
    {
        int ranCount = Random.Range(1, 1);
        for (int i = 0; i < ranCount; i++)
        {
            RoadCreate(playerPos + i, 3);
        }
        return ranCount;
    }
    #endregion
    #region CreateRoad
    public void RoadCreate(int playerPos, int num)
    {
        GameObject ob = Instantiate(roadPrefabs[num]);
        ob.SetActive(true);
        Vector3 offsetPos = roadParent.position;
        offsetPos.z = (float)playerPos;
        ob.transform.SetParent(roadParent);
        ob.transform.position = offsetPos;

        int ranAngle = Random.Range(0, 2);
        if (ranAngle == 1)
            ob.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    #endregion//
}
