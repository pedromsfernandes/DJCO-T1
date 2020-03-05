using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rnd = UnityEngine.Random;

public class TerrainGenerator : MonoBehaviour
{
    public int chunkWidth = 102;
    public int chunkHeight = 20;

    public GameObject unit;
    public GameObject window;
    public GameObject artifact;
    public GameObject slowCamera;
    public GameObject noRoasts;
    public GameObject[] blocks;

    int currentHeight = 4;

    void Start()
    {
        FillBlock(blocks[0]);
        FillBlock(blocks[1]);
        FillBlock(blocks[2]);
        FillBlock(blocks[3]);
    }

    public void FillBlock(GameObject block)
    {
        EmptyBlock(block);

        int[,] chunk = GenerateChunk(false);

        for (int i = 0; i < chunk.GetLength(0); i++)
            for (int j = 0; j < chunk.GetLength(1); j++)
                if (chunk[i, j] == 1)
                {
                    GameObject newUnit;
                    if (transform.Find("BlockPool").childCount == 0)
                    {
                        newUnit = (GameObject)Instantiate(unit);
                    }
                    else
                    {
                        newUnit = transform.Find("BlockPool").GetChild(0).gameObject;
                    }

                    newUnit.transform.parent = block.transform.Find("Items");
                    newUnit.transform.localPosition = new Vector3(-51f + i, -10f + (chunkHeight - j), 0);
                    newUnit.SetActive(true);
                }
                else if (chunk[i, j] == -1)
                {
                    GameObject newWindow;
                    if (transform.Find("WindowPool").childCount == 0)
                    {
                        newWindow = (GameObject)Instantiate(window);
                    }
                    else
                    {
                        newWindow = transform.Find("WindowPool").GetChild(0).gameObject;
                    }

                    newWindow.transform.parent = block.transform.Find("Items");
                    newWindow.transform.localPosition = new Vector3(-51f + i, -10f + (chunkHeight - j), 0);
                    newWindow.SetActive(true);
                }
                else if (chunk[i, j] == 2)
                {
                    GameObject newPowerUp;
                    if (transform.Find("PowerUpPool").childCount == 0)
                    {

                        double randomNumber = rnd.Range(0, 101);

                        if (randomNumber < 70)
                            newPowerUp = (GameObject)Instantiate(artifact);
                        else if(randomNumber < 90)
                            newPowerUp = (GameObject)Instantiate(noRoasts);
                        else
                            newPowerUp = (GameObject)Instantiate(slowCamera);
                    }
                    else
                    {
                        newPowerUp = transform.Find("PowerUpPool").GetChild(0).gameObject;
                    }
                    newPowerUp.transform.parent = block.transform.Find("Items");
                    newPowerUp.transform.localPosition = new Vector3(-51f + i, -10f + (chunkHeight - j), 0);
                    newPowerUp.SetActive(true);
                }
    }

    void EmptyBlock(GameObject block)
    {
        Transform items = block.transform.Find("Items");
        while (items.childCount > 0)
        {
            items.GetChild(0).gameObject.SetActive(false);

            if (items.GetChild(0).name.Contains("BasicBlock"))
            {
                items.GetChild(0).parent = transform.Find("BlockPool");
            }
            else if (items.GetChild(0).name.Contains("Artifact"))
            {
                items.GetChild(0).parent = transform.Find("PowerUpPool");
            }
            else if (items.GetChild(0).name.Contains("Window"))
            {
                items.GetChild(0).parent = transform.Find("WindowPool");
            }
        }
    }

    int[,] GenerateChunk(bool debug)
    {
        int[,] chunk = new int[chunkWidth, chunkHeight];

        int lastWidth = 0;

        int blockN = 0;
        bool lastPit = false;

        //generate first floor
        int floorWidth = rnd.Range(4, 10);
        int floorHeight = rnd.Range(-2, 3);
        currentHeight += floorHeight;
        for (int i = blockN; i < blockN + floorWidth; i++)
        {
            for (int j = 0; j <= currentHeight; j++)
            {
                GenerateBlock(chunk, chunkHeight, floorWidth, currentHeight, blockN, i, j);
            }
        }
        blockN += floorWidth;
        lastWidth = floorWidth;

        //generate chunk
        while (chunkWidth - blockN > 10)
        {
            if (!lastPit && rnd.Range(0, 100) < 40)
            {
                // create a pit
                int pitWidth = rnd.Range(1, 5);

                //generate platform over
                if (pitWidth >= 3 && currentHeight <= 12 && rnd.Range(0, 100) < 60)
                {
                    for (int i = blockN - 1; i < blockN + pitWidth - 1; i++)
                    {
                        chunk[i, chunkHeight - 1 - currentHeight - 3] = 1;
                        if (i % 2 == 0)
                            chunk[i, chunkHeight - 1 - currentHeight - 4] = 2;
                    }

                    //generate second platform over
                    if (rnd.Range(0, 100) < 60)
                    {
                        for (int i = blockN + pitWidth + 1; i < blockN + pitWidth + 4; i++)
                        {
                            chunk[i, chunkHeight - 1 - currentHeight - 6] = 1;
                            if (i % 2 == 0)
                                chunk[i, chunkHeight - 1 - currentHeight - 7] = 2;
                        }
                    }
                }

                blockN += pitWidth;
                lastPit = true;
            }
            else
            {
                // create a floor
                floorWidth = rnd.Range(2, 10);
                floorHeight = rnd.Range(-2, 3);
                currentHeight += floorHeight;
                if (currentHeight < 3) currentHeight = 3;
                if (currentHeight > 15) currentHeight = 15;
                for (int i = blockN; i < blockN + floorWidth; i++)
                {
                    for (int j = 0; j <= currentHeight; j++)
                    {
                        GenerateBlock(chunk, chunkHeight, floorWidth, currentHeight, blockN, i, j);
                    }
                }

                //if floor height < -> generate platform ahead, then generate platform ahead
                if (floorHeight < 0 && currentHeight <= 12 && rnd.Range(0, 100) < 60)
                {
                    for (int i = blockN + 2; i < blockN + floorWidth; i++)
                    {
                        chunk[i, chunkHeight - 1 - currentHeight - 3] = 1;
                        if (i % 2 == 0)
                            chunk[i, chunkHeight - 1 - currentHeight - 4] = 2;
                    }

                    if (rnd.Range(0, 100) < 60)
                    {
                        for (int i = blockN + floorWidth + 2; i < blockN + floorWidth; i++)
                        {
                            chunk[i, chunkHeight - 1 - currentHeight - 6] = 1;
                            if (i % 2 == 0)
                                chunk[i, chunkHeight - 1 - currentHeight - 7] = 2;
                        }
                    }

                }

                blockN += floorWidth;

                //if floor height > -> generate platform behind
                if (floorHeight > 0 && currentHeight <= 12 && !lastPit && rnd.Range(0, 100) < 60)
                {
                    for (int i = blockN - lastWidth; i < blockN - 2; i++)
                    {
                        chunk[i, chunkHeight - 1 - currentHeight - 3] = 1;
                        if (i % 2 == 0)
                            chunk[i, chunkHeight - 1 - currentHeight - 4] = 2;
                    }
                }

                lastWidth = floorWidth;
                lastPit = false;
            }
        }

        //generate last floor
        floorHeight = rnd.Range(-2, 3);
        currentHeight += floorHeight;
        if (currentHeight < 3) currentHeight = 3;
        if (currentHeight > 15) currentHeight = 15;
        for (int i = blockN; i < chunkWidth; i++)
        {
            for (int j = 0; j <= currentHeight; j++)
            {
                GenerateBlock(chunk, chunkHeight, floorWidth, currentHeight, blockN, i, j);
            }
        }

        if (debug) Print(chunk);

        return chunk;
    }

    void GenerateBlock(int[,] chunk, int chunkHeight, int floorWidth, int floorHeight, int blockN, int i, int j)
    {
        if (floorWidth < 2)
            chunk[i, chunkHeight - 1 - j] = 1;
        else
        {
            if (floorHeight % 2 != 0)
            {
                if (j % 2 == 0 && (blockN + i) % 2 != 0)
                    chunk[i, chunkHeight - 1 - j] = -1;
                else
                    chunk[i, chunkHeight - 1 - j] = 1;
            }
            else
            {
                if (j % 2 != 0 && (blockN + i) % 2 != 0)
                    chunk[i, chunkHeight - 1 - j] = -1;
                else
                    chunk[i, chunkHeight - 1 - j] = 1;
            }
        }
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
