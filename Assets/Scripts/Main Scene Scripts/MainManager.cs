using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text bestScore;
    public GameObject GameOverText;
    public TextMeshProUGUI finishText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;
    private string playerName = DataManager.instance.tempPlayerName;
    
    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = $"Score : {playerName} {m_Points}";
        bestScore.text = null;
        ShowHighscoreOnMain();
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        string name = playerName;
        ScoreText.text = $"Score : {name} {m_Points}";
        if (m_Points >= 96)
        {
            GameOver();
            GameOverText.SetActive(false);
            finishText.gameObject.SetActive(true);
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        Destroy(GameObject.Find("Paddle"));
        Destroy(GameObject.Find("Ball"));
        if (m_Points > DataManager.instance.scores1)
        {
            DataManager.instance.scores1 = m_Points;
            DataManager.instance.names1 = DataManager.instance.tempPlayerName;
        }
        else
        {
            DataManager.instance.ScoreChecker(m_Points);
        }
        DataManager.instance.SaveHS();
        
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowHighscoreOnMain()
    {
        DataManager.instance.LoadHS();
        string name = DataManager.instance.names1;
        int score = DataManager.instance.scores1;
        bestScore.text = $"Best Score: {name} {score}";
    }
}
