using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZiplinePoint : MonoBehaviour
{
	//Vars
	public GameObject endPoint;

	private RaycastHit hitInfo;
	private Vector3 endPos;

	void Start()
	{
		endPos = new Vector3(endPoint.transform.position.x, endPoint.transform.position.y-1, endPoint.transform.position.z);
	}

	// Update is called once per frame
	void Update()
	{
		if (endPoint != null)
		{
			endPos = new Vector3(endPoint.transform.position.x, endPoint.transform.position.y - 1, endPoint.transform.position.z);
			if (Physics.Linecast(transform.position, endPoint.transform.position, out hitInfo))
			{
				if (hitInfo.collider.tag == "Player")
					GameObject.FindWithTag("Player").GetComponent<MovementScript>().ZiplineTo(hitInfo.point, endPos, (transform.position.y - endPos.y));
			}
        }		
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, endPoint.transform.position);
	}
}
