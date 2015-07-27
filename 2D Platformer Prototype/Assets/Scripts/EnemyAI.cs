using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
<<<<<<< HEAD

=======
	
>>>>>>> playerWeapon
	public float moveSpeed; //move speed. ground walkers are less sensitive
	private double nextTriggerInterval, nextMoveInterval; // Last interval end time
	private int moveDirection; //+1 = right, -1 = left
	private bool grounded,trigger,move; //if on ground, if ready to shoot, if ready to move
	private int health; //haven't implemented this yet
	private Animator anim;
	public int enemyType; //0-ground, 1-flying, 2-boss;
	private Vector3 downV3, nextPosition; //next position is for a flier to determine next point in space
<<<<<<< HEAD
	public float minX, maxX, minY, maxY, rangeX, rangeY; //controls area which flying enemy can move 
	private Transform player; //used to locate player for aiming
	private bool visible; //is enemy in camera's view


=======
	//	public float minX, maxX, minY, maxY, rangeX, rangeY; //controls area which flying enemy can move 
	private Transform player; //used to locate player for aiming
	private bool visible; //is enemy in camera's view
	
	
>>>>>>> playerWeapon
	void Start () {
		visible = false;
		trigger = false;
		move = true;
		player = GameObject.FindObjectOfType<Player>().transform;
<<<<<<< HEAD
		rangeX = maxX - minX; //calculate range of X and Y
		rangeY = maxY - minY;
=======
		//		rangeX = maxX - minX; //calculate range of X and Y
		//		rangeY = maxY - minY;
>>>>>>> playerWeapon
		nextTriggerInterval = Time.time + (Random.value * 5 + 1); //randomly pick the next time for shooting between 1 and 6 seconds
		nextMoveInterval = Time.time + (Random.value * 3 + 2); //randomly pick next move between 2 and 5 seconds (2 + rand 3)
		moveDirection = 1;
		anim = GetComponent<Animator>();
		nextPosition = transform.position;
		downV3 = transform.TransformDirection (Vector3.down); //vector for downward raycast. used to detecting edge of platform
		if (enemyType == 1)
			GetComponent<Rigidbody> ().useGravity = false;
	}
<<<<<<< HEAD

=======
	
>>>>>>> playerWeapon
	void Update () {
		Vector3 dir = player.position - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x); //determine angle between Enemy and Player
		if (Time.time >= nextTriggerInterval) {
			trigger = true;
			nextTriggerInterval = Time.time + (Random.value * 5 + 1);
		}
		if (Time.time >= nextMoveInterval) {
			move = true;
			nextMoveInterval = Time.time + (Random.value * 3 + 2);
<<<<<<< HEAD
		}

		switch (enemyType) {
		case 0: //ground walker - Ray casting on downward in front and behind to detect if there is something to walk on
			if (!Physics.Raycast (new Vector3 (transform.position.x + 1, transform.position.y), downV3, 5)) {
				moveDirection = -1;
			}
			if (!Physics.Raycast (new Vector3 (transform.position.x - 1, transform.position.y), downV3, 5)) {
				moveDirection = 1;
			}
			if (trigger) {
				Jump (Random.value * 5 + 5);
				if (visible)
					Shoot (angle);
			}
			GetComponent<Rigidbody>().velocity = new Vector3(moveSpeed * moveDirection, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
			break;
		case 1://hover enemy
//			if (Vector3.Distance(transform.position, nextPosition) <= 1){
			if(player.position.x > transform.position.x) //Flying enemy is always facing player
				moveDirection = 1;
			else
				moveDirection = -1;
			if (move){
					nextPosition = new Vector3 (minX + Random.value * rangeX, minY + Random.value * rangeY, transform.position.z);
			}
			transform.position = Vector3.Slerp (transform.position, nextPosition, Time.deltaTime * moveSpeed);
			if (visible && trigger)
				Shoot (angle);
			break;
		case 2://boss
			break;
		}
		trigger = false; //reset trigger
		move = false; //reset move
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
			if (col.gameObject.transform.position.x < transform.position.x && col.gameObject.tag != "Player") //if collision with something other than player or ground, turn around
				moveDirection = 1;
			else if (col.gameObject.tag != "Player")
				moveDirection = -1;
		}
	}
=======
		}
		
		switch (enemyType) {
		case 0: //ground walker - Ray casting on downward in front and behind to detect if there is something to walk on
			if (!Physics.Raycast (new Vector3 (transform.position.x + 1, transform.position.y), downV3, 5)) {
				moveDirection = -1;
			}
			if (!Physics.Raycast (new Vector3 (transform.position.x - 1, transform.position.y), downV3, 5)) {
				moveDirection = 1;
			}
			if (trigger) {
				Jump (Random.value * 5 + 5);
				if (visible)
					Shoot (angle);
			}
			GetComponent<Rigidbody>().velocity = new Vector3(moveSpeed * moveDirection, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
			break;
		case 1://hover enemy
			//			if (Vector3.Distance(transform.position, nextPosition) <= 1){
			if(player.position.x > transform.position.x) //Flying enemy is always facing player
				moveDirection = 1;
			else
				moveDirection = -1;
			if (move && visible){
				nextPosition = new Vector3 (player.position.x + Random.value * 5 + (-3 * moveDirection), player.position.y + Random.value * 5 + 3, transform.position.z);
			}
			transform.position = Vector3.Slerp (transform.position, nextPosition, Time.deltaTime * moveSpeed);
			if (visible && trigger)
				Shoot (angle);
			break;
		case 2://boss
			break;
		}
		trigger = false; //reset trigger
		move = false; //reset move
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
			if (col.gameObject.transform.position.x < transform.position.x && col.gameObject.tag != "Player") //if collision with something other than player or ground, turn around
				moveDirection = 1;
			else if (col.gameObject.tag != "Player")
				moveDirection = -1;
		}
	}
>>>>>>> playerWeapon
	//below are the controls for determining if the enemy is in view of the camera or not
	void OnBecameInvisible(){
		visible = false;
	}
	void OnBecameVisible(){
		visible = true;
	}
	void OnWillRenderObject(){
		visible = true;
	}
	public void Jump(float jumpHeight)
	{
		GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, jumpHeight, GetComponent<Rigidbody>().velocity.z);  
	}
<<<<<<< HEAD

=======
	
>>>>>>> playerWeapon
	public void Attack(int direction){
		anim.SetBool("Attack", true);
	}
	public void Shoot(float angle)
	{
		GameObject bullet;
		if ((angle < (Mathf.PI/2) && angle > -(Mathf.PI/2)) && moveDirection > 0) {
			bullet = (GameObject)Instantiate (Resources.Load ("Bullet"), new Vector3 (transform.position.x + 2.5f, transform.position.y), Quaternion.Euler (new Vector3 (0, 0, (angle*Mathf.Rad2Deg) + 90)));
			bullet.GetComponent<Bullet> ().scaleX = Mathf.Cos (angle);
			bullet.GetComponent<Bullet> ().scaleY = Mathf.Sin (angle);
		}
		else if ((angle > (Mathf.PI/2) || angle < -(Mathf.PI/2)) && moveDirection < 0) {
			bullet = (GameObject)Instantiate(Resources.Load("Bullet"), new Vector3(transform.position.x - 2.5f, transform.position.y), Quaternion.Euler(new Vector3(0,0,(angle*Mathf.Rad2Deg)+90)));
			bullet.GetComponent<Bullet>().scaleX = Mathf.Cos(angle);
			bullet.GetComponent<Bullet>().scaleY = Mathf.Sin(angle);
		}
	}
}
