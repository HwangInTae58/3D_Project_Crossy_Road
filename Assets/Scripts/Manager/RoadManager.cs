using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public List<GameObject> roadPrefabs = new List<GameObject>();
    public Transform roadParent;
    int roadMinPos;
    int roadMaxPos;
    enum RoadType
    {
        Grass,
        Road,
        River,
        Rail
    }

    private void Awake()
    {
        roadMinPos = -8;
        roadMaxPos = 20;
    }              
    private void Start()
    {
        for (int i = roadMinPos; i < roadMaxPos; i++) { 
            RoadCreat(i);
            Debug.Log("생성");
        }
    }
    private void UpdateRoadCount(int playerPos)
    {
    }
    private void CreateRoadLine(int playerPos, int CreateNum)
    {
        GameObject roadOb = Instantiate(roadPrefabs[CreateNum]);
        roadOb.SetActive(true);
        Vector3 offsetPos = roadParent.position;
        offsetPos.z = (float)playerPos;
        roadOb.transform.SetParent(roadParent);
        roadOb.transform.position = offsetPos;
    }
    private void RoadCreat(int playerPos)
    {
        int ranIndex = Random.Range(0, roadPrefabs.Count);
        int ranAngle = Random.Range(0, 1);
        Vector3 offsetPos = roadParent.position;
        if(playerPos != 0) { 
        GameObject ob = Instantiate(roadPrefabs[ranIndex], roadParent);
        offsetPos.z = (float)playerPos;
        ob.transform.position = offsetPos;
        if (ranAngle == 1)
            ob.transform.rotation = Quaternion.Euler(0, 180, 0);
        ob.SetActive(true);
        }
    }
    
}
