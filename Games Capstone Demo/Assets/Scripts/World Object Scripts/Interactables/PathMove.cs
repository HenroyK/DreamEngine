using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMove : MonoBehaviour
{
	//Variables
	public float speed;	
	public float minDistance;
	public float waitTime;
	public GameObject[] movePoints;

	private Rigidbody rb;
	private int pointIndex = 0;
	private bool waiting = true;
	private float waitTimer = 0;
	private bool valid = false;

    // Start is called before the first frame update
    void Start()
    {
		rb = gameObject.GetComponent<Rigidbody>();
		if (movePoints.Length > 0)
			valid = true;
		else
			Debug.LogWarning("No move points found");
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		if (valid)
		{
			float step = Time.deltaTime * speed;
			Vector3 vel = rb.velocity;

			//Move towards point, going to next once close enough
			if (!waiting)
			{
				Vector3 dir = movePoints[pointIndex].transform.position - rb.position;
				dir /= Time.fixedDeltaTime;
				dir = Vector3.ClampMagnitude(dir, speed);
				rb.velocity = dir;
				if (Vector3.Distance(transform.position, movePoints[pointIndex].transform.position) < minDistance)
				{
					waiting = true;
					if (pointIndex < movePoints.Length-1)
						pointIndex++;
					else
						pointIndex = 0;
				}
			}
			else
			{
				waitTimer += Time.deltaTime;
				if (waitTimer >= waitTime)
				{
					waiting = false;
					waitTimer = 0;
				}
			}
		}

	}
}
