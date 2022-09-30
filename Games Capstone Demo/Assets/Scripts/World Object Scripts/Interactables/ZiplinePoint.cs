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
            /*RaycastHit[] blockDetect = Physics.BoxCastAll(transform.position, new Vector3(0.5f, 0.5f, 0.5f), (transform.position-endPoint.transform.position).normalized,
                transform.rotation, Vector3.Distance(transform.position, endPoint.transform.position));
            foreach (RaycastHit hit in blockDetect)
            {
                if (hit.collider.tag == "Player")
                    GameObject.FindWithTag("Player").GetComponent<MovementScript>().ZiplineTo(hitInfo.point, endPos, (transform.position.y - endPos.y));
            }*/
                if (Physics.Linecast(transform.position, endPoint.transform.position, out hitInfo, mask))
                {
                    GameObject.FindWithTag("Player").GetComponent<MovementScript>().ZiplineTo(hitInfo.point, endPoint, (transform.position.y - endPos.y));
                }
            }		
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, endPoint.transform.position);
	}
}
