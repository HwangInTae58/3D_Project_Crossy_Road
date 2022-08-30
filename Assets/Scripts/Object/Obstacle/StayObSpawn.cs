using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayObSpawn : MonoBehaviour
{
    public List<Transform> envirmentObjectList;
    public int startMinVal;
    public int startMaxVal;
    private void Awake()
    {
        startMinVal = -12;
        startMaxVal = 12;
    }
    void Start()
    {
        envirmentObjectList = new List<Transform>();
    }

    void Update()
    {
        
    }
}
