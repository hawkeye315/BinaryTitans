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

	private Animator anim;

	// Use this for initialization
	void Start () {
		lastInterval = Time.time;
		nextInterval = Time.time + (Random.value * 5 + 1);
		enemyStartPosition = transform.position;
		moveDirection = 1;
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
//		if(anim.GetBool("Attack")){
//			anim.SetBool("Attack", false);
//		}

		if (Time.time >= nextInterval) {
			//Debug.Log ("Attempting jump");
			Jump ();
			//Attack();
			nextInterval = Time.time + (Random.value * 5 + 1);
		}
		if (transform.position.x <= enemyStartPosition.x)
			moveDirection = 1;
		else if (transform.position.x >= enemyStartPosition.x + moveDistance)
			moveDirection = -1;
		GetComponent<Rigidbody>().velocity = new Vector3(moveSpeed * moveDirection, GetComponent<Rigidbody>().velocity.y + .3F, GetComponent<Rigidbody>().velocity.z);
	}
	public void Jump()
	{
		//Debug.Log ("Attempting jump");
		GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, jumpHeight, GetComponent<Rigidbody>().velocity.z);  
	}

	public void Attack(){
		anim.SetBool("Attack", true);
	}
}
