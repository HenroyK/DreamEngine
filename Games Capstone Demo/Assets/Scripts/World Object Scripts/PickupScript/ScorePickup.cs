using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : CollectableScript
{
    [SerializeField]
    private int comboIncrement = 1;
    [SerializeField]
    private int score = 1;
    [SerializeField]
    private GameObject scoreEffect;
    [SerializeField]
    private GameObject burstEffect;
    public override void PickUp()
    {
        GameObject scoreController = GameObject.FindGameObjectWithTag("GameController");
        scoreController.GetComponent<ScoreScript>().UpdateCombo(comboIncrement);
        scoreController.GetComponent<ScoreScript>().AddScore(score);
        GameObject effect = Instantiate(scoreEffect);
        effect.transform.position = this.gameObject.transform.position;
        effect = Instantiate(burstEffect);
        effect.transform.position = this.gameObject.transform.position;
        Destroy(this.gameObject);
    }
}
