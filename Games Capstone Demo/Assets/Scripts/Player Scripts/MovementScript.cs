using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
	//Variables
	public Rigidbody playerRigidbody;
	public Collider playerCollider;
	public PhysicMaterial slipperyMat;
	public PhysicMaterial roughMat;
	public GameObject cooldownBar;
	public GameObject spotLight;
	public Vector3 spotLightRotation;
	public float spotLightHeight = 12;

	public float maxSpeed;
    public float accel;
	public float airStrafeSpeed;
	public float jumpaccel;
	public float jumpTimer;
	public float globalSpeed;
	public float coyoteTimeLimit = 0.1f;
	public float coyoteTimer = 0;
	public float jumpBoost;

	//Sound Stuff
	public AudioSource audioSource;
	public AudioClip runClip;
	public AudioClip jumpClip;
	public AudioClip dashClip;
	public float stepDelay = 0.3f;
	private float stepTimer = 0;
	//Dash variables
	public float dashDuration;
	public float dashCooldown;
	public float dashSpeed;
	private float currentDashCooldown = -1;
	private float currentDashDuration = -1;
	private bool isDashing = false;
	private float dashDirection;

	public DepthBehaviour depth;

	public Animator animator;
	//
	private bool blockDetect = false;
	private RaycastHit blockRaycastHit;
	private bool moveBack = true;
	private bool ziplined = false;
	private bool ignoreZipline = false;
	private Vector3 ziplinePos;
	public float ziplineSpeed;
	private float ziplineRelSpeed = 0;
	private float lerpPos;
	private float lerpTimer;
	public float lerpSpeed;


	// Start is called before the first frame update
	void Start()
	{
		if (spotLight != null)
		{
			Vector3 spotlightSpawn = new Vector3(
				this.transform.position.x, 
				spotLightHeight, 
				this.transform.position.z);

            spotLight = Instantiate(
				spotLight,
                spotlightSpawn, 
				Quaternion.Euler(spotLightRotation));
        }
	}
	
	void FixedUpdate()
	{

		jumpTimer -= Time.fixedDeltaTime;
		if (jumpTimer <= 0)
		{
			jumpTimer = 0;
		}
		else
		{
			//Extend jump
			if (Input.GetButton("Jump"))
			{
				playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, playerRigidbody.velocity.y + jumpBoost);
			}
		}
	}
	// Update is called once per frame
	void Update()
	{
		stepTimer -= Time.deltaTime;

		if (!ziplined && Input.GetButtonDown("Dash") && (playerRigidbody.velocity.x != 0) && !isDashing && currentDashCooldown <= 0)
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
		cooldownBar.GetComponent<CooldownRadialScript>().UpdateRadialBar(currentDashCooldown/dashCooldown);
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
				RunAudio();
				//Move left/right
				playerRigidbody.velocity = new Vector3(Input.GetAxis("Horizontal") * maxSpeed, playerRigidbody.velocity.y);
				//Debug.Log(Input.GetAxis("Horizontal"));
                if (playerRigidbody.velocity.x < 0)
                {
					playerRigidbody.velocity += new Vector3(-globalSpeed, 0);
				}

				//Jump
				if (Input.GetButtonDown("Jump"))
				{
					Jump();
                    //Debug.Log("normal jump 1");
                }
			}
			else //Airstrafe movement code.
			{
				if (!ziplined)
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
				else
				{
					//Jump
					if (Input.GetButtonDown("Jump"))
					{
						Jump();
						//Debug.Log("normal jump 2");
					}
				}
			}

			//Change depth
			if (!ziplined)
			{
				if (Input.GetButtonDown("SwapForward"))
				{
					ChangeDepth(depth.CheckLayer(-1));
				}
				if (Input.GetButtonDown("SwapBackward"))
				{
					ChangeDepth(depth.CheckLayer(1));
				}
			}
		}
		if(ziplined)
		{
			playerRigidbody.velocity = Vector3.zero;
			//Move towards end point based on base speed and the relative distance vertically from start-end
			transform.position = Vector3.MoveTowards(transform.position, ziplinePos, (ziplineSpeed + ziplineRelSpeed) * Time.deltaTime);
			if (Vector3.Distance(transform.position, ziplinePos) < 5)
				EndZipline();
		}
		//Lerping to the current layer
		if (lerpTimer < 1)
		{
			lerpTimer += Time.deltaTime*lerpSpeed;
			transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, depth.layerAxis[depth.curDepth]), lerpTimer);
		}

		//Swap phys material (moving with objects)
		if (jumpTimer <= 0 && !ziplined && Input.GetAxis("Horizontal") == 0 && IsGrounded())
		{
			SwapPhysicsMaterial(false);
		}
		else
		{
			SwapPhysicsMaterial(true);
		}
	}

	// Change the physics material of the collider
	void SwapPhysicsMaterial(bool moving)
    {
		//Debug.Log(moving);
		if (moving || ziplined)
        {
			playerCollider.material = slipperyMat;
		}
		else
        {
			playerCollider.material = roughMat;
		}
	}

	//Change z axis
	void ChangeDepth(int newDepth)
	{
		if (newDepth != -1)
		{
			blockDetect = Physics.BoxCast(playerCollider.bounds.center, transform.localScale, transform.forward * (newDepth - depth.curDepth), 
				out blockRaycastHit, transform.rotation,Mathf.Abs(depth.layerAxis[depth.curDepth]-depth.layerAxis[newDepth]));
			if (blockDetect)
			{
				if(blockRaycastHit.collider.tag == "DoesntBlockSwap")
				{
					ChangeLayer(newDepth);
					Debug.Log("Hit : " + blockRaycastHit.collider.name);
				}
				else
				{
					//Output the name of the Collider your Box hit
					Debug.Log("Hit : " + blockRaycastHit.collider.name);
				}
			}
			else
			{
				ChangeLayer(newDepth);
			}
		}
	}

	//Change to the new layer
	void ChangeLayer(int newDepth)
	{
		audioSource.volume = 1;
		audioSource.PlayOneShot(jumpClip);
		depth.curDepth = newDepth;
		lerpTimer = 0;
		//transform.position = new Vector3(transform.position.x, transform.position.y, depth.layerAxis[depth.curDepth]);
	}

	//Check if the player is grounded
	bool IsGrounded()
    {
		LayerMask mask = LayerMask.GetMask(new string[] { "GroundFloor", "Building" });
		Quaternion weirdQuat = new Quaternion();
		weirdQuat.eulerAngles = new Vector3(0, 0, 0);
		if (Physics.CheckBox(playerCollider.bounds.center + new Vector3(0, -2.5f, 0), new Vector3(1, 0.1f, 1), weirdQuat, mask))
		{
			coyoteTimer = coyoteTimeLimit;
			animator.SetTrigger("Land");
			//Debug.Log("Is Grounded");
			if(ziplined)
				EndZipline();
			ignoreZipline = false;
			return true;
		}
		else
		{
			return false;
		}
	}

	//Change speed to the new global one
	public void UpdateSpeed(float speed)
    {
		globalSpeed = speed;
    }

	//Zipline to a point
	public void ZiplineTo(Vector3 attachPos,Vector3 endPos, float relSpeed)
	{
		//Finish dashing before grabbing on
		if (!ignoreZipline && !ziplined && !isDashing)
		{
			transform.position = new Vector3(attachPos.x, attachPos.y - 1, attachPos.z);
			ziplined = true;
			ziplinePos = new Vector3(endPos.x, endPos.y-1, endPos.z);
			playerRigidbody.useGravity = false;
			ziplineRelSpeed = relSpeed;
		}
	}

	//Finish ziplinging
	public void EndZipline()
	{
		ziplined = false;
		ziplinePos = new Vector3(0,0,0);
		playerRigidbody.useGravity = true;
		ignoreZipline = true;
	}

	//Jumping
	void Jump()
	{
		jumpTimer = 0.2f;
		audioSource.volume = 1;
		audioSource.PlayOneShot(jumpClip);
		animator.SetTrigger("Jump");
		playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, jumpaccel);
		if (ziplined)
		{
			//Jump forward / back
			playerRigidbody.velocity = new Vector3(Input.GetAxis("Horizontal") * maxSpeed, playerRigidbody.velocity.y);
			EndZipline();
		}
	}

	private void OnDrawGizmos()
    {
		Gizmos.color = Color.red;
		// *********************************
		// NUMBERS ARE TO BE CHANGED IF MODEL IS UPDATED.
		Gizmos.DrawWireCube(playerCollider.bounds.center + new Vector3(0, -2.5f, 0), new Vector3(2,0.2f,2));

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

	void RunAudio()
    {
		audioSource.volume = 0.3f;
		if (stepTimer < 0)
        {
			stepTimer = stepDelay;
			audioSource.PlayOneShot(runClip);
        }
		
	}
    void OnCollisionEnter(Collision other)
    {
    	if (ziplined && other.gameObject.tag == "Wall")
    	{
			EndZipline();
    	}
    }
}
