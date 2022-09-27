using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMove : MonoBehaviour
{
    private Rigidbody blockRigidbody;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
		blockRigidbody = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        blockRigidbody.velocity = Vector3.left * speed;
        if (transform.position.x < -450)
        {
            Destroy(this.gameObject);
        }
    }

    //Speed change message subscription
    public void ChangeSpeed(float newSpeed)
	{
		speed = newSpeed;
	}
}
