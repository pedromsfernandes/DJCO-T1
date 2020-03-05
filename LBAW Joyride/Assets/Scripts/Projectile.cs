using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 targetVector;
    bool moving = false;
    public float speed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(moving)
        {
            this.transform.position = this.transform.position + (targetVector * speed);
        }
    }

    public void Fire(Vector3 startPos, Vector3 target)
    {
        this.gameObject.SetActive(true);
        this.transform.position = startPos;
        targetVector = target;
        moving = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>().Stun();
    }
}
