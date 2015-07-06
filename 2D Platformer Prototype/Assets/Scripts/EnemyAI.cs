using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public float moveSpeed;
	private double lastInterval, nextInterval; // Last interval end time
	private float moveVelocity;
	private int moveDirection;
	private bool grounded;
	private int health;
	private Animator anim;
	public int enemyType; //0-ground, 1-flying, 2-boss;
	private Vector3 downV3, nextPosition;
	public float minX, maxX, minY, maxY, rangeX, rangeY;


	void Start () {
		rangeX = maxX - minX;
		rangeY = maxY - minY;
		lastInterval = Time.time;
		nextInterval = Time.time + (Random.value * 5 + 1);
		moveDirection = 1;
		anim = GetComponent<Animator>();
		nextPosition = transform.position;
		downV3 = transform.TransformDirection (Vector3.down);
		if (enemyType == 1)
			GetComponent<Rigidbody> ().useGravity = false;
	}

	void Update () {
		switch (enemyType) {
		case 0:
			if (!Physics.Raycast (new Vector3 (transform.position.x + 1, transform.position.y), downV3, 5)) {
				moveDirection = -1;
			}
			if (!Physics.Raycast (new Vector3 (transform.position.x - 1, transform.position.y), downV3, 5)) {
				moveDirection = 1;
			}
			if (Time.time >= nextInterval) {
				Jump (Random.value * 5 + 5);
				nextInterval = Time.time + (Random.value * 5 + 1);
			}
			GetComponent<Rigidbody>().velocity = new Vector3(moveSpeed * moveDirection, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
			break;
		case 1:
			if (Vector3.Distance(transform.position, nextPosition) <= 1){
				Debug.Log("going to next position");
				nextPosition = new Vector3 (minX + Random.value * rangeX, minY + Random.value * rangeY, transform.position.z);
			}
			transform.position = Vector3.Slerp (transform.position, nextPosition, Time.deltaTime * moveSpeed);
			Debug.Log("transform.position = " + transform.position.ToString() + ", nextPosition = " + nextPosition.ToString());
			break;
		case 2:
			break;
		}

//			if (attributes[1]) Shoot (0f + (moveDirection * 90));//
//			if (attributes[2]) Attack(moveDirection);
		//		if(anim.GetBool("Attack")){
		//			anim.SetBool("Attack", false);
		//		}
	}
	public void Jump(float jumpHeight)
	{
		Debug.Log ("Attempting jump");
		GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, jumpHeight, GetComponent<Rigidbody>().velocity.z);  
	}

	public void Attack(int direction){
		anim.SetBool("Attack", true);
	}
	public void Shoot(float angle)
	{

	}
}
