using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public AudioClip buttonSound;

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
}
