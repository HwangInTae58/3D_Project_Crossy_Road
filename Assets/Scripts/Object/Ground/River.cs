using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class River : MonoBehaviour, IDie
{
    public void Die()
    {
        Debug.Log("물에 빠져 죽었습니다.");
    }
}

