using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnPauseTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameContollerScript>().UnPauseTimer();
    }
}
