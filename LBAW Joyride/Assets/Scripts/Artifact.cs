using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : MonoBehaviour
{
    public int value = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        this.gameObject.SetActive(false);
        other.gameObject.GetComponent<PlayerScoreUpdater>().scoreController.GetComponent<ScoreController>().UpdateScore(value);
    }

}
