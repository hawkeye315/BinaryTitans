using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    
    // Move controls
    public float moveSpeed;
    public float jumpHeight;
    private float moveVelocity;
	private GameManager gameManager;
    private Animator anim;
	private bool onPlatform = false;

	//Player lives
	public int lives = 3;
	public int health = 100;
	public int score = 0;

	//time since last damage
	private float timeSinceDamage = 0;
	private float invunerableDamageTime = 0.5f;
	public bool takenDamage = true;

    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float groundCheckRadius;
	public bool grounded;
    private int jumpCount;
	private Vector3 lastPos;

	private RaycastHit hit;
	private Rigidbody playerBody;
	private Collider playerCol;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
		playerBody = GetComponent<Rigidbody>();
		lives = 3;
		playerCol = GetComponent<Collider> ();
	}

    // This function checks to see if the Player is touching the ground and
    // sets the ground variable to true if he is.
    //private void FixedUpdate()
    //{
	//	//grounded = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, whatIsGround);
	//}
	// Update is called once per frame
	private void Update () {
		grounded = false; 
		float distance = 2.2f;
		RaycastHit hit;
		Ray groundRay = new Ray(transform.position, Vector3.down);
		Debug.DrawRay(transform.position, Vector3.down * distance);
		if(Physics.Raycast(groundRay, out hit, distance)){
			if(hit.collider.tag == "Ground" || hit.collider.tag == "Platform"){
				grounded = true;
				jumpCount = 0;
			}
//			Debug.Log(hit.collider.tag + " Distance: " + hit.distance);
		}
		if (onPlatform) {
			jumpCount = 0;
		}
		//check if we need to reset some variables from animator
		if (anim.GetBool("MeleeAttack"))
        {
            anim.SetBool("MeleeAttack", false);
        }
		if (anim.GetBool("TakenDamage") && timeSinceDamage > invunerableDamageTime / 2){

			anim.SetBool("TakenDamage", false);
		}
		//Check if the timer for taken damage needs to be updated
		if(takenDamage){
			timeSinceDamage += Time.deltaTime;
			if(timeSinceDamage > invunerableDamageTime){
				timeSinceDamage = 0;
				takenDamage = false;
			}
		}
		//if the player was just hurt, or it's health is < 0 we dont want the player to be able to move so 
		//we just return
		if(takenDamage || health <= 0){
			return;
		}
        // Jump code. If space is pressed and the player is touching the ground then run Jump function.
        if (Input.GetKeyDown(KeyCode.Space))
        {
			if (jumpCount < 2) {
				Jump ();
			}
			jumpCount += 1;
		}

        // Left Movement
        if (Input.GetKey(KeyCode.A))
        {
			transform.eulerAngles = new Vector3(0,180,0);
			MovePlayer (moveSpeed, -1, playerBody.velocity.y +.2f, 1);
        }

        // Right movement
        if (Input.GetKey(KeyCode.D))
        {
			transform.eulerAngles = new Vector3(0,0,0);
			MovePlayer (moveSpeed, 1, playerBody.velocity.y +.2f, 1);
        }

       
        if (Input.GetKeyDown(KeyCode.P))
        {
            anim.SetBool("MeleeAttack", true);
        }
		if (transform.parent != null && transform.parent.tag == "Platform") {
			Vector3 dir = transform.parent.transform.position - transform.position;
			float angle = Mathf.Atan2 (dir.y, dir.x);
//			Debug.Log("Angle: " + angle);
			if (angle > 0.1 || angle < -1.8)
				playerBody.velocity = new Vector3(0, playerBody.velocity.y, playerBody.velocity.z);
		}
//		actualVelocityX = (playerBody.position.x-lastX)/Time.deltaTime;
		Debug.Log ("Grounded: " + grounded + ", onPlatform: " + onPlatform + ", jumpCount: " + jumpCount + ", friction: " + playerCol.material.dynamicFriction);
//		lastX = playerBody.position.x;
		if (!grounded && transform.parent == null) {
			playerCol.material.dynamicFriction = 0f;
			playerCol.material.staticFriction = 0f;
		}
		else
		{
			playerCol.material.dynamicFriction = 0.6f;
			playerCol.material.staticFriction = 0.6f;
		}
	}
	void OnCollisionEnter(Collision col){
		//Determines the angle of the collision
		Vector3 dir = col.gameObject.transform.position - transform.position;
		dir = col.gameObject.transform.InverseTransformDirection(dir);
		float angle = Mathf.Atan2(dir.y, dir.x);
//		Debug.Log("Angle of collision " + angle);
		if (col.gameObject.tag == "Enemy") {
			//if at a downward, vertical angle, destroy enemy, else hurt player
			if (angle <= -(Mathf.PI/3) && angle >= -(2*Mathf.PI/3) && col.gameObject.tag == "Enemy") {
				Destroy (col.gameObject);
				gameManager.ChangeScore (100);
				playerBody.velocity = new Vector3(0, jumpHeight);
			}
			else {
				//if player is to left of enemy, push left, otherwise right
				if (transform.position.x <= col.gameObject.transform.position.x)
					HurtPlayer("Left", 10);
				else
					HurtPlayer("Right", 10);
				ChangeHealth(-10);
				col.rigidbody.velocity = Vector3.zero;
				
			}
		}
		if (col.gameObject.tag == "Platform") {
			lastPos = col.transform.position;
			col.rigidbody.velocity = Vector3.zero;
		}
	}
	//On maintained collision with another object
	void OnCollisionStay(Collision col)
	{
		if (col.gameObject.tag == "Platform"){
			onPlatform = true;
//			Debug.Log(col.transform.);
//			playerBody.position = new Vector3(playerBody.position.x + (col.transform.position.x - lastPos.x), playerBody.position.y + (col.transform.position.y - lastPos.y), playerBody.position.z);
//			lastPos = col.transform.position;
			transform.parent = col.gameObject.transform;
		}
	}
	void OnCollisionExit(Collision col)
	{
		if (col.gameObject.tag == "Platform"){
			onPlatform = false;
//			transform.position = playerBody.position;
			transform.parent = null;
		}
	}
    // Jump function.
    public void Jump()
    {
		if (onPlatform) {
//			transform.parent = null;
		}
		playerBody.velocity = new Vector3(0, jumpHeight);
    }
	public void HurtPlayer(string direction, int damage){
		if (direction == "Left")
			MovePlayer (moveSpeed, -1, moveSpeed, 1);
		else if (direction == "Right")
			MovePlayer (moveSpeed, 1, moveSpeed, 1);
		ChangeHealth (-damage);
	}
    
    // Move function.
	private void MovePlayer(float xMoveSpeed, int xDirection, float yMoveSpeed, int yDirection)
	{
			playerBody.velocity = new Vector3(xMoveSpeed * xDirection, yMoveSpeed * yDirection);
	}

	public void ChangeHealth(int change){
		//If the player has taken damage recently we dont want him to take it again
		if(takenDamage){
			return;
		}
		health += change;
		if (change < 0) {
			anim.SetBool ("TakenDamage", true);
			takenDamage = true;
		}
		if (health <= 0) {
			gameManager.RespawnPlayer();
			takenDamage = true;
		} else if (health > 100)
			health = 100;
	}
	// Getters
	public int GetLives()
	{
		return lives;
	}
	public int GetHealth()
	{
		return health;
	}
}
