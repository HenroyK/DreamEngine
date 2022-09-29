using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    public static int score = 0;
    public static int totalScore = 0;
    //public static int numPickups = 0;
    public static void addScore(int pScore)
    {
        score += pScore;
    }

    public static int scoreScreenScoreEffectCounter = 0;
    public static int levelSScore = 600;
    public static int levelAScore = 550;
    public static int levelBScore = 300;
}
