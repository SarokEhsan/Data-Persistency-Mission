using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUI : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TextMeshProUGUI highScoreOnMenu;
    // Start is called before the first frame update
    void Start()
    {
        ShowHighScoreOnMenu();
    }

    public void ShowHighScoreOnMenu()
    {
        DataManager.instance.LoadHS();
        string name = DataManager.instance.names1;
        int score = DataManager.instance.scores1;
        highScoreOnMenu.text = $"Highscore: {name} {score}";
    }

    public void GetName()
    {
        string nameEntered = nameInput.text;
        DataManager.instance.tempPlayerName = nameEntered;
    }

    public void LoadMain()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadHighscoreScene()
    {
        SceneManager.LoadScene(2);
    }

    public void GameExit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
