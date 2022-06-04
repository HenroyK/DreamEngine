using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : CollectableScript
{
    public override void PickUp()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameContollerScript>().Score += 1;
        Destroy(this.gameObject);
    }
}
