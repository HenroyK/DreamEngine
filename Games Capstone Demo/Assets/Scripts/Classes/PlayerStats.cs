using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    public static bool hasSpawnedFPSCounter = false;
    public static bool showFPSCounter = false;
    public static int score = 0;
    public static int levelScoreCount = 0;
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
    public static int level1Score = 0;
    public static int level2Score = 0;

}
