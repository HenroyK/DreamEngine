using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
	//Variables
	public Rigidbody playerRigidbody;
	public Collider playerCollider;
	public float maxSpeed;
    public float accel;
	public float airStrafeSpeed;
	public float jumpaccel;
	public float gravityModifier;
	
	public float globalSpeed;

	public float coyoteTimeLimit = 0.1f;
	public float coyoteTimer = 0;
	////Control Keys
	//public KeyCode jumpKey;
	//public KeyCode forwardKey;
	//public KeyCode backKey;
	//public KeyCode duckKey;
	//public KeyCode swapFKey;
	//public KeyCode swapBKey;

	//Dash variables
	public float dashDuration;
	public float dashCooldown;
	public float dashSpeed;
	private float currentDashCooldown = -1;
	private float currentDashDuration = -1;
	private bool isDashing = false;
	private float dashDirection;

	//Vals
	public int minDepth;
	public int maxDepth;
	public int curDepth;

	public Animator animator;
	//
	private bool blockDetect = false;
	private RaycastHit blockRaycastHit;
	private bool moveBack = true;
	// Start is called before the first frame update
	void Start()
	{

	}

	//removed gravity code and just used the gravity part of the unity physics engine. Should be the same thing.

	//void FixedUpdate()
	//{
	//	playerRigidbody.AddForce(Vector3.down * gravityModifier * GetComponent<Rigidbody>().mass);
	//}

	// Update is called once per frame
	void Update()
	{
		
		if (Input.GetButtonDown("Dash") && (playerRigidbody.velocity.x != 0) && !isDashing && currentDashCooldown <= 0)
		{
			playerRigidbody.useGravity = false;
			//SoundManagerScript.PlaySound("Dash");
			currentDashDuration = dashDuration;
			currentDashCooldown = dashCooldown;
			//set which direction to dash
			dashDirection = Input.GetAxis("Horizontal");
			//set dashing state
			isDashing = true;
			//animation.DashAnimation((Direction)Mathf.Round(Input.GetAxis("Horizontal")));
		}
		//Tick down dash duration
		currentDashDuration -= Time.deltaTime;
		currentDashCooldown -= Time.deltaTime;

		//Coyote Timer
		coyoteTimer -= Time.deltaTime;

		//if dash duration is over, stop dashing
		if (currentDashDuration < 0 && isDashing)
		{
			playerRigidbody.velocity = new Vector3(0, 0, 0);
			isDashing = false;
			playerRigidbody.useGravity = true;
			
		}

		if (isDashing)  //when dashing.
		{
			
			//When dashing, move in a set direction
			if (dashDirection > 0)
			{
				playerRigidbody.velocity = new Vector3(dashSpeed, 0);
			}
			else
			{
				playerRigidbody.velocity = new Vector3(-dashSpeed, 0);
			}
		}
		else //when not dashing. Avoids conflict with dash movement
		{
			//Movement is different when airstrafing to standing on ground
			//CoyoteTimer or grounded
			if (IsGrounded() || coyoteTimer > 0)
			{
				//Move left/right

				playerRigidbody.velocity = new Vector3(Input.GetAxis("Horizontal") * maxSpeed, playerRigidbody.velocity.y);
                if (playerRigidbody.velocity.x < 0)
                {
					playerRigidbody.velocity += new Vector3(-globalSpeed, 0);
				}

				//Jump
				if (Input.GetButtonDown("Jump"))
				{
					//SoundManagerScript.PlaySound("Jump");
					animator.SetTrigger("Jump");
					playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, jumpaccel);
				}
			}
			else //Airstrafe movement code.
			{
				
				//Checks if the player is trying to go left or right, checks that they are below half of max speed then modifies velocity by airStrafeSpeed. 
				if (Input.GetAxis("Horizontal") > 0 && playerRigidbody.velocity.x < maxSpeed / 2)
				{
					playerRigidbody.velocity += new Vector3(Input.GetAxis("Horizontal") * airStrafeSpeed, 0);
				}
				if (Input.GetAxis("Horizontal") < 0 && playerRigidbody.velocity.x > -1 * maxSpeed)
				{
					playerRigidbody.velocity += new Vector3(Input.GetAxis("Horizontal") * airStrafeSpeed, 0);
				}
			}

			//Change depth
			if (Input.GetButtonDown("SwapForward"))
			{
				ChangeDepth(-1);
			}
			if (Input.GetButtonDown("SwapBackward"))
			{
				ChangeDepth(1);
			}

		}

		//// stop character if player isn't moving right or left
		//if (!Input.GetKey(backKey) && !Input.GetKey(forwardKey))
		//{
		//	//Debug.Log("stopped");
		//	HardStop();
		//}
	}

	//void HardStop()
	//{
	//	playerRigidbody.velocity = new Vector3(0, playerRigidbody.velocity.y, 0);
	//}

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
			coyoteTimer = coyoteTimeLimit;
			animator.SetTrigger("Land");
			return true;
		}
		else
		{
			return false;
		}
	}

	public void UpdateSpeed(float speed)
    {
		globalSpeed = speed;
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
