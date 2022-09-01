using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//오브젝트의 정보를 담기
[System.Serializable]
public struct ObjectInfo
{
    public string objectName;
    public GameObject prefab;
    public int count;
}
public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    //담길 오브젝트를 컴포넌트에 나타나게 하기
    [SerializeField]
    public ObjectInfo[] objectInfos = null;
    int obTypeCount;
    [Header("오브젝트 풀의 위치")]
    [SerializeField]
    Transform tfPoolParent; // 풀의 부모를 정해서 그곳에 POOL을 정착
    //리스트에 큐를 담고 큐마다 각자의 오브젝트가 생기도록
    public List<Queue<GameObject>> objectPoolList;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        // 리스트 초기화 작업 후
        objectPoolList = new List<Queue<GameObject>>();
        ObjectPoolState();
    }

    void ObjectPoolState()
    {
        if (objectInfos != null)
        {
            //objectInfos 배열의 크기만큼 보면서 종류별로 list에 추가
            for (int i = 0; i < objectInfos.Length; i++)
            {
                objectPoolList.Add(InsertQueue(objectInfos[i]));
            }
        }
    }

    Queue<GameObject> InsertQueue(ObjectInfo perfab_objectInfo)
    {
        Queue<GameObject> obQueue = new Queue<GameObject>();
        //반복문을 사용하여 ObjectInfo의 int값인 count만큼 돌아서 오브젝트를 생성
        for (int i = 0; i < perfab_objectInfo.count; i++)
        {
            GameObject objectClone = Instantiate(perfab_objectInfo.prefab);
            objectClone.SetActive(false);
            objectClone.transform.SetParent(tfPoolParent);
            obQueue.Enqueue(objectClone);
        }
        return obQueue;
    }
}
