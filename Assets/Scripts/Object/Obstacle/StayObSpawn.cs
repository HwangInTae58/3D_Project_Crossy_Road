using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayObSpawn : MonoBehaviour
{
    public GameObject[] stayObjectList;
    public int startMinVal;
    public int startMaxVal;
    bool Right;
    private void Awake()
    {
        startMinVal = -12;
        startMaxVal = 12;
    }
    private void SpawnObject()
    {
        int ranPos = Random.Range(startMinVal, startMaxVal);
        int ran = Random.Range(0, stayObjectList.Length);
        var ob = Instantiate(stayObjectList[ran]);
    }
}
