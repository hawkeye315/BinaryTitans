using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    
    // Move controls
    public float moveSpeed;
    public float jumpHeight;
    private float moveVelocity;

    // Ground check gameobject under Player
    public Transform groundCheck;
    // Ground Layer
    public LayerMask whatIsGround;
    // Amount of space to check to see if touching the ground
    public float groundCheckRadius;
    // Is the player touching the ground?
    private bool grounded;
    // Is the player allowed to doublejump?
    private bool doubleJumped;

	// Use this for initialization
	void Start () {
	
	}

    // This function checks to see if the Player is touching the ground and
    // sets the ground variable to true if he is.
    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }
	
	// Update is called once per frame
	void Update () {

        // If the player is touching the ground then set the doubleJumped variable to false.
        if (grounded)
            doubleJumped = false;

        // Jump code. If space is pressed and the player is touching the ground then run Jump function.
        if (Input.GetKeyDown(KeyCode.Space))
        {
			if (grounded)
				Jump ();
			else if (!doubleJumped) {
				Jump ();
				doubleJumped = true;
			}
        }
        // Left Movement
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }

        // Right movement
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
	}

    // Jump function.
    public void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpHeight);  
    }
}
