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

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= endX)
        {
            transform.position = start;
        }
        //blockRigidbody.velocity = Vector3.left * speed;
        transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
    }

    //Speed change message subscription
    public void ChangeSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
