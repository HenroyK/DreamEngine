using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadScript : MonoBehaviour
{
    public float force = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.attachedRigidbody.AddForce(Vector3.up*force, ForceMode.VelocityChange);
        }
    }
    void Update()
    {
    
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position +(Vector3.up * 0.55f), new Vector3(1,0.1f,1));
    }
}
