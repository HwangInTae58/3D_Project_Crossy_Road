using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayObSpawn : MonoBehaviour
{
    int prefabCount;
    int startMinVal;
    int startMaxVal;
    int spawnCreateRandom;
    bool start;
    Dictionary<Vector3, Transform> objectSave = new Dictionary<Vector3, Transform>();
    private void Awake()
    {
        start = true;
        prefabCount = 3;
        startMinVal = -25;
        startMaxVal = 25;
        spawnCreateRandom = 15;
    }
    private void Start()
    {
        RandomSpawn();
        start = false;
    }
    private void OnEnable()
    {
        if (start)
            return;

        RandomSpawn();
    }
    private void SetObject(int num, Vector3 pos)
    {
        GameObject ob = null;
        switch (num) 
        {
            case 0:
            if (ObjectPool.instance.prefabType[7].Count > 0)
            {
                ob = ObjectPool.instance.prefabType[7].Dequeue();
                ob.transform.position = pos;
                ob.SetActive(true);
                  
            }
            else
            {
                ObjectPool.instance.prefabType[7] = ObjectPool.instance.InitQueue(ObjectPool.instance.prefab[7], 1);
                SetObject(0, pos);
            }
            break;
             case 1:
            if (ObjectPool.instance.prefabType[8].Count > 0)
            {
                ob = ObjectPool.instance.prefabType[8].Dequeue();
                ob.transform.position = pos;
                ob.SetActive(true);
            }
            else
            {
                ObjectPool.instance.prefabType[8] = ObjectPool.instance.InitQueue(ObjectPool.instance.prefab[8], 1);
                SetObject(1, pos);
            }
            break;
             case 2:
            if (ObjectPool.instance.prefabType[9].Count > 0)
            {
                ob = ObjectPool.instance.prefabType[9].Dequeue();
                ob.transform.position = pos;
                ob.SetActive(true);
            }
            else
            {
                ObjectPool.instance.prefabType[9] = ObjectPool.instance.InitQueue(ObjectPool.instance.prefab[9], 1);
                SetObject(2, pos);
            }
            break;
        }
       
        if(ob != null) {
            objectSave.Add(pos, ob.transform);
        }
    }
    private void OnDisable()
    {
        RemoveObject();
    }
    private void RemoveObject()
    {
        for (int i = startMinVal; i < startMaxVal; i++)
        {
            Vector3 offset = new Vector3(i, 0, transform.position.z);
            if (objectSave.ContainsKey(offset))
            {
                Transform stayOb = objectSave[offset];
                if(stayOb != null) { 
                    GetObject(stayOb.gameObject);
                    objectSave.Remove(offset);
                }
            }
            else
            {
                continue;
            }
        }
    }
    private void GetObject(GameObject objec)
    {
        if (objec.name == "Rock(Clone)") 
            ObjectPool.instance.prefabType[7].Enqueue(objec);
        else if (objec.name == "Tree1(Clone)") 
            ObjectPool.instance.prefabType[8].Enqueue(objec);
        else if (objec.name == "Tree2(Clone)")
            ObjectPool.instance.prefabType[9].Enqueue(objec);
        objec.SetActive(false);
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
                offsetPos.Set(i, 0, transform.position.z);
                SetObject(randomIndex, offsetPos);//소환
            }
            else if (randomVal < spawnCreateRandom && Mathf.Abs(i) < 9)//랜덤으로 비교하여
            {
                offsetPos.Set(i, 0, transform.position.z);
                SetObject(randomIndex, offsetPos);//소환
            }
        }
    }
}
