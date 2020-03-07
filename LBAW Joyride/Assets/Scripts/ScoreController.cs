using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour, IPowerUpEvents
{
    public float score { get; set;}
    public Text scoreText;

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
        scoreText.text = ((int)score) + "";
    }

    public void CatchArtifact(string type)
    {
        score += artifactValues[type];
        scoreText.text = ((int)score) + "";
    }

    void IPowerUpEvents.OnPowerUpCollected(PowerUp powerUp)
    {
        if (powerUp is PowerUpArtifact)
        {
            this.CatchArtifact(((PowerUpArtifact)powerUp).GetArtifactType());
        }
    }

    void IPowerUpEvents.OnPowerUpExpired(PowerUp powerUp)
    {

    }
}
