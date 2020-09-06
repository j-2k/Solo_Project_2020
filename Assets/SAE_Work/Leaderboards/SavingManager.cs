using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SocialPlatforms.Impl;

public static class SavingManager
{
    public static void SaveScoreData(List<LeaderboardEntry> lbList)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string savePath = Application.persistentDataPath + "/playerScores.test";           //path 2 directory that wont change
        FileStream stream = new FileStream(savePath, FileMode.OpenOrCreate);

        formatter.Serialize(stream, lbList);

        stream.Close();
    }

    public static List<LeaderboardEntry> LoadScoreDataLE()
    {
        string savePath = Application.persistentDataPath + "/playerScores.test";
        if (File.Exists(savePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(savePath, FileMode.Open);
            List<LeaderboardEntry> lbData = formatter.Deserialize(stream) as List<LeaderboardEntry>;
            stream.Close();
            return lbData;
        }
        else
        {
            Debug.LogError("Save file not found? Check Path?" + savePath);
            return null;
        }
    }
}
