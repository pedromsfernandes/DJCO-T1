using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimator : MonoBehaviour
{
    public Sprite[] frames;
    public float speed;

    int currFrame = 0;
    float timeCounter = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (this.transform.GetComponent<SpriteRenderer>() == null)
            this.transform.GetComponent<Image>().sprite = frames[0];
        else
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
            if (this.transform.GetComponent<SpriteRenderer>() == null)
                this.transform.GetComponent<Image>().sprite = frames[currFrame];
            else
                this.transform.GetComponent<SpriteRenderer>().sprite = frames[currFrame];
        }
    }
}
