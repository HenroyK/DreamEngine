using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreenTextScript : MonoBehaviour
{
    public Text scoreText;
    public bool level1 = false;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + PlayerStats.score;
        PlayerStats.totalScore += PlayerStats.score;
        if (level1)
        {
            PlayerStats.level1Score = PlayerStats.score;
        }
        else
        {
            PlayerStats.level2Score = PlayerStats.score;
        }
        PlayerStats.score = 0;
    }
}
