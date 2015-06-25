using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	// Movements
	public float moveSpeed;
	public float moveDistance;

	private Vector3 enemyStartPosition;
	private double lastInterval; // Last interval end time
	private double nextInterval;

	// Random jump height
	public float jumpHeight;
	private float moveVelocity;
	// Direction (+ = Right), (- = Left)
	private int moveDirection;
	private bool grounded;
	private int health;

	// Use this for initialization
	void Start () {
		lastInterval = Time.time;
		nextInterval = Time.time + (Random.value * 5 + 3);
		enemyStartPosition = transform.position;
		moveDirection = 1;
	}

	// Update is called once per frame
	void Update () {
		if (Time.time >= nextInterval) {
			Jump ();
			nextInterval = Time.time + (Random.value * 5 + 3);
		}
		if (transform.position.x <= enemyStartPosition.x)
			moveDirection = 1;
		else if (transform.position.x >= enemyStartPosition.x + moveDistance)
			moveDirection = -1;
		GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed * moveDirection, GetComponent<Rigidbody2D>().velocity.y);
	}
	public void Jump()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);  
	}
}
