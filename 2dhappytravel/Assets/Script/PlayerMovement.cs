using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;

    private float move;

    public Rigidbody2D rb;
    public Animator animator;

    public bool isGrounded;
    private bool isWalled;
    public bool facingRight = true;
    public Transform groundCheck;
    public Transform wallCheck;
    public float checkRadius;
    public LayerMask whatIsTile;

    private int extraJumps;
    public int extraJumpsValue;

    public Vector3 respawnPoint;
    private bool isWallSliding;
    public float wallSlidingSpeed;
    public float wallJumpingDuration;
    public Vector2 wallJumpingPower;
    private bool wallJumping;
    public bool canMove;

    void Start()
    {
        canMove = true;
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
    }

    void FixedUpdate()
    {
        //Ground Check + Wall Check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsTile);
        isWalled = Physics2D.OverlapCircle(wallCheck.position, checkRadius, whatIsTile);
        
        //Horizontal Movement
        move = Input.GetAxis("Horizontal");
        //Debug.Log(move);
        if (canMove)
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(speed * move, rb.velocity.y);
                animator.SetBool("isGrounded", true);
                animator.SetFloat("Speed", Mathf.Abs(move));
            }
            else
            {
                rb.velocity = new Vector2(speed * move, rb.velocity.y);
                animator.SetBool("isGrounded", false);
            }

            //Flip character
            if (move > 0 && !facingRight)
            {
                Flip();
            }

            if (move < 0 && facingRight)
            {
                Flip();
            }

            WallSlide();
        }
    }

    void Update() 
    {
        //Reset extra jump
        if(isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }

        //Jump + Wall jump control
        if(Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
            extraJumps--;
        } else if(Input.GetButtonDown("Jump") && extraJumps == 0 && isGrounded)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
        } else if(Input.GetButtonDown("Jump") && wallJumping && facingRight != true)
        {
            rb.velocity = new Vector2(-move * wallJumpingPower.x, wallJumpingPower.y);
        }
    }
    
    void Flip()
    {
        //Flip calculation
        transform.Rotate(0, 180, 0);

        facingRight = !facingRight;
    }

    private void WallSlide()
    {
        //Wall slide calculation
        if(isWalled && isGrounded != true && move > 0 || move < 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, wallSlidingSpeed, float.MaxValue));
            wallJumping = true;
            Invoke("StopWallJump", wallJumpingDuration);
        } else {
            isWallSliding = false;
        }
    }

    void StopWallJump()
    {
        wallJumping = false;
    }
}
