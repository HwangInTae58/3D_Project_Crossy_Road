using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    public List<Queue<GameObject>> prefabType;
    public List<GameObject> prefab = new List<GameObject>();
    public Transform trans;
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
       
    }
    private void Start()
    {
        prefabType = new List<Queue<GameObject>>();
        StartListAdd();
    }
    public void StartListAdd()
    {
        for (int i = 0; i < prefab.Count; i++)
        {
            prefabType.Add(InitQueue(prefab[i],20));
        }
        RoadManager.instance.UpdateGetPlayerPos((int)Vector3.zero.z);
    }
    public Queue<GameObject> InitQueue(GameObject prefab, int count)
    {
        Queue<GameObject> test_queue = new Queue<GameObject>();

        for (int i = 0; i < count; i++)
        {
            GameObject objectClone = CreateObject(prefab);
            test_queue.Enqueue(objectClone);
        }

        return test_queue;
    }
    private GameObject CreateObject(GameObject _prefab)
    {
            var ob = Instantiate(_prefab);
            ob.transform.SetParent(trans);
            ob.SetActive(false);
            return ob;
    }
}
