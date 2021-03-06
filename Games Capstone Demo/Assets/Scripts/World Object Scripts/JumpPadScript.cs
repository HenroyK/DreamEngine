using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadScript : MonoBehaviour
{
    public float force = 1;

    // Makes the player bounce upward when in contacted with the player
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.attachedRigidbody.AddForce(Vector3.up * force, ForceMode.VelocityChange);
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position +(Vector3.up * 0.55f), new Vector3(1,0.1f,1));
    }
}
