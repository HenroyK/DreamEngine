using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZiplinePoint : MonoBehaviour
{
	//Vars
	public GameObject endPoint;

	private RaycastHit hitInfo;
	private Vector3 endPos;
    private LayerMask mask;
    void Start()
	{
        mask = LayerMask.GetMask("Player");
        endPos = new Vector3(endPoint.transform.position.x, endPoint.transform.position.y-1, endPoint.transform.position.z);
	}

	// Update is called once per frame
	void Update()
	{
		if (endPoint != null)
		{
            //transform.LookAt(endPoint.transform);
			endPos = new Vector3(endPoint.transform.position.x, endPoint.transform.position.y - 1, endPoint.transform.position.z);
            if (Physics.Linecast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), new Vector3(endPoint.transform.position.x, endPoint.transform.position.y + 1, endPoint.transform.position.z), out hitInfo, mask) || Physics.Linecast(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), new Vector3(endPoint.transform.position.x, endPoint.transform.position.y - 1, endPoint.transform.position.z), out hitInfo, mask))
            {
                GameObject.FindWithTag("Player").GetComponent<MovementScript>().ZiplineTo(hitInfo.point, endPoint, (transform.position.y - endPos.y));
            }
        }		
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), new Vector3(endPoint.transform.position.x, endPoint.transform.position.y + 1, endPoint.transform.position.z));
        Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), new Vector3(endPoint.transform.position.x, endPoint.transform.position.y - 1, endPoint.transform.position.z));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,endPoint.transform.position);
    }
}
