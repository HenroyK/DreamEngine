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
	private float globalSpeed;

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
		if (valid && transform.position.x > -400)
		{
			float step = Time.deltaTime * speed;
			Vector3 vel = rb.velocity;
			//Move towards point, going to next once close enough
			if (movePoints[pointIndex] != null)
			{
				if (!waiting)
				{
					Vector3 dir = movePoints[pointIndex].GetComponent<Rigidbody>().position - rb.position;
					dir /= Time.fixedDeltaTime;
					dir = Vector3.ClampMagnitude(dir, speed) + (Vector3.left * globalSpeed);
					rb.velocity = dir;
					if (Vector3.Distance(rb.position, movePoints[pointIndex].GetComponent<Rigidbody>().position) < minDistance)
					{
						waiting = true;
						if (pointIndex < movePoints.Length - 1)
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
		else
        {
			this.enabled = false;
        }

	}

	//Speed change message subscription
	public void ChangeSpeed(float newSpeed)
	{
		globalSpeed = newSpeed;
	}

    private void OnDrawGizmos()
	{
		if (this.enabled)
		{
			Gizmos.color = Color.yellow;

			//Draw the radius of being close enough and the path being taken
			for (int i = 0; i < movePoints.Length; i++)
			{
				Vector3 pos = new Vector3(movePoints[i].transform.position.x + 3, movePoints[i].transform.position.y, movePoints[i].transform.position.z);
				Gizmos.DrawSphere(pos, minDistance);
				if (i < movePoints.Length - 1)
					Gizmos.DrawLine(pos, new Vector3(movePoints[i + 1].transform.position.x + 3, movePoints[i + 1].transform.position.y, movePoints[i + 1].transform.position.z));
				else
					Gizmos.DrawLine(pos, new Vector3(movePoints[0].transform.position.x + 3, movePoints[0].transform.position.y, movePoints[0].transform.position.z));
			}
		}
	}
}
