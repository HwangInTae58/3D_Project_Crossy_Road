using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Obstacle, IDie
{
    CarData data;
    private void Update()
    {
        base.Move(SpeedGet());
    }
    public void Die(Transform player)
    {
        Debug.Log("차에 치여 사망");
        player.transform.localScale = new Vector3(0.7f, 0.1f, 0.7f); 
    }
}
