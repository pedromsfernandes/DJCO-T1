using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public CameraMovement camera;
    public AudioClip buttonSound;
    public GameObject highscoresController;
    public GameObject scoreController;

    public InputField input;

    public void PlayAgain()
    {
        SingleAudioSource.PlayMusic(buttonSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SingleAudioSource.PlayMusic(buttonSound);
        SceneManager.LoadScene("MainMenu");
    }

    public void OnGameOver()
    {
        this.gameObject.SetActive(true);

        int score = (int)scoreController.GetComponent<ScoreController>().score;

        if (highscoresController.GetComponent<HighscoresController>().isScoreHighEnough(score))
            transform.Find("HighscoreInput").gameObject.SetActive(true);
    }

    public void OnHighscoreSave()
    {
        string name = input.text;
        Debug.Log(name);
        highscoresController.GetComponent<HighscoresController>().AddHighscoreEntry((int)scoreController.GetComponent<ScoreController>().score, name);
    }
    public void Continue()
    {
        SingleAudioSource.PlayMusic(buttonSound);
        camera.Continue();
    }
}
