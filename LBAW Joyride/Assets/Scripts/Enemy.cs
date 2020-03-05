using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rnd = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    bool moving = false;
    bool firing = false;
    float startYPosition;

    float totalTime = 0;
    float fireCooldownCounter;
    public float fireCooldown = 5f;

    // Start is called before the first frame update
    void Start()
    {
        fireCooldownCounter = fireCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if(moving)
        {
            totalTime += Time.deltaTime;
            fireCooldownCounter -= Time.deltaTime;
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, startYPosition * Mathf.Sin(totalTime), this.transform.localPosition.z);

            if(fireCooldownCounter <= rnd.Range(-0.3f, 0.3f))
            {
                moving = false;
                firing = true;
                fireCooldownCounter = 0f;
            }
        }
        else if(firing)
        {
            fireCooldownCounter += Time.deltaTime;
            
            //simulate firing
            if(fireCooldownCounter >= 2f)
            {
                moving = true;
                firing = false;
                fireCooldownCounter = fireCooldown;
            }
        }
    }

    public void SaveStartPos()
    {
        startYPosition = this.transform.localPosition.y;
        moving = true;
    }
}
