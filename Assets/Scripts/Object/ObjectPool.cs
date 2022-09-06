using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    public List<Queue<GameObject>> prefabType = new List<Queue<GameObject>>();
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
        StartListAdd(20);
    }
    private void StartListAdd(int count)
    {
        for (int i = 0; i < prefab.Count; i++)
        {
            prefabType.Add(InitQueue(count, prefab[i]));
        }
    }
    public void ListAdd(int num,GameObject ob, int count)
    {
        Debug.Log("들");
        ob.SetActive(false);
        prefabType[num] = InitQueue(count, ob);
    }
    private Queue<GameObject> InitQueue(int count, GameObject prefab)
    {
        Queue<GameObject> test_queue = new Queue<GameObject>();

        for (int i = 0; i < count; i++)
        {
            GameObject objectClone = Instantiate(prefab);
            objectClone.SetActive(false);
            objectClone.transform.SetParent(trans);
            test_queue.Enqueue(objectClone);
        }

        return test_queue;
    }
    private GameObject CreateObject(GameObject _prefab)
    {
        if(prefabType.Count != prefab.Count) { 
            var ob = Instantiate(_prefab);
            ob.transform.SetParent(trans);
            ob.SetActive(false);
            return ob;
        }
        else
        {
            _prefab.transform.SetParent(trans);
            _prefab.SetActive(false);
            return _prefab;
        }
        
    }
}
