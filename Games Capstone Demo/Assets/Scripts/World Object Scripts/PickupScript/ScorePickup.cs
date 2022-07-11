using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : CollectableScript
{
    [SerializeField]
    private int comboIncrement = 1;
    [SerializeField]
    private int score = 1; 
    public override void PickUp()
    {
        GameObject scoreController = GameObject.FindGameObjectWithTag("GameController");
        scoreController.GetComponent<ScoreScript>().updateCombo(comboIncrement);
        scoreController.GetComponent<ScoreScript>().addScore(comboIncrement);
        Destroy(this.gameObject);
    }
}
