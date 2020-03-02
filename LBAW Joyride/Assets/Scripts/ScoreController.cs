using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public float score;
    Dictionary<string, int> artifactValues = new Dictionary<string, int>(){
        {
            "low", 100
        },
         {
            "medium", 200
        },
         {
            "high", 300
        }
    };

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScore(float increment)
    {
        score += increment;
    }

    public void catchArtifact(string type)
    {
        score += artifactValues[type];
    }
}
