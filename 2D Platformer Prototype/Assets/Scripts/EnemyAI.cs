using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	// Movements
	public float moveSpeed;
	public float moveDistance;

	private Vector3 enemyStartPosition;

	// Random jump height
	public float jumpHeight;

	private float moveVelocity;
	// Direction (+ = Right), (- = Left)
	private int moveDirection;

	// Ground check gameobject under Player
	public Transform groundCheck;
	// Ground Layer
	public LayerMask whatIsGround;
	// Amount of space to check to see if touching the ground
	public float groundCheckRadius;
	// Is the player touching the ground?
	private bool grounded;

	private int health;

	// Use this for initialization
	void Start () {
		enemyStartPosition = transform.position;
		moveDirection = 1;
	}
	

	private void FixedUpdate()
	{
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
	}

	// Update is called once per frame
	void Update () {
		if (transform.position.x <= enemyStartPosition.x)
			moveDirection = 1;
		else if (transform.position.x >= enemyStartPosition.x + moveDistance)
			moveDirection = -1;
		GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed * moveDirection, GetComponent<Rigidbody2D>().velocity.y);
	}
}
