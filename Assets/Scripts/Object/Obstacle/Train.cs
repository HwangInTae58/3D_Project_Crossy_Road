using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : Obstacle, IDie
{
    private void Update()
    {
        base.Move(SpeedGet());
    }
    private void Move()
    {

    }
    public void Die(Transform player)
    {
        Debug.Log("기차에 치여 사망");
        player.transform.localScale = new Vector3(0.7f, 0.1f, 0.7f);
    }
}
