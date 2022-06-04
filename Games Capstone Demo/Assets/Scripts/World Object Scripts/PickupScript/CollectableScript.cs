using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectableScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PickUp();
        }
    }

    public abstract void PickUp();
}
