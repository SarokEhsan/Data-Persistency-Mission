using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoresManager : MonoBehaviour
{
    public static ScoresManager instance;


    public Text highscore1 = null;
    public Text highscore2 = null;
    public Text highscore3 = null;

    private void Awake()
    {
        DataManager.instance.LoadHS();
        instance = this;
        ScoreWriter();

        Debug.Log(DataManager.instance.scores2);
    }
    public void ScoreWriter()
    {
        highscore1.text = $"1st: {DataManager.instance.names1}   :   {DataManager.instance.scores1} points";
        highscore2.text = $"2nd: {DataManager.instance.names2}   :   {DataManager.instance.scores2} points";
        highscore3.text = $"3rd: {DataManager.instance.names3}   :   {DataManager.instance.scores3} points";
    }

    public void BackToMenuFromHS()
    {
        SceneManager.LoadScene(0);
    }

    
}
