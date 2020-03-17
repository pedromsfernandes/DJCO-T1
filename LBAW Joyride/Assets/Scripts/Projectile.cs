using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 targetVector;
    bool moving = false;
    public float speed = 0.2f;

    public AudioClip fire;
    public AudioClip hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(moving)
        {
            this.transform.position = this.transform.position + (targetVector * speed * Time.deltaTime);
        }
    }

    public void Fire(Vector3 startPos, Vector3 target, bool flip)
    {
        this.gameObject.SetActive(true);
        this.transform.position = startPos;
        this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, this.transform.localEulerAngles.y, Vector3.Angle(new Vector3(1f, 0, 0), target) * (flip ? -1f : 1f));
        targetVector = target;
        moving = true;
        SingleAudioSource.PlayMusic(fire);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        SingleAudioSource.PlayMusic(hit);
        this.gameObject.SetActive(false);
        other.gameObject.GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>().Stun();
        other.gameObject.transform.Find("Particle System").GetComponent<ParticleSystem>().Play();
    }
}
