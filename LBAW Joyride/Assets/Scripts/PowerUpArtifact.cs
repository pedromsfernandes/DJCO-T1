using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rnd = UnityEngine.Random;

public class PowerUpArtifact : PowerUp
{
    public string type;
    public Sprite[] imgs;

    public AudioClip artifactSound;

    protected override void Start()
    {
        this.sound = artifactSound;

        double randomNumber = rnd.Range(0, 101);

        if (randomNumber < 60)
        {
            type = "low";
            this.gameObject.transform.GetComponent<SpriteRenderer>().sprite = imgs[0];
        }
        else if (randomNumber < 90)
        {
            type = "medium";
            this.gameObject.transform.GetComponent<SpriteRenderer>().sprite = imgs[1];
        }
        else
        {
            type = "high";
            this.gameObject.transform.GetComponent<SpriteRenderer>().sprite = imgs[2];
        }

    }

    public string GetArtifactType(){
        return type;
    }
}
