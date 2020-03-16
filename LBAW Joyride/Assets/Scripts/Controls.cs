using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviour
{

    public AudioClip buttonSound;

    public void MainMenu()
    {
        SingleAudioSource.PlayMusic(buttonSound);
        SceneManager.LoadScene("MainMenu");
    }
}
