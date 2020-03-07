using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCollider : MonoBehaviour
{

    public GameObject gameOverUI;
    public GameObject toldt;

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
        Debug.Log("GAME OVER");
        transform.parent.gameObject.GetComponent<CameraMovement>().SetSpeed(0f);
        gameOverUI.SetActive(true);
        toldt.GetComponent<Enemy>().Stop();
    }

}
