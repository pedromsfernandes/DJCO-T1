using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rnd = UnityEngine.Random;

public class TerrainGenerator : MonoBehaviour
{
    public int chunkWidth = 102;
    public int chunkHeight = 20;

    public GameObject unit;
    public GameObject[] blocks;

    int currentHeight = 4;

    void Start()
    {
        FillBlock(blocks[0]);
        FillBlock(blocks[1]);
        FillBlock(blocks[2]);
        FillBlock(blocks[3]);
    }

    void FillBlock(GameObject block)
    {
        int[,] chunk = GenerateChunk(true);

        for (int i = 0; i < chunk.GetLength(0); i++)
            for (int j = 0; j < chunk.GetLength(1); j++)
                if (chunk[i, j] == 1)
                {
                    GameObject newUnit = (GameObject)Instantiate(unit);
                    newUnit.transform.parent = block.transform;
                    newUnit.transform.localPosition = new Vector3(-51f + i, -10f + (chunkHeight - j), 0);
                }
    }

    int[,] GenerateChunk(bool debug)
    {
        int[,] chunk = new int[chunkWidth, chunkHeight];

        int blockN = 0;
        bool lastPit = false;

        //generate first floor
        int floorWidth = rnd.Range(4, 10);
        int floorHeight = rnd.Range(-3, 4);
        currentHeight += floorHeight;
        for (int i = blockN; i < blockN + floorWidth; i++)
        {
            for (int j = 0; j < currentHeight; j++)
            {
                chunk[i, chunkHeight - 1 - j] = 1;
            }
        }
        blockN += floorWidth;

        //generate chunk
        while (chunkWidth - blockN > 10)
        {
            if (!lastPit && rnd.Range(0, 100) < 30)
            {
                // create a pit
                blockN += rnd.Range(1, 7);
                lastPit = true;
            }
            else
            {
                // create a floor
                floorWidth = rnd.Range(2, 10);
                floorHeight = rnd.Range(-2, 3);
                currentHeight += floorHeight;
                if (currentHeight < 0) currentHeight = 0;
                if (currentHeight > 19) currentHeight = 19;
                for (int i = blockN; i < blockN + floorWidth; i++)
                {
                    for (int j = 0; j <= currentHeight; j++)
                    {
                        chunk[i, chunkHeight - 1 - j] = 1;
                    }
                }
                blockN += floorWidth;
                lastPit = false;
            }
        }

        //generate last floor
        floorHeight = rnd.Range(-3, 4);
        currentHeight += floorHeight;
        if (currentHeight < 0) currentHeight = 0;
        if (currentHeight > 19) currentHeight = 19;
        for (int i = blockN; i < chunkWidth; i++)
        {
            for (int j = 0; j < currentHeight; j++)
            {
                chunk[i, chunkHeight - 1 - j] = 1;
            }
        }

        if (debug) Print(chunk);

        return chunk;
    }

    void Print(int[,] chunk)
    {
        for (int i = 0; i < chunk.GetLength(1); i++)
        {
            string s = "";
            for (int k = 0; k < chunk.GetLength(0); k++)
            {
                s += chunk[k, i] + " ";
            }
            Debug.Log(s);
        }
    }
}
