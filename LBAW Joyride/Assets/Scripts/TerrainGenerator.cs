using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rnd = UnityEngine.Random;

public class TerrainGenerator : MonoBehaviour
{
    public int chunkWidth = 102;
    public int chunkHeight = 20;

    public GameObject unit;
    public GameObject artifact;
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
                else if (chunk[i, j] == 2)
                {
                    GameObject newArtifact;
                    if (transform.Find("ArtifactPool").childCount == 0)
                    {
                        newArtifact = (GameObject)Instantiate(artifact);
                    }
                    else
                    {
                        newArtifact = transform.Find("ArtifactPool").GetChild(0).gameObject;
                    }
                    newArtifact.transform.parent = block.transform.Find("Items");
                    newArtifact.transform.localPosition = new Vector3(-51f + i, -10f + (chunkHeight - j), 0);
                    newArtifact.SetActive(true);
                }
    }

    void EmptyBlock(GameObject block)
    {
        while(block.transform.Find("Items").childCount > 0)
        {
            block.transform.GetChild(0).gameObject.SetActive(false);

            if(block.transform.GetChild(0).name.Contains("BasicBlock"))
            {
                block.transform.GetChild(0).parent = transform.Find("BlockPool");
            }
            else
            {
                block.transform.GetChild(0).parent = transform.Find("ArtifactPool");
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
            for (int j = 0; j < currentHeight; j++)
            {
                chunk[i, chunkHeight - 1 - j] = 1;
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
                if (currentHeight < 0) currentHeight = 0;
                if (currentHeight > 19) currentHeight = 19;
                for (int i = blockN; i < blockN + floorWidth; i++)
                {
                    for (int j = 0; j <= currentHeight; j++)
                    {
                        chunk[i, chunkHeight - 1 - j] = 1;
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
