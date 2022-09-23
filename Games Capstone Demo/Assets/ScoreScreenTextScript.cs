using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreenTextScript : MonoBehaviour
{
    public Text scoreText;
    public Text totalScoreText;
    public float scoreUpdateDelay = 2f;
    private int scoreCounter = 0;
    private int score=0;
    private int prevTotalScore = 0;
    private float scoreUpdateTimer = 0;
    private float updateDelayCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score this stage: " + PlayerStats.score;
        totalScoreText.text = "Total Score: " + PlayerStats.totalScore;
        prevTotalScore = PlayerStats.totalScore;
        PlayerStats.totalScore += PlayerStats.score;
        score = PlayerStats.score;
        PlayerStats.score = 0;
    }
    private void Update()
    {
        scoreUpdateTimer -= Time.deltaTime;
        updateDelayCounter += Time.deltaTime;
        if (updateDelayCounter > scoreUpdateDelay && scoreUpdateTimer < 0)
        {
            if (scoreCounter < score)
            {
                scoreUpdateTimer = 0.0045f;
                scoreCounter++;
                totalScoreText.text = "Total Score: " + (prevTotalScore + scoreCounter);
            }
        }
    }
}
