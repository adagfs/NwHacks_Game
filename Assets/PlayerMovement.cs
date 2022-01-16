using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public static bool canMove;
	public float moveSpeed;
	public float jumpForce;
	public Transform ceilingCheck;
	public Transform groundCheck;
	public LayerMask groundObjects;
	public float checkRadius;
	public int maxJumpCount;

	private Rigidbody2D rb;
	private bool facingRight = true;
	private float moveDirection;
	private bool isJumping = false;
	private bool isGrounded;
	private int jumpCount;
    public Animator animator;

	private void Awake(){
		rb = GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Start () {
		jumpCount = maxJumpCount;
        canMove = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
        // get inputs
        if (canMove)
        {
            ProcessInputs();
        }
        // animate
        Animate();
	}

	private void FixedUpdate() {
		// check if grounded
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);

		if (isGrounded) {
			jumpCount = maxJumpCount;
		}
        // move
        if (canMove)
        {
            Move();
        }
    }

	private void Move()
	{
		rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

		if (isJumping && jumpCount > 0) {
			rb.AddForce(new Vector2(0f, jumpForce));
			jumpCount--;
		}
		isJumping = false;
	}

	private void Animate()
	{
		if (moveDirection > 0 && !facingRight)
		{
			FlipCharacter();
		}
		else if (moveDirection < 0 && facingRight)
		{
			FlipCharacter();
		}
	}

	private void ProcessInputs()
	{
		moveDirection = Input.GetAxis("Horizontal");

		if (Input.GetButtonDown("Jump") && jumpCount > 0) {
			isJumping = true;
		}

        if(transform.position.x == 53)
        {
            animator.SetBool("TouchingBloob", true);
        }
        else
        {
            animator.SetBool("TouchingBloob", false);
        }

        
	}

	private void FlipCharacter() {
		facingRight = !facingRight;
		transform.Rotate(0f, 180f, 0f);
	}
}
