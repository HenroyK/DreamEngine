using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnpauseTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameContollerScript>().UnPauseTimer();
        BroadcastMessage("DeleteTrigger");
        
    }
}
