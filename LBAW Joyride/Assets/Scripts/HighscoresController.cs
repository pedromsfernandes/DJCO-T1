using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighscoresController : MonoBehaviour
{
    private string PLAYER_PREFS_STRING = "highscoreTable";
    public int capacity = 10;

    private Highscores highscores;

    public AudioClip buttonSound;

    public bool isScoreHighEnough(int score)
    {
        highscores = GetHighscores();
        return highscores.highscoreEntries.Count < capacity || score > highscores.highscoreEntries[highscores.highscoreEntries.Count - 1].score;
    }

    public void AddHighscoreEntry(int score, string name)
    {
        string jsonString = PlayerPrefs.GetString(PLAYER_PREFS_STRING);

        if (PlayerPrefs.HasKey(PLAYER_PREFS_STRING))
            highscores = JsonUtility.FromJson<Highscores>(jsonString);
        else
            highscores = new Highscores();

        Debug.Log(jsonString);

        if (highscores.highscoreEntries.Count < capacity)
        {
            HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };
            highscores.highscoreEntries.Add(highscoreEntry);
            highscores.highscoreEntries = highscores.highscoreEntries.OrderByDescending(h => h.score).ToList();
            saveHighscores();
        }
        else if (score > highscores.highscoreEntries[highscores.highscoreEntries.Count - 1].score)
        {
            HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };
            highscores.highscoreEntries.RemoveAt(highscores.highscoreEntries.Count - 1);
            highscores.highscoreEntries.Add(highscoreEntry);
            highscores.highscoreEntries = highscores.highscoreEntries.OrderByDescending(h => h.score).ToList();
            saveHighscores();
        }

    }

    public Highscores GetHighscores()
    {
        string jsonString = PlayerPrefs.GetString(PLAYER_PREFS_STRING);

        if (PlayerPrefs.HasKey(PLAYER_PREFS_STRING))
            highscores = JsonUtility.FromJson<Highscores>(jsonString);
        else
            highscores = new Highscores();

        return highscores;
    }

    private void saveHighscores()
    {
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString(PLAYER_PREFS_STRING, json);
        PlayerPrefs.Save();
    }

    public void MainMenu()
    {
        SingleAudioSource.PlayMusic(buttonSound);
        SceneManager.LoadScene("MainMenu");
    }

    public class Highscores
    {
        public List<HighscoreEntry> highscoreEntries = new List<HighscoreEntry>();
    }
}

[System.Serializable]
public class HighscoreEntry
{
    public int score;
    public string name;
}