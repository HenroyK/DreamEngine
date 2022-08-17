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
	private int pointIndex = 0;
	private bool waiting = true;
	private float waitTimer = 0;
	private bool valid = false;

    // Start is called before the first frame update
    void Start()
    {
		if (movePoints.Length > 0)
			valid = true;
		else
			Debug.LogWarning("No move points found");
	}

    // Update is called once per frame
    void Update()
    {
		if (valid)
		{
			var step = speed * Time.deltaTime;
			//Move towards point, going to next once close enough
			if (!waiting)
			{
				transform.position = Vector3.MoveTowards(transform.position, movePoints[pointIndex].transform.position, speed);
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
