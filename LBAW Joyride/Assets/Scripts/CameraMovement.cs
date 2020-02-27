using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 0;

    public GameObject player;
    public GameObject[] blocks;

    int pos = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = new Vector3(this.transform.localPosition.x + speed, this.transform.localPosition.y, this.transform.localPosition.z);
        
        if(this.transform.localPosition.x > 122.4f)
        {
            for(int i = 0; i < blocks.Length; i++)
            {
                if(i != pos) {
                    blocks[i].transform.localPosition = new Vector3(blocks[i].transform.localPosition.x - 102.4f, blocks[i].transform.localPosition.y, blocks[i].transform.localPosition.z);
                }
                else {
                    blocks[i].transform.localPosition = new Vector3(102.4f * 3, blocks[i].transform.localPosition.y, blocks[i].transform.localPosition.z);
                }
            }

            player.transform.localPosition = new Vector3(player.transform.localPosition.x - 102.4f, player.transform.localPosition.y, player.transform.localPosition.z);
            this.transform.localPosition = new Vector3(this.transform.localPosition.x - 102.4f, this.transform.localPosition.y, this.transform.localPosition.z);
        
            pos++;
            if(pos > blocks.Length)
                pos = 0;
        }
    }
}
