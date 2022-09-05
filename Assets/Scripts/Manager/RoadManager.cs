﻿using System.Collections;
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
    public GameObject[] roadPrefabs;
    private Dictionary<int, Transform> roadMapDic = new Dictionary<int, Transform>();
    RoadControl control;
    public Transform roadParent;
    public int roadMinPos;
    int[] roadNumver;
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
    public int lastLinePos = 0;
    public void UpdateGetPlayerPos(int playerPos)
    {
        if(roadMapDic.Count <= 0)
        {
            //처음 라인 세팅
            int i = 0;
            minPosZ = roadMinPos;
            for (i = roadMinPos; i < roadMaxPos; i++)
            {
                int ranRoad = Random.Range(0, 4);
                if (i == 0)
                    continue;
                else if (i <= 3)
                    RoadCreate(i, 0);
                else if(i > 3 && i < roadMaxPos)
                {
                    if (control == RoadControl.None)
                        control = RoadControl.Road1;
                    if (control == RoadControl.Road1)
                    {
                        int random = Random.Range(0, 4);
                        if (random <= 2)
                            RoadCreate(i, 0);
                        if (random == 3)
                            RoadCreate(i,3);
                        control = RoadControl.Road2;
                    }
                    else if (control == RoadControl.Road2)
                    {
                        int random = Random.Range(0, 3);
                        if (random == 2)
                            RoadCreate(i,2);
                        if (random <= 1)
                            RoadCreate(i,1);
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
                RemoveRoad(i);
            }
            minPosZ += deleteRoad;
        }
    }
    private void RemoveRoad(int playerPos)
    {
        if (roadMapDic.ContainsKey(playerPos))
        {
            Transform roadTrans = roadMapDic[playerPos];
            Destroy(roadTrans.gameObject);
            roadMapDic.Remove(playerPos);
        }
        else 
        {
            Debug.Log("ERROR");
        }
    }
    #region GroupRoad
    public int GroupGrass(int playerPos)
    {
        int ranCount = Random.Range(0,6);
        for (int i = 0; i < ranCount; ++i)
        {
            RoadCreate(playerPos + i, 0);
        }
        return ranCount;
    }
    public int GroupRoad(int playerPos)
    {
        int ranCount = Random.Range(0,4);
        for (int i = 0; i < ranCount; i++)
        {
            RoadCreate(playerPos + i, 1);
        }
        return ranCount;
    }
    public int GroupRiver(int playerPos)
    {
        int ranCount = Random.Range(0,3);
        for (int i = 0; i < ranCount; i++)
        {
            RoadCreate(playerPos + i, 2);
        }
        return ranCount;
    }
    public int GroupRail(int playerPos)
    {
        int ranCount = Random.Range(0,2);
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
        roadMapDic.Add(playerPos, ob.transform);
        int ranAngle = Random.Range(0, 2);
        if (ranAngle == 1)
            ob.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    #endregion
}
