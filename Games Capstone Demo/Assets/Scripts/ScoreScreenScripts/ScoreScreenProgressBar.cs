using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreScreenProgressBar : MonoBehaviour
{
    [SerializeField]
    private GameObject scoreProgressBar;
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
            scoreProgressBar.GetComponent<Image>().fillAmount = score/PlayerStats.levelSScore;
        }
        else
        {
            scoreProgressBar.GetComponent<Image>().fillAmount = 1;
        }

    }
}
