using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	// Fields
	// Do not set these in the editor, they change anyway
	public int health;
	public int lives;
	// These are fine to change
	public float moveSpeed;
	public float jumpHeight;
	// Private variables
	private float moveVelocity;
	public State currentState;
	// Ground check gameobject under Player
	public Transform groundCheck;
	// Ground Layer
	public LayerMask whatIsGround;
	// Amount of space to check to see if touching the ground
	public float groundCheckRadius;
	// Is the player touching the ground?
	public bool grounded;
	// Is the player moving?
	public bool moving;
	private bool justJumped = false;

	private Animator anim;

	public enum State {
		Idle,
		Jumping,
		DoubleJumping,
		Moving
	}

	public Dictionary <string, KeyCode> controlKeys;

	// Use this for initialization
	void Start () {
		health = 100;
		lives = 3;
		anim = GetComponent<Animator>();
		//set up keys
		controlKeys = new Dictionary<string, KeyCode>();
		controlKeys.Add("moveLeft", KeyCode.A);
		controlKeys.Add("moveRight", KeyCode.D);
		controlKeys.Add("jump", KeyCode.Space);
		controlKeys.Add("attack", KeyCode.P);

		//start it up in idle
		currentState = State.Idle;
	}
	
	// Update is called once per frame
	void Update () {
		switch(currentState)
		{
			case State.Idle:
				//From here we can go to moving, or jumping 
				if(Input.GetKeyDown(controlKeys["moveLeft"])){
					MovePlayerLeft();
					currentState = State.Moving;
				}
				if(Input.GetKeyDown(controlKeys["moveRight"])){
					MovePlayerRight();
					currentState = State.Moving;
				}
				if(Input.GetKeyDown(controlKeys["jump"])){
					Jump();
					currentState = State.Jumping;
					justJumped = true;
				}
				break;
			case State.Jumping:
				//From here we can go to double jumping, idle, moving
				if(grounded && !justJumped){
                    if(moving){
                        currentState = State.Moving;
                    } else {
                        currentState = State.Idle;
                	}
				}
				if(Input.GetKey(controlKeys["moveLeft"])){
					MovePlayerLeft();
				}
				if(Input.GetKey(controlKeys["moveRight"])){
					MovePlayerRight();
				}
                if(Input.GetKeyDown(controlKeys["jump"])){
                    Jump();
                    currentState = State.DoubleJumping;
                }
				justJumped = false;
				break;
			case State.DoubleJumping:
				//From here we can go to idle, moving
				if(grounded){
                    if(moving){
                        currentState = State.Moving;
                    } else {
                        currentState = State.Idle;
                	}
				}
				if(Input.GetKey(controlKeys["moveLeft"])){
					MovePlayerLeft();
				}
				if(Input.GetKey(controlKeys["moveRight"])){
					MovePlayerRight();
				}
				break;
			case State.Moving:
				//From here we can go to idle or jumping
                if(!moving){
                    currentState = State.Idle;
                }

				if(Input.GetKey(controlKeys["moveLeft"])){
					MovePlayerLeft();
				}
				if(Input.GetKey(controlKeys["moveRight"])){
					MovePlayerRight();
				}
				if(Input.GetKeyDown(controlKeys["jump"])){
					Jump();
					currentState = State.Jumping;
				}
                 
				break;
		}
        // Regardles off the current state we should be able to attack
        if (anim.GetBool("MeleeAttack")) {
            anim.SetBool("MeleeAttack", false);
        }
        if (Input.GetKeyDown(KeyCode.P)){
            anim.SetBool("MeleeAttack", true);
        }
	}

	// This function checks to see if the Player is touching the ground and
	// sets the ground variable to true if he is.
	private void FixedUpdate()
	{
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
		moving = (GetComponent<Rigidbody2D>().velocity.x != 0);
	}

	/*
	 * Help methods
	 */

	private void MovePlayerLeft(){
		MovePlayer(moveSpeed, -1, GetComponent<Rigidbody2D>().velocity.y, 1);
	}

	private void MovePlayerRight(){
		MovePlayer(moveSpeed, 1, GetComponent<Rigidbody2D>().velocity.y, 1);
	}

	private void MovePlayer(float xMoveSpeed, int xDirection, float yMoveSpeed, int yDirection) {
		GetComponent<Rigidbody2D>().velocity = new Vector2(xMoveSpeed * xDirection, yMoveSpeed * yDirection);
	}

	private void Jump()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpHeight);  
	}    
}
