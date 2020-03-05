using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rnd = UnityEngine.Random;

public class PowerUpArtifact : PowerUp
{
    public string type;

    protected override void Start()
    {
        double randomNumber = rnd.Range(0, 101);

        if (randomNumber < 60)
            type = "low";
        else if (randomNumber < 90)
            type = "medium";
        else
            type = "high";
    }

    public string GetArtifactType(){
        return type;
    }
}
