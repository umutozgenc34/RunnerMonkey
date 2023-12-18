using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    [SerializeField] private Transform spawnPoint;

    int score = 0;
    private int highScore;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject player;

    
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreText();
    }

    
    void Update()
    {
        
    }

    IEnumerator SpawnObstacles()
    {
        
        while (true)
        {
            float waitTime = CalculateWaitTime();
            
            yield return new WaitForSeconds(waitTime);

            Instantiate(obstacle, spawnPoint.position, Quaternion.identity);
        }
    }
    float CalculateWaitTime()
    {
        
        return Mathf.Max(2.0f - 0.1f * score/4, 0.9f);
    }

    public void GameStart()
    {
        player.SetActive(true);
        playButton.SetActive(false);

        StartCoroutine("SpawnObstacles");
        InvokeRepeating("ScoreUpdate",2f,1f);
    }

    void ScoreUpdate()
    {
        score++;
        scoreText.text = score.ToString();
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            UpdateHighScoreText();
        }
    }

    void UpdateHighScoreText()
    {
        highScoreText.text = "High Score: " + highScore;
    }
}
