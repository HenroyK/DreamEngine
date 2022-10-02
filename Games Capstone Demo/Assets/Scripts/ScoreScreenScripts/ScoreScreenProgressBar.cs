using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreScreenProgressBar : MonoBehaviour
{
    [SerializeField]
    private GameObject scoreProgressBar;
    [SerializeField]
    private GameObject scoreStamp;
    private int score = 0;
    private float filledScore = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        score = PlayerStats.score;
    }
    public void FillScore(int pScore)
    {
        filledScore += pScore;
        if ((filledScore*10) <= score)
        {
            scoreProgressBar.GetComponent<Image>().fillAmount = filledScore*10/PlayerStats.levelSScore;
        }
        else if(score < PlayerStats.levelSScore)
        {
            scoreProgressBar.GetComponent<Image>().fillAmount = (float)score/ (float)PlayerStats.levelSScore;
            Debug.Log((float)score / (float)PlayerStats.levelSScore);
        }
        else
        {
            scoreProgressBar.GetComponent<Image>().fillAmount = 1;
        }
        if (filledScore * 10 >= score)
        {
            if (score >= PlayerStats.levelSScore)
            {
                scoreStamp.GetComponent<ScoreStampScript>().DisplayStamp(ScoreStampScript.Stamp.S);
            }
            else if (score >= PlayerStats.levelAScore)
            {
                scoreStamp.GetComponent<ScoreStampScript>().DisplayStamp(ScoreStampScript.Stamp.A);
            }
            else if (score >= PlayerStats.levelBScore)
            {
                scoreStamp.GetComponent<ScoreStampScript>().DisplayStamp(ScoreStampScript.Stamp.B);
            }
            else
            {
                scoreStamp.GetComponent<ScoreStampScript>().DisplayStamp(ScoreStampScript.Stamp.C);
            }
            this.enabled = false;
        }
    }
}
