using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public CameraMovement camera;
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

    public void Continue()
    {
        SingleAudioSource.PlayMusic(buttonSound);
        camera.Continue();
    }
}
