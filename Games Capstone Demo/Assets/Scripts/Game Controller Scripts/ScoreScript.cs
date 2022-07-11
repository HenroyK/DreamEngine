using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    //Score variable, does nothing right now.
    private int score;
    private int combo;
    private float comboTimer = 0;
    private float startComboTimer = 5;


    private void Update()
    {
        comboTimer += Time.deltaTime;
        if (comboTimer > startComboTimer)
        {
            combo = 0;
        }
    }
    public int Combo { get => combo; set => combo = value; }

    public void updateCombo(int pIncrement = 1)
    {
        combo += pIncrement;
        comboTimer = 0;
    }
    public void addScore(int pScore)
    {
        score += pScore;
    }
    public void addScore(int pScore, int pMultiplier)
    {
        score += pScore * pMultiplier;
    }

    private void updateScoreboard()
    {
        //update scoreboard
    }
}
