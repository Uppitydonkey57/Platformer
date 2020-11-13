using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    #region Setup

    // Components
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;

    ScreenShake screenShake;

    ScreenFreeze screenFreeze;

    // Movement Parameters
    [Header("Basic Movement")]

    public Transform groundCheck;
    bool grounded;

    public float maxSpeed = 7;

    [Header("Jumping")]
    public float jumpTakeOffSpeed = 7;

    public float movementCutoff;

    public float jumpBufferTime;
    private float jumpBuffer;

    public float groundRememberTime;
    private float groundRemember;

    public float extraJumps;
    private float jumps;

    // Movement Dampening
    [Range(0, 0.9f)]
    public float startingDampen;
    [Range(0, 0.9f)]
    public float stoppingDampen;
    [Range(0, 0.9f)]
    public float turningDampen;

    // Dashing Parameters
    [Header("Dashing")]

    public float dashSpeed;

    public float dashDistanceTime;
    float dashDistance;

    public float noGroundStopTime;
    float noGroundStop;

    public int howManyDashes;
    int dashesLeft;

    public bool isDashing;
    bool isDashingDown;

    public float dashCutoff;

    // Mouse Variables
    Vector2 mousePos;

    // Gravity
    float startGravity;

    //Layer Masks
    public LayerMask whatIsGround;

    // Wall Slide Parameters
    [Header("Wall Sliding")]
    public Transform frontCheck;
    public Transform frontCheck2;

    bool isTouchingFront;
    bool isTouchingFront2;

    bool wallSliding;

    float holdOn;
    public float holdOnTime;

    public float wallSlidingSpeed;

    // Wall Jump Parameters
    [Header("Wall Jump")]

    public float xWallForce;
    public float yWallForce;
    [Range(0f, 1f)]public float wallJumpTimeX;
    [Range(0f, 1f)] public float wallJumpTimeY;

    bool wallJumpingX;
    bool wallJumpingY;

    bool jumpFacingRight;

    [Header("Shooting")]
    public GameObject projectile;

    Weapon weapon;

    // Graphics Object
    [Header("Other")]

    public GameObject graphics;

    // Flipping
    bool isFacingRight;

    Vector2 velocity;

    #endregion

    #region Main Functions
    // Use this for initialization
    void Awake()
    {
        screenShake = FindObjectOfType<ScreenShake>();

        screenFreeze = FindObjectOfType<ScreenFreeze>();
        
        animator = graphics.GetComponentInChildren<Animator>();

        rb = GetComponent<Rigidbody2D>();

        startGravity = rb.gravityScale;

        dashesLeft = howManyDashes;

        velocity = rb.velocity;

        jumps = extraJumps;

        weapon = GetComponent<Weapon>();
    }

    void Update()
    {
        velocity = rb.velocity;

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, whatIsGround) != null;
        
        float horizontal = Input.GetAxisRaw("Horizontal");

        bool jump = Input.GetButtonDown("Jump");

        bool jumpUp = Input.GetButtonUp("Jump");

        bool dash = Input.GetMouseButtonDown(0);

        bool fireArrow = Input.GetMouseButtonDown(0);

        Vector2 move = Vector2.zero;

        //Moving the characters
        move.x = horizontal;

        //Dampening
        if (Mathf.Abs(horizontal) < 0.01f)
        {
            move.x *= Mathf.Pow(1f - stoppingDampen, Time.deltaTime * 10f);
        }
        else if (Mathf.Sign(horizontal) != Mathf.Sign(rb.velocity.x))
        {
            move.x *= Mathf.Pow(1f - turningDampen, Time.deltaTime * 10f);
        }
        else
        {
            move.x *= Mathf.Pow(1f - startingDampen, Time.deltaTime * 10f);
        }

        // Using the jump buffer
        if (jump)
        {
            jumpBuffer = jumpBufferTime;
            noGroundStop = noGroundStopTime;
        }
        jumpBuffer -= Time.deltaTime;

        //Fire
        if (fireArrow)
        {
            Shoot();
        }

        //Dash
        if (dash && dashesLeft > 0)
        {
            grounded = false;

            dashDistance = dashDistanceTime;

            noGroundStop = noGroundStopTime;

            Dash();
        }
        noGroundStop -= Time.deltaTime;

        if (grounded)
        {

            if (isDashing && noGroundStop < 0)
            {
                StopDash();
            }

            dashesLeft = howManyDashes;
        }

        // Coyote time
        if (grounded)
        {
            if (noGroundStop < 0)
            {
                jumps = extraJumps;
            }

            groundRemember = groundRememberTime;
        }
        groundRemember -= Time.deltaTime;

        // Initial Jump
        if (jumpBuffer > 0 && groundRemember > 0)
        {
            jumps = extraJumps;
            jumpBuffer = 0;
            velocity.y = jumpTakeOffSpeed;
        } else if (jumpUp)
        {
            // Continues going up if you hold down the button

            if (velocity.y > 0)
            {
                //how quickly the character falls
                velocity.y *= 0.4f;
            }
        }

        //Wall Slide
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, 0.1f, whatIsGround);
        isTouchingFront2 = Physics2D.OverlapCircle(frontCheck2.position, 0.1f, whatIsGround);

        /*Debug.Log(holdOn);

        if (holdOn <= holdOnTime && wallSliding && !wallJumping)
        {
            move.x = 0;
        }

        if (horizontal != (isFacingRight ? 1f : -1f))
        {
            holdOn = 0;
        } else
        {
            holdOn++;
        }*/

        if ((isTouchingFront || isTouchingFront2) && !grounded)
        {
            dashesLeft = howManyDashes;

            wallSliding = true;
        } else
        {
            wallSliding = false;
        }

        if (wallSliding)
        {
            velocity = new Vector2(velocity.x, Mathf.Clamp(velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        //Wall Jumping
        if (jump && wallSliding)
        {
            if (isTouchingFront)
            {
                jumpFacingRight = true;
            } else if (isTouchingFront2)
            {
                jumpFacingRight = false;
            }

            wallJumpingX = true;
            wallJumpingY = true;
            Invoke(nameof(SetWallJumpingXToFalse), wallJumpTimeX);
            Invoke(nameof(SetWallJumpingYToFalse), wallJumpTimeY);
        }

        if (wallJumpingX)
        {
            velocity = new Vector2(xWallForce * ((isFacingRight || isFacingRight && !jumpFacingRight) ? 1 : -1), velocity.y);
        }

        if (wallJumpingY)
        {
            velocity = new Vector2(velocity.x, yWallForce);
        }

        if (jump && jumps > 0 && !grounded && !wallJumpingX && !wallJumpingY)
        {
            velocity.y = jumpTakeOffSpeed;
            jumps--;
        }

        // Flip the sprite depending on the direction
        if (horizontal < 0 && !isFacingRight)
        {
            Flip();
        } else if (horizontal > 0 && isFacingRight)
        {
            Flip();
        }

        // Animation functions that changes moving and jumping parameters
        animator.SetBool("Grounded", grounded);
        animator.SetBool("IsDashing", isDashing);
        animator.SetFloat("VelocityX", Mathf.Abs(horizontal));
        animator.SetFloat("VelocityY", Mathf.Sign(velocity.y));

        //Adding Velocity
        Vector2 changeVelocity = rb.velocity;

        if (!isDashing && !wallJumpingX)
        {
            changeVelocity.x = move.x * maxSpeed;

            rb.velocity = new Vector2(changeVelocity.x, velocity.y);
        }
        else if (isDashing)
        {
            rb.velocity = velocity;

            if (velocity.y < 0 && !isDashingDown)
            {
                StopDash();
            }

            if (dashDistance <= dashCutoff)
            {
                StopDash();
            }

            dashDistance--;
        }
        else if (wallJumpingX || wallJumpingY)
        {
            //changeVelocity = rb.velocity;

            //changeVelocity.x = velocity.x;

            rb.velocity = velocity;
        }
    }
    #endregion

    #region Action Functions

    void Shoot()
    {
        float initialRotation = weapon.firePoint.transform.rotation.z;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDirection = mousePos - (Vector2)weapon.firePoint.transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        weapon.firePoint.transform.rotation = Quaternion.Euler(0, 0, angle);

        weapon.Attack();

        weapon.firePoint.transform.rotation = Quaternion.Euler(0, 0, initialRotation);
    }

    void Dash()
    {
        screenShake.Shake(0.09f, 0.08f);

        isDashing = true;

        if (mousePos.y < rb.position.y)
        {
            isDashingDown = true;
        }

        screenFreeze.Freeze(0.02f);

        dashesLeft--;

        Vector2 lookDirection = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;

        velocity = Vector2.zero;

        Vector2 moveDirection = (mousePos - rb.position).normalized;

        velocity += moveDirection * dashSpeed;

        graphics.transform.rotation = Quaternion.Euler(0, 0, angle);

        //graphics.GetComponent<Rigidbody2D>().rotation = angle;
    }

    void StopDash()
    {
        isDashing = false;
        isDashingDown = false;

        graphics.transform.rotation = Quaternion.identity;

        velocity = Vector2.zero;
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

    }

    void SetWallJumpingXToFalse()
    {
        wallJumpingX = false;
        //jumps = extraJumps;
    }

    void SetWallJumpingYToFalse()
    {
        wallJumpingY = false;
    }

    #endregion

    #region Collision Management

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (noGroundStop < 0)
        {
            StopDash();
        }
    }

    #endregion
}