using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnpauseTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameContollerScript>().UnPauseTimer();
        
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Trigger"))
        {
            g.BroadcastMessage("DeleteTrigger");
        }
    }
}
