using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject[] roadPrefab;
    public GameObject[] obstaclePrefab;
    Dictionary<GameObject, GameObject> roadInobtacle;
    private void Start()
    {
        for(int i = 0; i < roadPrefab.Length; i++) { 
        roadInobtacle.Add(roadPrefab[i], obstaclePrefab[i]);
        }
    }
    private void RandomRoadCreat()
    {

    }
}
