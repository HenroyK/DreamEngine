using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Text level1Score;
    public Text level2Score;
    public Text totalScore;
    private void Start()
    {
        level1Score.text = "Level 1: " + PlayerStats.level1Score;
        level2Score.text = "Level 2: " + PlayerStats.level2Score;
        totalScore.text = "Total Score: " + PlayerStats.totalScore;
    }
}
