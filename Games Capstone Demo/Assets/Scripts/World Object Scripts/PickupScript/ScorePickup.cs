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
    [SerializeField]
    public Sprite[] sprites;

    private void Start()
    {
        PlayerStats.pickupMax += 1;
        //Choose a random sprite if there is any
        if(sprites.Length > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0,sprites.Length-1)];
            gameObject.GetComponent<Animator>().enabled = false;
        }
    }

    public override void PickUp()
    {
        GameObject scoreController = GameObject.FindGameObjectWithTag("GameController");
        scoreController.GetComponent<ScoreScript>().AddScore(score);
        scoreController.GetComponent<ScoreScript>().UpdateCombo(comboIncrement);


        GameObject effect = Instantiate(scoreEffect);
        effect.transform.position = this.gameObject.transform.position;
        effect.gameObject.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        effect = Instantiate(burstEffect);
        effect.transform.position = this.gameObject.transform.position;
        PlayerStats.levelScoreCount++;
        Destroy(this.gameObject);
    }
}
