using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public string tempPlayerName;

    public string names1;
    public string names2;
    public string names3;
    public int scores1;
    public int scores2;
    public int scores3;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        
    }

    [System.Serializable]
    class SaveData
    {
        public string name1;
        public string name2;
        public string name3;
        public int score1;
        public int score2;
        public int score3;
    }
    public void SaveHS()
    {
        SaveData data = new SaveData();
        data.name1 = names1;
        data.name2 = names2;
        data.name3 = names3;
        data.score1 = scores1;
        data.score2 = scores2;
        data.score3 = scores3;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savedata.json", json);
    }

    public void LoadHS()
    {
        string path = Application.persistentDataPath + "/savedata.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            scores1 = data.score1;
            scores2 = data.score2;
            scores3 = data.score3;
            names1 = data.name1;
            names2 = data.name2;
            names3 = data.name3;
        }
            
    }

    public void ScoreChecker(int point)
    {
        string[] names = new string[3] { names1, names2, names3 };
        int[] scores = new int[3] { scores1, scores2, scores3 };

        for (int i = 0; i < 3; i++)
        {
            if (point > scores[i])
            {
                for (int j = scores.Length - 1; j > i; j--)
                {
                    scores[j] = scores[j - 1];
                    names[j] = names[j - 1];
                }
                names[i] = tempPlayerName;
                scores[i] = point;
                break;
            }
        }
    }
}
