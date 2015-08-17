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
    private bool isMovingLeft;

    public AudioSource[] sounds;
    public AudioSource footSound;
    public AudioSource jumpSound;
    public AudioSource doublejumpSound;
    public AudioSource deathSound;
    public AudioSource playerHitSound;
    public AudioSource enemyDeath;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager>();
		anim = GetComponentInChildren<Animator>();
		playerBody = GetComponent<Rigidbody>();
		lives = 3;
		playerCol = GetComponentInChildren<Collider> ();
        sounds = GetComponents<AudioSource>();
        footSound = sounds[0];
        jumpSound = sounds[1];
        doublejumpSound = sounds[2];
        deathSound = sounds[3];
        playerHitSound = sounds[4];
        enemyDeath = sounds[5];
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
		Ray groundRay = new Ray(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.down);
		Debug.DrawRay(transform.position, Vector3.down * distance);
		if(Physics.Raycast(groundRay, out hit, distance)){
			if(hit.collider.tag == "Ground" || hit.collider.tag == "Platform"){
				grounded = true;
				jumpCount = 0;
			}
			// Debug.Log(hit.collider.tag + " Distance: " + hit.distance);
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
        // Debug.Log ("Jump Count = " + jumpCount);
		// Jump code. If space is pressed and the player is touching the ground then run Jump function.
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (jumpCount < 2) {
				Jump ();
			}
            if (jumpCount == 1)
            {
                doublejumpSound.Play();
            }
			jumpCount += 1;
		}
		
		// Left Movement
		if (Input.GetKey(KeyCode.A))
		{
			transform.eulerAngles = new Vector3(0,180,0);
			MovePlayer (moveSpeed, -1, playerBody.velocity.y +.2f, 1);
            if (!footSound.isPlaying && grounded)
            {
                footSound.Play();
                isMovingLeft = true;
            }
		}
        else
        {
            if (isMovingLeft || !grounded)
            {
                footSound.Pause();
                isMovingLeft = false;
            }
        }
		
		// Right movement
		if (Input.GetKey(KeyCode.D))
		{
			transform.eulerAngles = new Vector3(0,0,0);
			MovePlayer (moveSpeed, 1, playerBody.velocity.y +.2f, 1);
            if (!footSound.isPlaying && grounded)
            {
                footSound.Play();
            }
		}
        else
        {
            if (!isMovingLeft || !grounded)
            {
                footSound.Pause();
            }
        }
		
		if (Input.GetKeyDown(KeyCode.P))
		{
			anim.SetBool("MeleeAttack", true);
		}
//		if (!grounded && transform.parent == null) {
		if (!grounded && !onPlatform) {
			Debug.Log("No Friction");
			playerCol.material.frictionCombine = PhysicMaterialCombine.Minimum;
			playerCol.material.dynamicFriction = 0f;
			playerCol.material.staticFriction = 0f;
		}
		else
		{
            playerCol.material.frictionCombine = PhysicMaterialCombine.Average;
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
                enemyDeath.Play();
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
	}
	//On maintained collision with another object
	void OnCollisionStay(Collision col)
	{
//		Debug.Log (col.transform.position.y+2.46 + " " + transform.position.y);
		if (col.gameObject.tag == "Platform" && col.transform.position.y + 2.46 <= transform.position.y){
			onPlatform = true;
//						Debug.Log(onPlatform + " " + grounded);
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
        jumpSound.Play();
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
            deathSound.Play();
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
