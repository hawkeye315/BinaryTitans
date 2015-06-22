using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	// Movements
	public float moveSpeed;
	public float moveDistance;

	private Vector3 enemyStartPosition;

	// Random jump height
	public float jumpHeight;
<<<<<<< HEAD
	private float moveVelocity;
	// Direction (+ = Right), (- = Left)
	private int moveDirection;
	private bool grounded;

=======

	// Direction (+ = Right), (- = Left)
	private int moveDirection;
	
>>>>>>> origin/master
	private int health;

	// Use this for initialization
	void Start () {
		enemyStartPosition = transform.position;
		moveDirection = 1;
	}
<<<<<<< HEAD

	// Update is called once per frame
=======
		// Update is called once per frame
>>>>>>> origin/master
	void Update () {
		if (transform.position.x <= enemyStartPosition.x)
			moveDirection = 1;
		else if (transform.position.x >= enemyStartPosition.x + moveDistance)
			moveDirection = -1;
		GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed * moveDirection, GetComponent<Rigidbody2D>().velocity.y);
	}
}
