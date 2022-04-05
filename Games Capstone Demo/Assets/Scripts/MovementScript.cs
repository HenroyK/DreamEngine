using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public float yvelocity;
    public float maxSpeed;
    public float accel;
    public float jumpaccel;
    public float gravityModifier;
    public bool CanJump;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        playerRigidbody.AddForce(Vector3.down * gravityModifier * GetComponent<Rigidbody>().mass);
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
        if (Input.GetKey(KeyCode.Space) && CanJump)
        {
            playerRigidbody.velocity = Vector3.up * jumpaccel;
            CanJump = false;
        }

        HardStop();
        yvelocity = playerRigidbody.velocity.y;
    }

    void HardStop()
    {
        // stop character if player isn't moving right or left
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            playerRigidbody.velocity = new Vector3(0,playerRigidbody.velocity.y,0);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "ground")
        {
            CanJump = true;
        }
    }
}
