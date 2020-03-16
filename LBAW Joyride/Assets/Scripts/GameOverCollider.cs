using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCollider : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject player;
    public GameObject toldt;

    public AudioClip gameOverSound;

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
        SingleAudioSource.PlayMusic(gameOverSound);
        transform.parent.gameObject.GetComponent<CameraMovement>().SetSpeed(0f);
        gameOverUI.GetComponent<GameOverUI>().OnGameOver();
        toldt.GetComponent<Enemy>().Stop();
        player.GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>().Stop();
    }

}
