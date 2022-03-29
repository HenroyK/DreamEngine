using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public float maxSpeed;
    public float accel;
    public float jumpaccel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move left
        if (Input.GetKey(KeyCode.A)&& playerRigidbody.velocity.x > maxSpeed *-1)
        {
            playerRigidbody.velocity += Vector3.left * accel;
        }
        //move right
        if (Input.GetKey(KeyCode.D) && playerRigidbody.velocity.x < maxSpeed)
        {
            playerRigidbody.velocity += Vector3.right * accel;
        }
        //jump if y velocity 0 and spcbar
        if (Input.GetKey(KeyCode.Space)&& playerRigidbody.velocity.y ==0)
        {
            playerRigidbody.velocity += Vector3.up * jumpaccel;
        }
    }
}
