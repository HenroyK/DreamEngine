using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMove : MonoBehaviour
{
    private Rigidbody blockRigidbody;

    public float speed;
    public Vector3 start;
    public float endX;

    void Start()
    {
        blockRigidbody = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // reset floor position when it reaches the end
        if (transform.position.x <= endX)
        {
            transform.position = start;
        }
        // move down the X axis at "speed"
        blockRigidbody.velocity = Vector3.left * speed;
    }

    //Speed change message subscription
    public void ChangeSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
