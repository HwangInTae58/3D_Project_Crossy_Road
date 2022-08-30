using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public static RoadManager instance;
    public GameObject[] carPrefab;
    Transform lastRoad;
    int roadCount;
    int ranRoad;

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
        ranRoad = Random.Range(0, 3);
        roadCount = 4;
    }
    private void Start()
    {
    }
    private void RoadCreat()
    {

    }
    private void RandomRoad()
    {
      
    }
}
