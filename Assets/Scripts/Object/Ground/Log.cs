using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Obstacle
{
    private void Update()
    {
        base.Move(SpeedGet());
    }
}
