using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    //Score variable, does nothing right now.
    private int score = 0;
    private int combo = 0;
    [SerializeField]
    private int comboCap = 10;
    private float comboTimer = 0; 
    public float startComboTimer = 10;
    [SerializeField]
    private float tempComboTimer = 1;

    public Image comboBar;
    public Text comboText;
    public Text scoreText;

    public int checkpointScore = 0;

    private void Update()
    {
        comboTimer -= Time.deltaTime;
        if (comboTimer <= 0)
        {
            DropCombo();
        }
        UpdateScoreboard();
    }
    public int Combo { get => combo; set => combo = value; }

    public void UpdateCombo(int pIncrement = 1)
    {
        if (combo < comboCap)
        {
            combo += pIncrement;
        }

        comboTimer = (startComboTimer-(combo/2));
        tempComboTimer = comboTimer;
    }
    public void AddScore(int pScore)
    {
        score += pScore*combo;
    }
    //public void AddScore(int pScore, int pMultiplier)
    //{
    //    score += pScore * pMultiplier;
    //}

    private void UpdateScoreboard()
    {
        //update combo meter
        if(comboTimer > 0)
        {
            comboBar.GetComponent<Image>().fillAmount = comboTimer / tempComboTimer;
        }
        else
        {
            comboBar.GetComponent<Image>().fillAmount = 0;
        }

        //update scoreboard

        scoreText.text = "Score: " + score;
        comboText.text = "Combo: " + combo;
    }
    //public void SetCheckpoint()
    //{
    //    checkpointScore = score;
    //}
    public void DropCombo()
    {
        if (combo > 1)
        {
            combo -= 1;
            comboTimer = 1;
            tempComboTimer = 1;
        }
    }
    public void ResetCombo()
    {
        //score = checkpointScore;
        combo = 0;
        comboTimer = 0;
    }
}
