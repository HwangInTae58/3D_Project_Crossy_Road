using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour, IDie
{
    enum RoadType
    {
        Grass,
        Road,
        River,
        Rail
    }
    RoadType roadType;

    Queue<GameObject> queObjectPool;
    public GameObject moveObjectPrefab;
    List<Transform> repeatOb;

    public GameObject[] trainLight = new GameObject[3];
    public Transform spawnPos;
    int listCount = 0;
    int listForward = 0;
    float moveSpeed;
    float spawnDelay;
    float spawnTime;
    float trainDelay;
    float trainOn;
    bool trinSpawn;
    private void Start()
    {
        
        MoveObjectSpeed();
    }
    private void Update()
    {
        if (repeatOb != null)
            RepeatObject();
        if(repeatOb[listForward].localPosition.x >= 20)
        {
            GetObjectPool(repeatOb[listForward].gameObject);
        }
    }
    private void InitQueObject(int count)
    {
        queObjectPool = new Queue<GameObject>();
        for (int i = 0; i < count; i++)
        {
            queObjectPool.Enqueue(CreateObject(moveObjectPrefab));
        }
    }
    private GameObject CreateObject(GameObject moveObject)
    {
        var ob = Instantiate(moveObject);
        ob.transform.SetParent(transform);
        ob.SetActive(false);
        return ob;
    }
    private GameObject SetObjectPool(GameObject objec)
    {
        if(queObjectPool.Count > 0)
        {
            objec = queObjectPool.Dequeue();
            objec.GetComponent<Obstacle>().moveSpeed = moveSpeed;
            objec.transform.position = spawnPos.position;
            objec.transform.eulerAngles = transform.eulerAngles;
            objec.SetActive(true);
        }
        else
        {
            queObjectPool.Enqueue(CreateObject(objec));
            objec.transform.SetParent(transform);
        }
        return objec;
    }
    private void GetObjectPool(GameObject objec)
    {
        objec.SetActive(false);
        queObjectPool.Enqueue(objec);
        listForward++;
        if(listForward >= repeatOb.Count)
        {
            listForward = 0;
        }
    }

    private void RepeatObject()
    {
        if (spawnTime > spawnDelay) 
        { 
            spawnDelay += Time.deltaTime;
            if(roadType == RoadType.Rail)
                OnTrain();
        }
        else
        {
            repeatOb.Add(SetObjectPool(repeatOb[listCount].gameObject).transform);
            listCount++;
            if (listCount >= repeatOb.Count)
                listCount = 0;
            spawnDelay = 0;
        }
    }
    private void OnTrain()
    {
        trainDelay = spawnDelay;
        if(trainDelay >= trainOn) {
            trinSpawn = true;
            if(trinSpawn)
                StartCoroutine(LightRepeat(trinSpawn));
        }
        if(trainDelay >= spawnTime - 0.2f)
        {
            trainDelay = 0f;
            trinSpawn = false;
            StartCoroutine(LightRepeat(trinSpawn));
        }
    }
    IEnumerator LightRepeat(bool isTrain)
    {
        if (isTrain)
        {
            if (trainLight[0].activeSelf)
                trainLight[0].SetActive(false);
            if (!trainLight[1].activeSelf) { 
                trainLight[1].SetActive(true);
            if (trainLight[2].activeSelf)
                trainLight[2].SetActive(false);
                yield return new WaitForSeconds(0.4f);
            }
            if (trainLight[1].activeSelf)
            {
                trainLight[2].SetActive(true);
                trainLight[1].SetActive(false);
                yield return new WaitForSeconds(0.4f);
            }
        }
        else
        {
            if (!trainLight[0].activeSelf)
            {
                trainLight[0].SetActive(true);
            }
            for (int i = 1; i < trainLight.Length; i++)
            {
                if (trainLight[i].activeSelf)
                    trainLight[i].SetActive(false);
            }
        }
    }
    private void MoveObjectSpeed()
    {
        repeatOb = new List<Transform>();
        //이걸 전부 길에 따른 자식들을 넣어주고 계속 생성되게 하기
        if (moveObjectPrefab != null)
        {
            if (moveObjectPrefab.GetComponent<Car>())
            {
                InitQueObject(10);
                roadType = RoadType.Road;
                spawnTime = Random.Range(2f, 5.5f);
                var ob = SetObjectPool(moveObjectPrefab).GetComponent<Car>();
                ob.randomMin = 2f; ob.randomMax = 8f;
                moveSpeed = ob.SpeedGet();
                repeatOb.Add(ob.transform);
            }
            else if (moveObjectPrefab.GetComponent<Log>())
            {
                InitQueObject(10);
                roadType = RoadType.River;
                spawnTime = Random.Range(1f, 3f);
                var ob = SetObjectPool(moveObjectPrefab).GetComponent<Log>();
                ob.randomMin = 1.5f; ob.randomMax = 4f;
                moveSpeed = ob.SpeedGet();
                repeatOb.Add(ob.transform);
            }
            else if (moveObjectPrefab.GetComponent<Train>())
            {
                InitQueObject(1);
                roadType = RoadType.Rail;
                spawnTime = Random.Range(8f, 12f);
                trainOn = spawnTime - 2f;
                var ob = SetObjectPool(moveObjectPrefab).GetComponent<Train>();
                ob.randomMin = 25f; ob.randomMax = 31f;
                moveSpeed = ob.SpeedGet();
                repeatOb.Add(ob.transform);
                for (int i = 0; i < trainLight.Length; i++)
                {
                    var lightOb = Instantiate(trainLight[i]);
                    trainLight[i] = lightOb;
                    lightOb.transform.SetParent(transform);
                    lightOb.transform.localPosition = new Vector3(-2, transform.position.y, 0.5f);
                    lightOb.transform.rotation = Quaternion.Euler(0, 180, 0);
                    trainLight[i].SetActive(false);
                    if (!trainLight[0].activeSelf)
                        trainLight[0].SetActive(true);
                }
            }
        }
        else
            return;
    }
 
    public void Die(Transform player)
    {
        if (gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Debug.Log("물에 빠져 사망");
            for (int i = 0; i < player.childCount; i++)
                player.GetChild(i).gameObject.SetActive(false);
            player.GetComponent<Collider>().enabled = false;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        }
        else
            return;
    }
}
