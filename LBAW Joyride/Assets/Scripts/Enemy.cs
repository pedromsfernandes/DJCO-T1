using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rnd = UnityEngine.Random;

public class Enemy : MonoBehaviour, IPowerUpEvents
{
    bool moving = false;

    public bool isRoastActive;

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
        isRoastActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            totalTime += Time.deltaTime;
            fireCooldownCounter -= Time.deltaTime;
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, startYPosition * Mathf.Sin(totalTime), this.transform.localPosition.z);

            if (fireCooldownCounter <= rnd.Range(-0.5f, 0.5f) && isRoastActive)
            {
                Vector3 targetVector = player.transform.position - this.transform.position;
                projectile.Fire(this.transform.position, Vector3.Normalize(targetVector), this.transform.localPosition.y > 0);
                fireCooldownCounter = fireCooldown;
            }
        }
    }

    public void SaveStartPos()
    {
        startYPosition = this.transform.localPosition.y;
        moving = true;
    }

    void IPowerUpEvents.OnPowerUpCollected(PowerUp powerUp)
    {
        if (powerUp is PowerUpNoRoasts)
        {
            isRoastActive = false;
        }
    }

    void IPowerUpEvents.OnPowerUpExpired(PowerUp powerUp)
    {
        if (powerUp is PowerUpNoRoasts)
        {
            isRoastActive = true;
        }

    }

    public void Begin()
    {
        moving = true;
        isRoastActive = true;
    }

    public void Stop()
    {
        moving = false;
        isRoastActive = false;
    }
}
