using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour, IDie
{
    public GameObject moveObPrefab;
    public Transform spawnPos;
    Transform repeatOb;
    float spawnDelay;
    float spawnTime;
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
        if (spawnTime > spawnDelay)
            spawnDelay += Time.deltaTime;
        else
        {
            SpawnMoveOb(repeatOb.gameObject);
            spawnDelay = 0;
        }
        
    }

    private void MoveObjectSpeed()
    {
        //이걸 전부 길에 따른 자식들을 넣어주고 계속 생성되게 하기
        if (moveObPrefab != null)
        {
            if (moveObPrefab.GetComponent<Car>())
            {
                spawnTime = Random.Range(2f, 4f);
                var ob = SpawnMoveOb(moveObPrefab).GetComponent<Car>();
                ob.randomMin = 2f; ob.randomMax = 8f;
                ob.SpeedGet();
                repeatOb = ob.transform;
                moveObPrefab.gameObject.SetActive(true);
            }
            else if (moveObPrefab.GetComponent<Log>())
            {
                spawnTime = Random.Range(1f, 2.5f);
                var ob = SpawnMoveOb(moveObPrefab).GetComponent<Log>();
                ob.randomMin = 1.5f; ob.randomMax = 4f;
                ob.SpeedGet();
                repeatOb = ob.transform;
                moveObPrefab.gameObject.SetActive(true);
            }
            else if (moveObPrefab.GetComponent<Train>())
            {
                spawnTime = Random.Range(8f, 12f);
                var ob = SpawnMoveOb(moveObPrefab).GetComponent<Train>();
                ob.randomMin = 20f; ob.randomMax = 21f;
                ob.SpeedGet();
                repeatOb = ob.transform;
                moveObPrefab.gameObject.SetActive(true);
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
            //player.gameObject.SetActive(false);
        }
        else
            return;
    }
}
