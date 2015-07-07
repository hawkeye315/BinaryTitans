using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public float moveSpeed;
	private double nextInterval; // Last interval end time
	private int moveDirection;
	private bool grounded;
	private int health;
	private Animator anim;
	public int enemyType; //0-ground, 1-flying, 2-boss;
	private Vector3 downV3, nextPosition;
	public float minX, maxX, minY, maxY, rangeX, rangeY;
	private Transform player;


	void Start () {
		player = GameObject.FindObjectOfType<Player>().transform;
		rangeX = maxX - minX;
		rangeY = maxY - minY;
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
			Vector3 dir = player.position - transform.position;
//			dir = player.InverseTransformDirection(dir);
			float angle2 = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			if (!Physics.Raycast (new Vector3 (transform.position.x + 1, transform.position.y), downV3, 5)) {
				moveDirection = -1;
			}
			if (!Physics.Raycast (new Vector3 (transform.position.x - 1, transform.position.y), downV3, 5)) {
				moveDirection = 1;
			}
			if (Time.time >= nextInterval) {
				Jump (Random.value * 5 + 5);
				nextInterval = Time.time + (Random.value * 5 + 1);
				if (Mathf.Abs(dir.x) <= 20)
					Shoot (angle2);
			}
			GetComponent<Rigidbody>().velocity = new Vector3(moveSpeed * moveDirection, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
			break;
		case 1:
			if (Vector3.Distance(transform.position, nextPosition) <= 1){
				nextPosition = new Vector3 (minX + Random.value * rangeX, minY + Random.value * rangeY, transform.position.z);
				if(nextPosition.x - transform.position.x >= 0)
					moveDirection = 1;
				else
					moveDirection = -1;
			}
			transform.position = Vector3.Slerp (transform.position, nextPosition, Time.deltaTime * moveSpeed);
			break;
		case 2:
			break;
		}
		if (moveDirection >= 0)
			transform.eulerAngles = new Vector3(0,0,0);
		else
			transform.eulerAngles = new Vector3(0,180,0);

//			if (attributes[1]) Shoot (0f + (moveDirection * 90));//
//			if (attributes[2]) Attack(moveDirection);
		//		if(anim.GetBool("Attack")){
		//			anim.SetBool("Attack", false);
		//		}
	}
	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag != "Ground") {
			if (col.gameObject.transform.position.x < transform.position.x && col.gameObject.tag != "Player")
				moveDirection = 1;
			else if (col.gameObject.tag != "Player")
				moveDirection = -1;
		}
	}
	public void Jump(float jumpHeight)
	{
		GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, jumpHeight, GetComponent<Rigidbody>().velocity.z);  
	}

	public void Attack(int direction){
		anim.SetBool("Attack", true);
	}
	public void Shoot(float angle)
	{
		GameObject bullet;
		if ((angle < 90 && angle > -90) && moveDirection > 0) {
			bullet = (GameObject)Instantiate (Resources.Load ("Bullet"), new Vector3 (transform.position.x + 2.5f, transform.position.y), Quaternion.Euler (new Vector3 (0, 0, angle + 90)));
			bullet.GetComponent<Bullet> ().scaleX = Mathf.Cos (angle * Mathf.PI / 180);
			bullet.GetComponent<Bullet> ().scaleY = Mathf.Sin (angle * Mathf.PI / 180);
		}
		else if ((angle > 90 || angle < -90) && moveDirection < 0) {
			bullet = (GameObject)Instantiate(Resources.Load("Bullet"), new Vector3(transform.position.x - 2.5f, transform.position.y), Quaternion.Euler(new Vector3(0,0,angle+90)));
			bullet.GetComponent<Bullet>().scaleX = Mathf.Cos(angle * Mathf.PI / 180);
			bullet.GetComponent<Bullet>().scaleY = Mathf.Sin(angle * Mathf.PI / 180);
		}
	}
}
