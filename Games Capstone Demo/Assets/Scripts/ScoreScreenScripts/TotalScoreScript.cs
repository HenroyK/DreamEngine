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
    public GameObject stamp;
    public const int totalScoreS = 1250;
    public const int totalScoreA = 1150;
    public const int totalScoreB = 650;


    private void Start()
    {
        level1Score.text = "Level 1: " + PlayerStats.level1Score;
        level2Score.text = "Level 2: " + PlayerStats.level2Score;
        totalScore.text = "Total Score: " + PlayerStats.totalScore;
        switch (PlayerStats.totalScore)
        {
            case > totalScoreS:
                stamp.GetComponent<ScoreStampScript>().DisplayStamp(ScoreStampScript.Stamp.S);
                break;
            case > totalScoreA:
                stamp.GetComponent<ScoreStampScript>().DisplayStamp(ScoreStampScript.Stamp.A);
                break;
            case > totalScoreB:
                stamp.GetComponent<ScoreStampScript>().DisplayStamp(ScoreStampScript.Stamp.B);
                break;
            default:
                stamp.GetComponent<ScoreStampScript>().DisplayStamp(ScoreStampScript.Stamp.C);
                break;
        }

        
    }
}
