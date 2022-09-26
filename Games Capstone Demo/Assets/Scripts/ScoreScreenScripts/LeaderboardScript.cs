using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LeaderboardScript : MonoBehaviour
{
    private int listCounter = 0;
    private List<HighScoreEntry> leaderboard = new List<HighScoreEntry>();
    private void WriteLeaderboard()
    {
        XMLManager.instance.SaveScores(leaderboard, 1);
    }
    private void DisplayScore()
    {
        Debug.Log(PlayerStats.score);
    }
    private void DisplayLeaderboard()
    {
        leaderboard = XMLManager.instance.LoadScores(1);
        
        leaderboard.OrderBy(HighScoreEntry => HighScoreEntry.score);
        bool pass = false;
        Debug.Log("----Printing Scoreboard----");
        while (listCounter < leaderboard.Count() && listCounter < 10 && !pass)
        {
            if (PlayerStats.score > leaderboard.ElementAt(listCounter).score)
            {
                Debug.Log((listCounter + 1) + ": " + PlayerStats.score);
                pass = true;
            }
            Debug.Log((listCounter + 1) + ": " + leaderboard.ElementAt(listCounter).score);
        }
        if (!pass)
        {
            Debug.Log("***"+(listCounter + 1) + ": " + PlayerStats.score + "***");
        }
        leaderboard.Add(new HighScoreEntry { score = PlayerStats.score });

        Debug.Log("----Finished Printing----");

    }
}
