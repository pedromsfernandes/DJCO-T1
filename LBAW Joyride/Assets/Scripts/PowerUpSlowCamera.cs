using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSlowCamera : PowerUp
{
    public float previousSpeed;
    public float slowerSpeed;
    public GameObject camera;

    protected override void Start(){
        base.Start();

        previousSpeed = 0;
        slowerSpeed = camera.GetComponent<CameraMovement>().GetSpeed();
    }

    protected override void PowerUpPayload()  // Checklist item 1
    {
        base.PowerUpPayload();

        previousSpeed = camera.GetComponent<CameraMovement>().GetSpeed();
        camera.GetComponent<CameraMovement>().SetSpeed(slowerSpeed);
    }

    protected override void PowerUpHasExpired()
    {
        camera.GetComponent<CameraMovement>().SetSpeed(previousSpeed);
        base.PowerUpHasExpired();
    }

}
