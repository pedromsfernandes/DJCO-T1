using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public Sprite[] frames;
    public float speed;

    int currFrame = 0;
    float timeCounter = 0f;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.GetComponent<SpriteRenderer>().sprite = frames[0];
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter >= speed)
        {
            timeCounter = 0;
            currFrame++;
            if (currFrame >= frames.Length)
                currFrame = 0;
            this.transform.GetComponent<SpriteRenderer>().sprite = frames[currFrame];
        }
    }
}
