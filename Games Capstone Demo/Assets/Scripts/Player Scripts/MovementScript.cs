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
    public bool spotLightToggle = false;
    public Vector3 spotLightRotation;
    public float spotLightHeight = 12;

    public float maxSpeed;
    public float minSpeed = 5;
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
    public AudioClip dashReadyClip;
    public AudioClip changeClip;
    public AudioClip respawnClip;
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
    public SpriteRenderer spriteRenderer;
    //
    private bool ziplined = false;
    private bool ignoreZipline = false;
    private Vector3 ziplinePos;
    public float ziplineSpeed;
    private float ziplineRelSpeed = 0;
    private float lerpPos;
    private float lerpTimer;
    public float lerpSpeed;
    private bool currentlyGrounded = true;
    private bool dashed = false;
    private Vector3 swapVel;
    private BlackFade fader;
    private GameObject gameController;
    private float swapPen;
    private bool onMoving = false;
    private GameObject zipPos;

    // Start is called before the first frame update
    void Start()
    {
        if (spotLight != null && spotLightToggle == true)
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
        gameController = GameObject.FindWithTag("GameController");
        fader = gameController.GetComponent<BlackFade>();
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
        //Lerping to the current layer
        if (lerpTimer <= 1)
        {
            lerpTimer += Time.deltaTime * lerpSpeed;
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + swapVel.x, transform.position.y + ((swapVel.y / 100) - swapPen), depth.layerAxis[depth.curDepth]), lerpTimer);
        }
        else
        {
            swapVel.Set(0, 0, 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        stepTimer -= Time.deltaTime;
        currentlyGrounded = IsGrounded();

        if (!ziplined && Input.GetButtonDown("Dash") && (Input.GetAxis("Horizontal") != 0) && !isDashing && currentDashCooldown <= 0)
        {
            animator.SetTrigger("Dash");
            playerRigidbody.useGravity = false;
            audioSource.PlayOneShot(dashClip);
            currentDashDuration = dashDuration;
            currentDashCooldown = dashCooldown;
            //set which direction to dash
            dashDirection = Input.GetAxis("Horizontal");
            //set dashing state
            isDashing = true;
            dashed = true;
        }
        //Tick down dash duration
        currentDashDuration -= Time.deltaTime;
        currentDashCooldown -= Time.deltaTime;
        cooldownBar.GetComponent<CooldownRadialScript>().UpdateRadialBar(currentDashCooldown / dashCooldown);
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
            if (Input.GetAxis("Horizontal") != 0)
            {
                if (Input.GetAxis("Horizontal") > 0)
                    playerRigidbody.velocity = new Vector3(dashSpeed, 0);
                else
                    playerRigidbody.velocity = new Vector3(-dashSpeed, 0);
            }
        }
        else //when not dashing. Avoids conflict with dash movement
        {
            //Movement is different when airstrafing to standing on ground
            //CoyoteTimer or grounded
            if (currentlyGrounded || coyoteTimer > 0)
            {
                // TEST THIS ASAP
                //Move left/right

                if (Input.GetAxis("Horizontal") > 0)
                {
                    playerRigidbody.velocity = new Vector3(
                        Mathf.Clamp(Input.GetAxis("Horizontal") * maxSpeed, minSpeed, maxSpeed), playerRigidbody.velocity.y);
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    float max = maxSpeed + globalSpeed;
                    playerRigidbody.velocity = new Vector3(
                        Mathf.Clamp(Input.GetAxis("Horizontal") * maxSpeed, -max, -minSpeed), playerRigidbody.velocity.y);
                }
                else
                {
                    // Default move velocity
                    playerRigidbody.velocity = new Vector3(Input.GetAxis("Horizontal") * maxSpeed, playerRigidbody.velocity.y);
                }


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
                if (lerpTimer >= 1)
                {
                    if (Input.GetAxis("Swap") > 0)
                    {
                        ChangeDepth(depth.CheckLayer(-1));
                    }
                    else if (Input.GetAxis("Swap") < 0)
                    {
                        ChangeDepth(depth.CheckLayer(1));
                    }
                }
            }
        }
        if (ziplined)
        {
            playerRigidbody.velocity = Vector3.zero;
            ziplinePos = new Vector3(zipPos.transform.position.x, zipPos.transform.position.y - 3, zipPos.transform.position.z);
            //Move towards end point based on base speed and the relative distance vertically from start-end
            transform.position = Vector3.MoveTowards(transform.position, ziplinePos, (ziplineSpeed + ziplineRelSpeed) * Time.deltaTime);
            if (Vector3.Distance(transform.position, ziplinePos) < 1)
                EndZipline();
        }

        //Swap phys material (moving with objects)
        if (jumpTimer <= 0 && !ziplined && Input.GetAxis("Horizontal") == 0 && currentlyGrounded)
        {
            SwapPhysicsMaterial(false);
        }
        else
        {
            SwapPhysicsMaterial(true);
        }
    }

    void LateUpdate()
    {
        //Fade check
        Physics.Raycast(gameObject.transform.position, -gameObject.transform.right,
            out RaycastHit hit, fader.fadeDist, LayerMask.GetMask("Boundary"), UnityEngine.QueryTriggerInteraction.Collide);
        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Boundary"))
        {
            fader.SetFade(gameObject, Vector3.Distance(gameObject.transform.position, hit.point));
        }
        //Animations / Audio
        if (Input.GetAxis("Horizontal") < 0 || (!currentlyGrounded && playerRigidbody.velocity.x < 0))
        {
            spriteRenderer.flipX = true;
            if (Input.GetAxis("Horizontal") > 0)
                spriteRenderer.flipX = false;
        }
        else
            spriteRenderer.flipX = false;
        //Idle
        if (Input.GetAxis("Horizontal") == 0)
        {
            animator.SetBool("Moving", false);
            animator.SetFloat("Horizontal Speed", 1);
            animator.SetTrigger("Idle");
        }
        else
        {
            RunAudio();
            animator.SetBool("Moving", true);
            animator.SetTrigger("Run");
            if (Input.GetAxis("Horizontal") > 0)
                animator.SetFloat("Horizontal Speed", Mathf.Abs(playerRigidbody.velocity.x) / (maxSpeed / 1.1f));
            else if (Input.GetAxis("Horizontal") < 0)
                animator.SetFloat("Horizontal Speed", Mathf.Abs(playerRigidbody.velocity.x) / (maxSpeed * 1.5f));
            else
                animator.SetFloat("Horizontal Speed", 1);
            if (!currentlyGrounded)
                animator.SetFloat("Horizontal Speed", 1);
        }

        if (!currentlyGrounded && playerRigidbody.velocity.y < -1)
            animator.SetTrigger("Landing");
        animator.SetBool("Grounded", currentlyGrounded);
        animator.SetFloat("Vertical Speed", playerRigidbody.velocity.y);
        animator.SetBool("Zipline", ziplined);
        if (currentDashCooldown / dashCooldown <= 0 && dashed == true)
        {
            dashed = false;
            audioSource.PlayOneShot(dashReadyClip);
        }
        animator.SetBool("OnMoving", onMoving);
        onMoving = false;
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
            bool valid = true;
            RaycastHit[] blockDetect = Physics.BoxCastAll(playerCollider.bounds.center, new Vector3(1, 2.2f, 1), transform.forward * (newDepth - depth.curDepth),
                transform.rotation, Mathf.Abs(depth.layerAxis[depth.curDepth] - depth.layerAxis[newDepth]));
            //Check all we hit, invalidating if it does block swapping
            foreach (RaycastHit hit in blockDetect)
            {
                if (hit.collider.gameObject.layer != 0 && hit.collider.gameObject.layer != 3 && hit.collider.tag != "DoesntBlockSwap" && hit.collider.tag != "Player")
                {
                    valid = false;
                    //Debug.Log("Hit: " + hit.collider.name);
                }
                if (hit.collider.tag == "ground" || hit.collider.gameObject.layer == LayerMask.NameToLayer("GroundFloor"))
                {
                    Vector3 dir;
                    float dist;
                    Rigidbody hitRB = hit.collider.GetComponent<Rigidbody>();
                    bool overlapped = Physics.ComputePenetration(
                playerCollider, new Vector3(transform.position.x, transform.position.y + swapVel.y, transform.position.z + (depth.layerAxis[depth.curDepth] - depth.layerAxis[newDepth])), transform.rotation,
                hit.collider, hitRB.position, hitRB.rotation,
                out dir, out dist);
                    if (dist > 0)
                    {
                        swapPen = dist;
                    }
                }
            }
            if (valid)
            {
                ChangeLayer(newDepth);
            }
        }
    }

    //Change to the new layer
    void ChangeLayer(int newDepth)
    {
        audioSource.PlayOneShot(changeClip);
        depth.curDepth = newDepth;
        lerpTimer = 0;
        swapVel = playerRigidbody.velocity / 100;
        //transform.position = new Vector3(transform.position.x, transform.position.y, depth.layerAxis[depth.curDepth]);
    }

    //Check if the player is grounded
    bool IsGrounded()
    {
        LayerMask mask = LayerMask.GetMask(new string[] { "GroundFloor", "Building" });
        Quaternion weirdQuat = new Quaternion();
        weirdQuat.eulerAngles = new Vector3(0, 0, 0);
        //Check for moving platforms (boxcheck wouldnt work so this will do for now)
        if (playerRigidbody.velocity.y < 0)
        {
            Physics.Raycast(gameObject.transform.position, (-transform.up + transform.right) * 15,
                out RaycastHit hitR, 15, mask);
            if (hitR.collider && hitR.collider.gameObject.tag == "Moving")
                onMoving = true;
            Physics.Raycast(gameObject.transform.position, -transform.up * 10,
                out RaycastHit hitM, 10, mask);
            if (hitM.collider && hitM.collider.gameObject.tag == "Moving")
                onMoving = true;
            Physics.Raycast(gameObject.transform.position, (-transform.up + -transform.right) * 15,
                out RaycastHit hitL, 15, mask);
            if (hitL.collider && hitL.collider.gameObject.tag == "Moving")
                onMoving = true;
        }
        else
        {
            onMoving = false;
        }
        if (Physics.CheckBox(playerCollider.bounds.center + new Vector3(0, -2.5f, 0), new Vector3(1, 0.1f, 1), weirdQuat, mask))
        {
            coyoteTimer = coyoteTimeLimit;
            if (ziplined)
                EndZipline();
            ignoreZipline = false;
            return true;
        }
        else
        {
            if (currentlyGrounded && onMoving && playerRigidbody.velocity.y > 0)
            {
                playerRigidbody.velocity += new Vector3(-globalSpeed * 1.4f, 0);
            }
            return false;
        }
    }

    //Change speed to the new global one
    public void UpdateSpeed(float speed)
    {
        globalSpeed = speed;
    }

    //Zipline to a point
    public void ZiplineTo(Vector3 attachPos, GameObject endPos, float relSpeed)
    {
        //Finish dashing before grabbing on
        if (!ignoreZipline && !ziplined && !isDashing && !currentlyGrounded)
        {
            transform.position = new Vector3(attachPos.x, attachPos.y - 3, attachPos.z);
            ziplined = true;
            zipPos = endPos;
            ziplinePos = new Vector3(zipPos.transform.position.x, zipPos.transform.position.y - 3, zipPos.transform.position.z);
            playerRigidbody.useGravity = false;
            ziplineRelSpeed = relSpeed;
        }
    }

    //Finish ziplinging
    public void EndZipline()
    {
        ziplined = false;
        ziplinePos = new Vector3(0, 0, 0);
        playerRigidbody.useGravity = true;
        ignoreZipline = true;
    }

    //Jumping
    void Jump()
    {
        jumpTimer = 0.2f;
        audioSource.PlayOneShot(jumpClip);
        animator.SetTrigger("Jump_Start");
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
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + (playerRigidbody.velocity.y / 100), transform.position.z + (depth.layerAxis[depth.curDepth] - depth.layerAxis[depth.curDepth - 1])));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + (playerRigidbody.velocity.y / 100), transform.position.z + (depth.layerAxis[depth.curDepth] - depth.layerAxis[depth.curDepth + 1])));
    }

    void RunAudio()
    {
        if (stepTimer < 0 && currentlyGrounded)
        {
            stepTimer = stepDelay;
            audioSource.PlayOneShot(runClip);
        }

    }

    //Play an audioclip (requested from external sources)
    public void PlayAudio(string audio)
    {
        switch (audio)
        {
            case "Respawn":
                audioSource.PlayOneShot(respawnClip);
                animator.SetTrigger("Respawn");
                break;
            default:
                Debug.LogWarning("No clip found");
                break;

        }
    }
    void OnCollisionEnter(Collision other)
    {
        //Right bounds stops ziplines
        if (ziplined && other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            EndZipline();
        }
        //Moving platforms stop falling animation
        if (other.gameObject.tag == "Moving")
            onMoving = true;
    }
}
