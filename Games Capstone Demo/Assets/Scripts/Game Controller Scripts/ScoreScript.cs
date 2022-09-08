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

    [SerializeField]
    private AudioClip scoreAudioClip;
    [SerializeField]
    private AudioSource scoreAudioSource;
    private void Start()
    {
        scoreAudioSource.clip = scoreAudioClip;
    }
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
        //combo linearly decreases from startComboTimer in steps of 0.5. startComboTimer should be set 0.5 seconds higher than the timer for the first score.
        comboTimer = (startComboTimer-(combo/2));
        tempComboTimer = comboTimer;
    }
    public void AddScore(int pScore)
    {
        score += pScore*combo;

        //The twelfth root of two is an algebraic irrational number, approximately equal to 1.0594631.
        //It is most important in Western music theory, where it represents the frequency ratio (musical interval) of a semitone
        //Here, it makes the pitch of the audioclip change by half a note per combo.
        scoreAudioSource.pitch = Mathf.Pow(1.0594631f, combo -5);
        scoreAudioSource.Play();
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
        comboText.text = "Combo: x" + combo;
    }
    //public void SetCheckpoint()
    //{
    //    checkpointScore = score;
    //}
    public void DropCombo()
    {
        if (combo > 0)
        {
            combo -= 1;
            //tempComboTimer is used to allow the combo bar to be displayed properly.
            //here the combo is hardcoded to have a timer of 0.5 between dropping once it's started ticking down.

            comboTimer = 0.5f;
            if (combo > 0)
            {
                tempComboTimer = 0.5f;
            }
            else
            {
                tempComboTimer = 0;
            }
        }
    }
    public void ResetCombo()
    {
        //score = checkpointScore;
        combo = 0;
        comboTimer = 0;
    }
}
