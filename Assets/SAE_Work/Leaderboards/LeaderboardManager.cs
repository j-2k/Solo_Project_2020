using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.UIElements;

[System.Serializable]
public class LeaderboardEntry
{
    public int score;
    public string name;
}

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] Transform lbContainer;
    [SerializeField] Transform lbTemplate;
    [SerializeField] float templateHeight;
    //====================================
    [SerializeField] List<LeaderboardEntry> listOfLBScoreEntry;
    [SerializeField] List<Transform> listOfLBTransform;
    string[] names = {"Mazen","James","Yoka","Dajo"};
    //List<string> namesL = new List<string>();
    void Start()
    {

        /*
        namesL.Add("Mazen");
        namesL.Add("James");
        namesL.Add("Yoka");
        namesL.Add("Dajo");
        */
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            int randomIndexA = Random.Range(0, names.Length);
            listOfLBScoreEntry.Add(new LeaderboardEntry { score = Random.Range(100, 1000), name = names[randomIndexA] });
            //int randomIndexL = Random.Range(0, namesL.Count);
            //listOfLBScoreEntry.Add(new LeaderboardEntry { score = Random.Range(100, 1000), name = names[randomIndexL] });
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
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            listOfLBScoreEntry.Clear();
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            foreach (GameObject temp in GameObject.FindGameObjectsWithTag("ScoreTemplate"))
            {
                if (temp.tag == "ScoreTemplate")
                {
                    Destroy(temp);
                }
            }
        }
    }

    void Awake()
    {
        lbTemplate.gameObject.SetActive(false);

        listOfLBScoreEntry = new List<LeaderboardEntry>()
        {
            new LeaderboardEntry{score = 999,name = "Ziad"},
            new LeaderboardEntry{score = 354,name = "Saman"},
            new LeaderboardEntry{score = 456,name = "Aryan"},
        };

        //sorting algo V1
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

    public void SaveData()
    {
        SavingManager.SaveScoreData(listOfLBScoreEntry);
    }

    public void LoadDataLE()
    {
        foreach (GameObject temp in GameObject.FindGameObjectsWithTag("ScoreTemplate")) //load new data aka delete our current leaderboard date & display new saved ones
        {
            if (temp.tag == "ScoreTemplate")
            {
                Destroy(temp);
            }
        }
        List<LeaderboardEntry> lbData = SavingManager.LoadScoreDataLE();
        listOfLBScoreEntry = lbData;
        listOfLBTransform = new List<Transform>();
        foreach (LeaderboardEntry entryData in listOfLBScoreEntry)
        {
            CreateNewLeaderboardEntry(entryData, lbContainer, listOfLBTransform);
        }
    }

    /*
    //==========================================================================================================================
    class LeaderboardScores
    {
        public List<LeaderboardEntry> lbScoreEntryList;
    }

    void AddScoreToLeaderboards(int scoreMethod, string nameMethod)
    {
        LeaderboardEntry scoreEntry = new LeaderboardEntry { score = scoreMethod, name = nameMethod };

        string jsonString = PlayerPrefs.GetString("leaderboardTable");
        LeaderboardScores scores = JsonUtility.FromJson<LeaderboardScores>(jsonString);

        scores.lbScoreEntryList.Add(scoreEntry);

        string json = JsonUtility.ToJson(scores);
        PlayerPrefs.SetString("leaderboardTable", json);
        PlayerPrefs.Save();
    }
    //==========================================================================================================================
    */
    /*
        AddScoreToLeaderboards(100, "Juma");
        AddScoreToLeaderboards(123, "Ziad");
        AddScoreToLeaderboards(465, "Saman");
        AddScoreToLeaderboards(657, "Mazin");
        AddScoreToLeaderboards(837, "Hassan");

        string jsonString = PlayerPrefs.GetString("leaderboardTable");
        LeaderboardScores lbScores = JsonUtility.FromJson<LeaderboardScores>(jsonString);

        //sorting algo V2
        for (int i = 0; i < lbScores.lbScoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < lbScores.lbScoreEntryList.Count; j++)
            {
                if (lbScores.lbScoreEntryList[j].score > lbScores.lbScoreEntryList[i].score)
                {
                    //swap here
                    LeaderboardEntry temp = lbScores.lbScoreEntryList[i];
                    lbScores.lbScoreEntryList[i] = lbScores.lbScoreEntryList[j];
                    lbScores.lbScoreEntryList[j] = temp;
                }
            }
        }        
        
        LeaderboardScores lbScoresObj = new LeaderboardScores { lbScoreEntryList = listOfLBScoreEntry };
        string json = JsonUtility.ToJson(lbScoresObj);
        PlayerPrefs.SetString("leaderboardTable", json);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("leaderboardTable"));
        */

    //==========================================================================================================================

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
