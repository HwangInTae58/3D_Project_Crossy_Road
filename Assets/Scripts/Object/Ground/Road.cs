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
    public GameObject moveObPrefab;
    public GameObject[] trainLight = new GameObject[3];
    public Transform spawnPos;
    Transform repeatOb;
    float spawnDelay;
    float spawnTime;

    bool trinSpawn;
    float trainDelay;
    float trainOn;
    private void Start()
    {
        MoveObjectSpeed();
    }
    private void Update()
    {
        if (repeatOb != null)
            RepeatObject();
    }
    private void RepeatObject()
    {
        if (spawnTime > spawnDelay) { 
            spawnDelay += Time.deltaTime;
            if(roadType == RoadType.Rail)
                OnTrain();
        }
        else
        {
            SpawnMoveOb(repeatOb.gameObject);
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
        //이걸 전부 길에 따른 자식들을 넣어주고 계속 생성되게 하기
        if (moveObPrefab != null)
        {
            if (moveObPrefab.GetComponent<Car>())
            {
                roadType = RoadType.Road;
                spawnTime = Random.Range(2f, 5.5f);
                var ob = SpawnMoveOb(moveObPrefab).GetComponent<Car>();
                ob.randomMin = 2f; ob.randomMax = 8f;
                ob.SpeedGet();
                repeatOb = ob.transform;
                moveObPrefab.gameObject.SetActive(true);
            }
            else if (moveObPrefab.GetComponent<Log>())
            {
                roadType = RoadType.River;
                spawnTime = Random.Range(1f, 3f);
                var ob = SpawnMoveOb(moveObPrefab).GetComponent<Log>();
                ob.randomMin = 1.5f; ob.randomMax = 4f;
                ob.SpeedGet();
                repeatOb = ob.transform;
                moveObPrefab.gameObject.SetActive(true);
            }
            else if (moveObPrefab.GetComponent<Train>())
            {
                roadType = RoadType.Rail;
                spawnTime = Random.Range(8f, 12f);
                trainOn = spawnTime - 2f;
                var ob = SpawnMoveOb(moveObPrefab).GetComponent<Train>();
                ob.randomMin = 25f; ob.randomMax = 31f;
                ob.SpeedGet();
                repeatOb = ob.transform;
                moveObPrefab.gameObject.SetActive(true);
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
    private GameObject SpawnMoveOb(GameObject objec)
    {
        var ob = Instantiate(objec);
        ob.SetActive(true);
        ob.transform.position = spawnPos.position;
        ob.transform.SetParent(transform);
        ob.transform.eulerAngles = transform.eulerAngles;
        return ob;
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
