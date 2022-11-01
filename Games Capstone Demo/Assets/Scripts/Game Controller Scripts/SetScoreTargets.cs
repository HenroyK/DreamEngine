using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScoreTargets : MonoBehaviour
{
    public int sRank;
    public int aRank;
    public int bRank;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerStats.levelSScore = sRank;
        PlayerStats.levelAScore = aRank;
        PlayerStats.levelBScore = bRank;
        PlayerStats.scoreScreenScoreEffectCounter = 0;
        PlayerStats.levelScoreCount = 0;
        PlayerStats.pickupMax = 0;
    }
}
