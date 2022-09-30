using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScreenScoreEffect : MonoBehaviour
{
    [SerializeField]
    private float speed = 10;
    private GameObject target;
    private Vector3 targetLocation;
    [SerializeField]
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("PickupEffectTarget");

        //If statements with xPerScore changes here.
        float xPerScore = 45f/PlayerStats.levelSScore;
        targetLocation = target.transform.position + new Vector3((xPerScore*PlayerStats.scoreScreenScoreEffectCounter*10)-(45/2), 0,0);
        PlayerStats.scoreScreenScoreEffectCounter++;
    }
    private void Update()
    {
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, targetLocation, step);
        if (Vector3.Distance(transform.position, targetLocation) < 0.001f)
        {
            target.GetComponent<ScoreScreenProgressBar>().FillScore(1);
            Destroy(this.gameObject);
        }
    }
}
