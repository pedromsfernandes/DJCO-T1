using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    bool moving = false;
    float startYPosition;

    float totalTime = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(moving)
        {
            totalTime += Time.deltaTime;
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, startYPosition * Mathf.Sin(totalTime), this.transform.localPosition.z);
        }
    }

    public void SaveStartPos()
    {
        startYPosition = this.transform.localPosition.y;
        moving = true;
    }
}
