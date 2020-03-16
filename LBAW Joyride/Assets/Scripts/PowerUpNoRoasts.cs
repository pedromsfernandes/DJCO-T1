using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpNoRoasts : PowerUp
{
    public AudioClip powerUpSound;

    protected override void Start()
    {
        base.Start();
        
        this.sound = powerUpSound;
    }
}
