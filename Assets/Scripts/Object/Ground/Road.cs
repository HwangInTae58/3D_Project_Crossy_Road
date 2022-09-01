using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour, IDie
{
    Transform childOb;
    private void Start()
    {
        MoveObjectSpeed();
    }
    private void MoveObjectSpeed()
    {
        //이걸 전부 길에 따른 자식들을 넣어주고 계속 생성되게 하기
        if (GetComponentInChildren<Car>() != null)
        {
            var ob = GetComponentInChildren<Car>();
            childOb = ob.transform;
            ob.randomMin = 2f;
            ob.randomMax = 6f;
        }
        else if (GetComponentInChildren<Log>() != null)
        {
            var ob = GetComponentInChildren<Log>();
            childOb = ob.transform;
            ob.randomMin = 1f;
            ob.randomMax = 4f;
        }
        else if (GetComponentInChildren<Train>() != null)
        {
            var ob = GetComponentInChildren<Train>();
            childOb = ob.transform;
            ob.randomMin = 20f;
            ob.randomMax = 20f;
        }
        else
            return;
    }
    private void SpawnMoveOb(Transform ob)
    {
        Vector3 offset = new Vector3(-20, 0, 0);
        Instantiate(ob);
        ob.transform.SetParent(this.transform);
        ob.transform.localPosition = offset;
    }
    public void Die()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Debug.Log("물에 빠져 사망");
        }
        else
            return;
    }
}
