using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    enum GroundType
    {
        Nomal,
        Water,
        DriveWay,
        Train
    }
    public Transform prePos;
    int sizeX;
    int sizeY;
    Vector3 preGroundPos;
    private void Awake()
    {
        sizeX = 20;
        sizeY = 20;
        preGroundPos = prePos.position;
    }
    private void Update()
    {
        //start에서 랜덤한 타일들이 소환 될 수 있도록 하여야 됨
        CreateGround();
    }
    public void CreateGround()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { 
            GameObject ground = ObjectPool.instance.objectPoolList[0].Dequeue();
            ground.transform.SetParent(transform);
            ground.SetActive(true);
            ground.transform.position = new Vector3(preGroundPos.x, preGroundPos.y, preGroundPos.z + 1);
            preGroundPos = ground.transform.position;
        }
    }
}
