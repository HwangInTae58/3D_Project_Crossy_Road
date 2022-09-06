using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayObSpawn : MonoBehaviour
{
    Queue<GameObject> rockqueue;
    Queue<GameObject> treequeue;
    Queue<GameObject> otherTreequeue;
    public GameObject  rockPrefab;
    public GameObject  treePrefab;
    public GameObject  otherTreeprefab;
    int prefabCount;
    int startMinVal;
    int startMaxVal;
    int spawnCreateRandom;
    private void Awake()
    {
        prefabCount = 3;
        startMinVal = -25;
        startMaxVal = 25;
        spawnCreateRandom = 15;
    }
    private void Start()
    {
        InitQueue(10);
        SpawnObject();
    }
    private void SpawnObject()
    {
        RandomSpawn();
    }
    private void InitQueue(int count)
    {
        rockqueue = new Queue<GameObject>();
        treequeue = new Queue<GameObject>();
        otherTreequeue = new Queue<GameObject>();
        for (int i = 0; i < count; i++)
        {
            rockqueue.Enqueue(CreateObject(rockPrefab));
            treequeue.Enqueue(CreateObject(treePrefab));
            otherTreequeue.Enqueue(CreateObject(otherTreeprefab));
        }
    }
    private GameObject CreateObject(GameObject stayObject)
    {
        var ob = Instantiate(stayObject);
        ob.transform.SetParent(transform);
        ob.SetActive(false);
        return ob;
    }
    private void SetObject(int num, Vector3 pos)
    {
        switch (num) 
        {
            case 0:
            if (rockqueue.Count > 0)
            {
                var ob = rockqueue.Dequeue();
                ob.transform.localPosition = pos;
                ob.SetActive(true);
            }
            else
            {
                rockqueue.Enqueue(CreateObject(rockPrefab));
                SetObject(0, pos);
            }
            break;
             case 1:
            if (treequeue.Count > 0)
            {
                var ob = treequeue.Dequeue();
                ob.transform.localPosition = pos;
                ob.SetActive(true);
            }
            else
            {
                treequeue.Enqueue(CreateObject(treePrefab));
                SetObject(1, pos);
            }
            break;
             case 2:
            if (otherTreequeue.Count > 0)
            {
                var ob = otherTreequeue.Dequeue();
                ob.transform.localPosition = pos;
                ob.SetActive(true);
            }
            else
            {
                otherTreequeue.Enqueue(CreateObject(otherTreeprefab));
                SetObject(2, pos);
            }
            break;
        }
    }
    private void RandomSpawn()
    {
        int randomIndex;
        int randomVal;
        Vector3 offsetPos = Vector3.zero;
        //반복문으로 길의 왼쪽 부터 오른쪽 까지 탐색 하면서
        for (int i = startMinVal; i < startMaxVal; i++)
        {
            randomVal = Random.Range(0, 100);
            randomIndex = Random.Range(0, prefabCount);//참이면 소환물을 랜덤으로 정한 후
            if (Mathf.Abs(i)>= 9 && Mathf.Abs(i) < 25)
            {
                offsetPos.Set(i, 0.37f, 0f);
                SetObject(randomIndex, offsetPos);//소환
            }
            else if (randomVal < spawnCreateRandom && Mathf.Abs(i) < 9)//랜덤으로 비교하여
            {
                offsetPos.Set(i, 0.37f, 0f);
                SetObject(i, offsetPos);//소환
            }
        }
    }
}
