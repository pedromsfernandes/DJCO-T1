using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rnd = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    bool moving = false;
    float startYPosition;

    float totalTime = 0;
    float fireCooldownCounter;

    public GameObject player;
    public Projectile projectile;
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

            if(fireCooldownCounter <= rnd.Range(-0.5f, 0.5f))
            {
                Vector3 targetVector = player.transform.position - this.transform.position;
                projectile.Fire(this.transform.position, Vector3.Normalize(targetVector));
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
