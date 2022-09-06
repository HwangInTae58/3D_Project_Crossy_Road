using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public static RoadManager instance;
    enum RoadControl
    {
        Road1,
        Road2,
        None
    }
    Queue<GameObject> queGrassRoad;
    Queue<GameObject> queNormalRoad;
    Queue<GameObject> queRiverRoad;
    Queue<GameObject> queRailRoad;
    public GameObject grassPrefabs;
    public GameObject roadPrefabs;
    public GameObject riverPrefabs;
    public GameObject railPrefabs;

    private Dictionary<int, Transform> roadMapDic = new Dictionary<int, Transform>();
    RoadControl control;
    public Transform roadParent;
    public int roadMinPos;
    public int lastLinePos = 0;
    int roadMaxPos;
    int frontOffsetPosZ;
    int backOffsetPosZ;
    int minPosZ;
    int deleteRoad;
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
        roadMinPos = -8;
        roadMaxPos = 20;
        frontOffsetPosZ = 20;
        backOffsetPosZ = 30;
        deleteRoad = 10;
    }
    private void SetObject(int playerPos, int num)
    {
        Vector3 offsetPos = roadParent.position;
        offsetPos.z = (float)playerPos;
        int ranAngle = Random.Range(0, 2);
        GameObject ob = null;
        switch (num)
        {
            case 0:
                if (ObjectPool.instance.prefabType[0].Count > 0)
                {
                    ob = ObjectPool.instance.prefabType[0].Dequeue();
                    ob.transform.position = offsetPos;
                    ob.SetActive(true);
                }
                else
                {
                    ObjectPool.instance.ListAdd(0, grassPrefabs,1);
                    SetObject(playerPos, 0);
                }
            break;
            case 1:
                if (ObjectPool.instance.prefabType[1].Count > 0)
                {
                    ob = ObjectPool.instance.prefabType[1].Dequeue();
                    ob.transform.position = offsetPos;
                    ob.SetActive(true);
                }
                else
                {
                    ObjectPool.instance.ListAdd(1, roadPrefabs,1);
                    SetObject(playerPos, 1);
                }
                break;
            case 2:
                if (ObjectPool.instance.prefabType[2].Count > 0)
                {
                    ob = ObjectPool.instance.prefabType[2].Dequeue();
                    ob.transform.position = offsetPos;
                    ob.SetActive(true);
                }
                else
                {
                    ObjectPool.instance.ListAdd(2, riverPrefabs,1);
                    SetObject(playerPos, 2);
                }
                break;
            case 3:
                if (ObjectPool.instance.prefabType[3].Count > 0)
                {
                    ob = ObjectPool.instance.prefabType[3].Dequeue();
                    ob.transform.position = offsetPos;
                    ob.SetActive(true);
                }
                else
                {
                    ObjectPool.instance.ListAdd(3, railPrefabs,1);
                    SetObject(playerPos, 3);
                }
                break;
        }
        if(ob != null) { 
        roadMapDic.Add(playerPos, ob.transform);
        if (ranAngle == 1)
            ob.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    private void RemoveRoad(int playerPos,int count)
    {
        if (roadMapDic.ContainsKey(playerPos))
        {
            Transform roadTrans = roadMapDic[playerPos];
            GetObject(roadTrans.gameObject, count);
            roadMapDic.Remove(playerPos);
        }
        else
        {
            Debug.Log("ERROR");
        }
    }
    private void GetObject(GameObject objec, int count)
    {
        if(objec == grassPrefabs) 
            ObjectPool.instance.ListAdd(0, objec, count);
        if (objec == roadPrefabs)
            ObjectPool.instance.ListAdd(1, objec, count);
        if(objec == riverPrefabs)
            ObjectPool.instance.ListAdd(2, objec, count);
        if(objec == railPrefabs)
            ObjectPool.instance.ListAdd(3, objec, count);
    }

    public void UpdateGetPlayerPos(int playerPos)
    {
        if (roadMapDic.Count <= 0)
        {
            //처음 라인 세팅
            int i = 0;
            minPosZ = roadMinPos;
            for (i = roadMinPos; i < roadMaxPos; i++)
            {
                if (i == 0)
                    continue;
                else if (i <= 3)
                    SetObject(i, 0);
                else if(i > 3 && i < roadMaxPos)
                {
                    if (control == RoadControl.None)
                        control = RoadControl.Road1;
                    if (control == RoadControl.Road1)
                    {
                        int random = Random.Range(0, 3);
                        if (random <= 1)
                            SetObject(i, 0);
                        if (random == 2)
                            SetObject(i,3);
                        control = RoadControl.Road2;
                    }
                    else if (control == RoadControl.Road2)
                    {
                        int random = Random.Range(0, 3);
                        if (random == 2)
                            SetObject(i,2);
                        if (random <= 1)
                            SetObject(i,1);
                        control = RoadControl.Road1;
                    }
                }
            }
            lastLinePos = i;
        }
        //새롭게 생성
        if(lastLinePos < playerPos + frontOffsetPosZ)
        {
            int offsetVal = lastLinePos;
            if(control == RoadControl.Road1) {
                int random = Random.Range(0, 4);
                if (random <= 2)
                    offsetVal = GroupGrass(lastLinePos);
                if(random == 3)
                    offsetVal = GroupRail(lastLinePos);
                control = RoadControl.Road2;
            }
            else if(control == RoadControl.Road2)
            {
                int random = Random.Range(0,3);
                if (random == 2)
                    offsetVal = GroupRiver(lastLinePos);
                if(random <= 1)
                    offsetVal = GroupRoad(lastLinePos);
                control = RoadControl.Road1;
            }
            lastLinePos += offsetVal;

        }
        //많이 지나가면 지우기
        if(playerPos - backOffsetPosZ > minPosZ - deleteRoad)
        {
            int count = minPosZ + deleteRoad;
            for (int i = minPosZ; i < count; i++)
            {
                RemoveRoad(i, count);
            }
            minPosZ += deleteRoad;
        }
    }

    #region GroupRoad
    public int GroupGrass(int playerPos)
    {
        int ranCount = Random.Range(0,6);
        for (int i = 0; i < ranCount; ++i)
        {
            SetObject(playerPos + i, 0);
        }
        return ranCount;
    }
    public int GroupRoad(int playerPos)
    {
        int ranCount = Random.Range(0,4);
        for (int i = 0; i < ranCount; i++)
        {
            SetObject(playerPos + i, 1);
        }
        return ranCount;
    }
    public int GroupRiver(int playerPos)
    {
        int ranCount = Random.Range(0,3);
        for (int i = 0; i < ranCount; i++)
        {
            SetObject(playerPos + i, 2);
        }
        return ranCount;
    }
    public int GroupRail(int playerPos)
    {
        int ranCount = Random.Range(0,2);
        for (int i = 0; i < ranCount; i++)
        {
            SetObject(playerPos + i, 3);
        }
        return ranCount;
    }
    #endregion
}
