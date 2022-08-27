using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public Transform prePos;
    Car carPrefab;
    int sizeX;
    int sizeY;
    Vector3 preGroundPos;
    private void Awake()
    {
        sizeX = 20;
        sizeY = 20;
        preGroundPos = prePos.position;
    }
    private void Start()
    {
        if (gameObject.GetComponentInChildren<Car>() != null)
        {
            carPrefab = gameObject.GetComponentInChildren<Car>();
        }
    }
    private void Update()
    {
        //start에서 랜덤한 타일들이 소환 될 수 있도록 하여야 됨
    }
    public void CreateGround(int count)
    {
          GameObject ob = ObjectPool.instance.ObjectSet(new Vector3(preGroundPos.x, preGroundPos.y, preGroundPos.z + 1), 0);
          ob.transform.SetParent(transform);
          preGroundPos = ob.transform.position;
    }
    public void CreateCar()
    {
        var ob = Instantiate(carPrefab, transform).GetComponent<Car>();
    }
}
