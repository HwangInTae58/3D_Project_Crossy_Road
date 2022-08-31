﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayObSpawn : MonoBehaviour
{
    public List<GameObject> stayObjectList = new List<GameObject>();
    int startMinVal;
    int startMaxVal;
    int spawnCreateRandom;
    bool Right;
    private void Awake()
    {
        startMinVal = -12;
        startMaxVal = 12;
        spawnCreateRandom = 20;
    }
    private void Start()
    {
        SpawnObject();
    }
    private void SpawnObject()
    {
        int randomIndex;
        int randomVal;
        GameObject spawnOb;
        Vector3 offsetPos = Vector3.zero;
        //반복문으로 길의 왼쪽 부터 오른쪽 까지 탐색 하면서
        for(int i = startMinVal; i <startMaxVal; i++)
        {
            randomVal = Random.Range(0, 100);
            if(randomVal < spawnCreateRandom)//랜덤으로 비교하여
            {
                randomIndex = Random.Range(0, stayObjectList.Count);//참이면 소환물을 랜덤으로 정한 후
                spawnOb = Instantiate(stayObjectList[randomIndex].gameObject);//소환
                offsetPos.Set(i, 0.37f, 0f);
                spawnOb.SetActive(true);
                spawnOb.transform.SetParent(this.transform);
                //로컬포지션은 부모를 기준으로 위치를 가져오는 것
                spawnOb.transform.localPosition = offsetPos;
                
            }
        }


        
    }
}
