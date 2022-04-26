using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
	//Variables
	public Rigidbody playerRigidbody;
	public Collider playerCollider;
	public float yvelocity;
	public float maxSpeed;
	public float accel;
	public float jumpaccel;
	public float gravityModifier;
	//Control Keys
	public KeyCode jumpKey;
	public KeyCode forwardKey;
	public KeyCode backKey;
	public KeyCode duckKey;
	public KeyCode swapFKey;
	public KeyCode swapBKey;
	//Vals
	public int minDepth;
	public int maxDepth;
	public int curDepth;

	//
	private bool blockDetect = false;
	private RaycastHit blockRaycastHit;
	private bool moveBack = true;
	// Start is called before the first frame update
	void Start()
	{

	}

	void FixedUpdate()
	{
		playerRigidbody.AddForce(Vector3.down * gravityModifier * GetComponent<Rigidbody>().mass);
	}

	// Update is called once per frame
	void Update()
	{
		//move left
		if (Input.GetKey(backKey) && playerRigidbody.velocity.x > maxSpeed * -1)
		{
			playerRigidbody.velocity += Vector3.left * accel;
		}
		//move right
		if (Input.GetKey(forwardKey) && playerRigidbody.velocity.x < maxSpeed)
		{
			playerRigidbody.velocity += Vector3.right * accel;
		}
		//jump if y velocity 0 and spcbar
		if (Input.GetKey(jumpKey) && IsGrounded())
		{
			playerRigidbody.velocity = Vector3.up * jumpaccel;
		}
		//Change depth
		if (Input.GetKeyDown(swapFKey))
		{
			ChangeDepth(-1);
		}
		if (Input.GetKeyDown(swapBKey))
		{
			ChangeDepth(1);
		}

		// stop character if player isn't moving right or left
		if (!Input.GetKey(backKey) && !Input.GetKey(forwardKey))
		{
			Debug.Log("stopped");
			HardStop();
		}
	}

	void HardStop()
	{
		playerRigidbody.velocity = new Vector3(0, playerRigidbody.velocity.y, 0);
	}

	//Change z axis
	void ChangeDepth(int newDepth)
	{
		
		
		blockDetect = Physics.BoxCast(playerCollider.bounds.center, transform.localScale, transform.forward*newDepth, out blockRaycastHit, transform.rotation, 5);

		if(blockDetect)
		{
			//Output the name of the Collider your Box hit
			Debug.Log("Hit : " + blockRaycastHit.collider.name);
		}
		else
        {
			curDepth = Mathf.Clamp(curDepth += newDepth, minDepth, maxDepth);
			//gameObject.layer = curDepth + 7;
			transform.position = new Vector3(transform.position.x, transform.position.y, curDepth * 5);
		}

		
	}
	
	bool IsGrounded()
    {
		LayerMask mask = LayerMask.GetMask(new string[] { "Ground", "Building" });
		Quaternion weirdQuat = new Quaternion();
		weirdQuat.eulerAngles = new Vector3(0, 0, 0);
		if (Physics.CheckBox(playerCollider.bounds.center + new Vector3(0, -1.5f, 0), new Vector3(1, 0.1f, 1),weirdQuat, mask))
		{
			Debug.Log("grounded");
			return true;
		}
		else
		{
			return false;
		}
	}

    private void OnDrawGizmos()
    {
		Gizmos.color = Color.red;
		// *********************************
		// NUMBERS ARE TO BE CHANGED IF MODEL IS UPDATED.
		Gizmos.DrawWireCube(playerCollider.bounds.center + new Vector3(0, -1.5f, 0), new Vector3(2,0.2f,2));

		if(moveBack)
        {
			Gizmos.DrawRay(transform.position, Vector3.forward * 5);

			//Draw a cube at the maximum distance
			Gizmos.DrawWireCube(transform.position + Vector3.forward * 5, transform.localScale);
		}
        else
        {
			Gizmos.DrawRay(transform.position, Vector3.forward * -5);
			//Draw a cube at the maximum distance
			Gizmos.DrawWireCube(transform.position + Vector3.forward * -5, transform.localScale);
		}
        
		

	}
    //void OnCollisionEnter(Collision other)
    //{
    //	if (other.gameObject.tag == "ground")
    //	{
    //		CanJump = true;
    //	}
    //}
}
