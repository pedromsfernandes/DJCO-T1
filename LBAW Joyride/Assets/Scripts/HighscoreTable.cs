using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    public float templateHeight = 20f;
    public HighscoresController highscoresController;

    void Awake()
    {
        entryContainer = transform.Find("HighscoreEntryContainer");
        entryTemplate = entryContainer.Find("HighscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscoresController.GetHighscores().highscoreEntries)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;

        switch (rank)
        {
            case 1:
                rankString = "1ST";
                break;
            case 2:
                rankString = "2ND";
                break;
            case 3:
                rankString = "3RD";
                break;
            default:
                rankString = rank + "TH";
                break;
        }

        int score = highscoreEntry.score;
        string name = highscoreEntry.name;

        entryTransform.Find("PosText").GetComponent<Text>().text = rankString;
        entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();
        entryTransform.Find("NameText").GetComponent<Text>().text = name;

        transformList.Add(entryTransform);
    }
}
