using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZiplinePoint : MonoBehaviour
{
	//Vars
	public GameObject endPoint;
	public bool isEnd;

	private RaycastHit hitInfo;
	private Vector3 endPos;

	void Start()
	{
		endPos = new Vector3(endPoint.transform.position.x, endPoint.transform.position.y-1, endPoint.transform.position.z);
	}

	// Update is called once per frame
	void Update()
    {
		if (!isEnd && Physics.Linecast(transform.position, endPoint.transform.position, out hitInfo))
		{
			if(hitInfo.collider.tag == "Player")
				GameObject.FindWithTag("Player").GetComponent<MovementScript>().ZiplineTo(hitInfo.point,endPos,(hitInfo.point.y-endPos.y)/10);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;

		if (!isEnd)
		{
			Gizmos.DrawLine(transform.position, endPoint.transform.position);
		}
	}
}
