using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    static bool first = true;

    public GameObject canvas;
    public Image logo;
    public GameObject[] btns;

    public AudioClip soundtrack;
    public AudioClip btnSound;

    Vector2 screenSize;

    void Start()
    {
        //Generate world space point information for position and scale calculations
        screenSize.x = canvas.GetComponent<RectTransform>().sizeDelta.x;
        screenSize.y = canvas.GetComponent<RectTransform>().sizeDelta.y;

        logo.GetComponent<RectTransform>().sizeDelta = new Vector2(10000, screenSize.y / 3f);
        logo.GetComponent<RectTransform>().localPosition = new Vector3(0, screenSize.y / 3f, 0);

        float delta = 4.5f * screenSize.y / 9f;

        for (int i = 0; i < btns.Length; i++)
        {
            btns[i].GetComponent<RectTransform>().sizeDelta = new Vector2(screenSize.y / 2.5f, screenSize.y / 9f);
            btns[i].GetComponent<RectTransform>().localPosition = new Vector3(0, (-screenSize.y / 2f) - (screenSize.y / 9f) * (i + 1), 0);
            btns[i].transform.Find("Text").GetComponent<Text>().fontSize = (int)(btns[i].GetComponent<RectTransform>().sizeDelta.y / 2f);
        }

        if (first)
        {
            LoopAudioSource.PlayMusic(soundtrack);
            StartCoroutine(FirstMenuAnim(delta));
            first = false;
        }
        else
        {
            logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, 1f);
            foreach (GameObject btn in btns)
                btn.transform.localPosition = new Vector3(btn.transform.localPosition.x, btn.transform.localPosition.y + delta, btn.transform.localPosition.z);

        }
    }

    public void NewGame()
    {
        SingleAudioSource.PlayMusic(btnSound);
        SceneManager.LoadScene("GameScene");
    }

    public void Controls()
    {
        SingleAudioSource.PlayMusic(btnSound);
        SceneManager.LoadScene("ControlsMenu");
    }

    public void HighScores()
    {
        SingleAudioSource.PlayMusic(btnSound);
        SceneManager.LoadScene("HighScoresMenu");
    }

    public void Exit()
    {
        SingleAudioSource.PlayMusic(btnSound);
        Application.Quit();
    }

    IEnumerator FirstMenuAnim(float delta)
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 20; i++)
        {
            logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, logo.color.a + 0.05f);
            yield return new WaitForSeconds(0.03f);
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < btns.Length; i++)
        {
            StartCoroutine(ShowBtn(btns[i], delta));
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator ShowBtn(GameObject btn, float delta)
    {
        float step = delta / 20f;

        for (int i = 0; i < 20; i++)
        {
            btn.transform.localPosition = new Vector3(btn.transform.localPosition.x, btn.transform.localPosition.y + step, btn.transform.localPosition.z);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
