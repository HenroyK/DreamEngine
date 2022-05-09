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

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -100)
        {
            Destroy(this.gameObject);
        }
        blockRigidbody.velocity = Vector3.left * speed;
    }

	//Speed change message subscription
	public void ChangeSpeed(float newSpeed)
	{
		speed = newSpeed;
	}
}
