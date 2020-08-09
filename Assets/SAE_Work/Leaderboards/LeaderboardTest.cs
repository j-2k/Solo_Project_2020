using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class LeaderboardEntry
{
    public int score;
    public string name;
}

public class LeaderboardTest : MonoBehaviour
{

    [SerializeField] Transform lbContainer;
    [SerializeField] Transform lbTemplate;
    [SerializeField] float templateHeight;
    [SerializeField] int numOfEntries;
    //====================================
    [SerializeField] List<LeaderboardEntry> listOfLBScoreEntry;
    [SerializeField] List<Transform> listOfLBTransform;
    void Awake()
    {
        lbTemplate.gameObject.SetActive(false);

        listOfLBScoreEntry = new List<LeaderboardEntry>()
        {
            new LeaderboardEntry{score = 999,name = "Ziad"},
            new LeaderboardEntry{score = 354,name = "Saman"},
            new LeaderboardEntry{score = 456,name = "Aryan"},
            new LeaderboardEntry{score = 666,name = "Juma"},
            new LeaderboardEntry{score = 123,name = "Mufasa"},
            new LeaderboardEntry{score = 446,name = "Hassan"},
            new LeaderboardEntry{score = 790,name = "Mazen"},
        };

        //sorting algo
        for (int i = 0; i < listOfLBScoreEntry.Count; i++)
        {
            for (int j = i + 1; j < listOfLBScoreEntry.Count; j++)
            {
                if (listOfLBScoreEntry[j].score > listOfLBScoreEntry[i].score)
                {
                    //swap here
                    LeaderboardEntry temp = listOfLBScoreEntry[i];
                    listOfLBScoreEntry[i] = listOfLBScoreEntry[j];
                    listOfLBScoreEntry[j] = temp;
                }
            }
        }

        listOfLBTransform = new List<Transform>();
        foreach (LeaderboardEntry entry in listOfLBScoreEntry)
        {
            CreateNewLeaderboardEntry(entry, lbContainer, listOfLBTransform);
        }

        PlayerPrefs.SetString("leaderboardTable", "10");
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("leaderboardTable"));
    }

    //Create a single line of a high score entry in our leaderboard

    void CreateNewLeaderboardEntry(LeaderboardEntry lbClassEntry, Transform container, List<Transform> listOfTransforms)
    {
        Transform lbTransform = Instantiate(lbTemplate, container);
        RectTransform lbRectTransform = lbTransform.GetComponent<RectTransform>();
        lbRectTransform.anchoredPosition = new Vector2(0, -templateHeight * listOfTransforms.Count);
        lbTransform.gameObject.SetActive(true);

        int posOrder = listOfTransforms.Count + 1;
        string posString;
        switch (posOrder)
        {
            default:
                posString = posOrder + "th";
                break;

            case 1:
                posString = "1st";
                break;
            case 2:
                posString = "2nd";
                break;
            case 3:
                posString = "3rd";
                break;
        }

        lbTransform.Find("PosSpaceTXT").GetComponent<Text>().text = posString;

        string name = lbClassEntry.name;
        lbTransform.Find("NameSpaceTXT").GetComponent<Text>().text = name;

        int scoreTest = lbClassEntry.score;
        lbTransform.Find("ScoreSpaceTXT").GetComponent<Text>().text = scoreTest.ToString();

        listOfTransforms.Add(lbTransform);
    }


/*
for (int i = 0; i < numOfEntries; i++)
{

    Transform lbTransform = Instantiate(lbTemplate, lbContainer);
    RectTransform lbRectTransform = lbTransform.GetComponent<RectTransform>();
    lbRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
    lbTransform.gameObject.SetActive(true);

    int posOrder = i + 1;
    string posString;
    switch(posOrder)
    {
        default:
            posString = posOrder + "th"; 
            break;

        case 1:
            posString = "1st";
            break;
        case 2:
            posString = "2nd";
            break;
        case 3:
            posString = "3rd";
            break;
    }

    lbTransform.Find("PosSpaceTXT").GetComponent<Text>().text = posString;

    string name = "nameTest";
    lbTransform.Find("NameSpaceTXT").GetComponent<Text>().text = name;

    int scoreTest = Random.Range(0, 100);
    lbTransform.Find("ScoreSpaceTXT").GetComponent<Text>().text = scoreTest.ToString();
}
*/
}
