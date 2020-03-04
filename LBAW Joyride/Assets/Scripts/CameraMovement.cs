using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 0;

    public GameObject player;
    public GameObject[] blocks;
    public TerrainGenerator terrainGenerator;
    public float colliderColDepth = 4f;
    public float colliderZPosition = 0f;

    int pos = 0;
    Vector3 cameraPos;
    Vector2 screenSize;

    public GameObject scoreController;

    public DateTime startTime;

    public int speedIncreaseInterval = 5;

    public float speedIncreaseFactor = 1.25f;

    int lastSeconds;

    // Start is called before the first frame update
    void Start()
    {
        //Generate world space point information for position and scale calculations
        cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

        this.transform.Find("RightCollider").localScale = new Vector3(colliderColDepth, screenSize.y * 2, colliderColDepth);
        this.transform.Find("RightCollider").position = new Vector3(cameraPos.x + screenSize.x + (this.transform.Find("RightCollider").localScale.x * 0.5f), cameraPos.y, colliderZPosition);
        this.transform.Find("LeftCollider").localScale = new Vector3(colliderColDepth, screenSize.y * 2, colliderColDepth);
        this.transform.Find("LeftCollider").position = new Vector3(cameraPos.x - screenSize.x - (this.transform.Find("LeftCollider").localScale.x * 0.5f), cameraPos.y, colliderZPosition);
        this.transform.Find("BottomCollider").localScale = new Vector3(screenSize.x * 2, colliderColDepth, colliderColDepth);
        this.transform.Find("BottomCollider").position = new Vector3(cameraPos.x, (cameraPos.y - screenSize.y - (this.transform.Find("BottomCollider").localScale.y * 0.5f)) * 1.4f, colliderZPosition);

        startTime = DateTime.Now;
        lastSeconds = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = new Vector3(this.transform.localPosition.x + speed, this.transform.localPosition.y, this.transform.localPosition.z);

        if (this.transform.localPosition.x > 152f)
        {
            for (int i = 0; i < blocks.Length; i++)
            {
                if (i != pos)
                {
                    blocks[i].transform.localPosition = new Vector3(blocks[i].transform.localPosition.x - 102f, blocks[i].transform.localPosition.y, blocks[i].transform.localPosition.z);
                }
                else
                {
                    blocks[i].transform.localPosition = new Vector3(102f * 3 + 50, blocks[i].transform.localPosition.y, blocks[i].transform.localPosition.z);
                }
            }

            player.transform.localPosition = new Vector3(player.transform.localPosition.x - 102f, player.transform.localPosition.y, player.transform.localPosition.z);
            this.transform.localPosition = new Vector3(this.transform.localPosition.x - 102f, this.transform.localPosition.y, this.transform.localPosition.z);

            terrainGenerator.FillBlock(blocks[pos]);

            pos++;
            if (pos > blocks.Length)
                pos = 0;
        }

        // Update score
        scoreController.GetComponent<ScoreController>().UpdateScore(speed);

        // Update speed
        UpdateSpeed();
    }

    void UpdateSpeed()
    {
        DateTime currTime = DateTime.Now;
        int seconds = (int)(currTime - startTime).TotalSeconds;

        if (seconds > 0 && seconds % speedIncreaseInterval == 0 && lastSeconds != seconds)
        {
            speed *= speedIncreaseFactor;
            lastSeconds = seconds;
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

}
