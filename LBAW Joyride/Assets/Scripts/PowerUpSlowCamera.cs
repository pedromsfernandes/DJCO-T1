using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSlowCamera : PowerUp
{
    public float previousSpeed;
    public float slowerSpeed = 0;

    protected override void Start()
    {
        base.Start();

        previousSpeed = 0;
    }

    public void SetPreviousSpeed(float speed)
    {
        previousSpeed = speed;
    }

    public float GetSlowerSpeed()
    {
        return slowerSpeed;
    }


    public float GetPreviousSpeed()
    {
        return previousSpeed;
    }
}
