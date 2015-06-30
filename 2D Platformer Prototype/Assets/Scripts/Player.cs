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

    // Ground check gameobject under Player
    public Transform groundCheck;
    // Ground Layer
    public LayerMask whatIsGround;
    // Amount of space to check to see if touching the ground
    public float groundCheckRadius;
    // Is the player touching the ground?
	public bool grounded;
    // Is the player allowed to doublejump?
    private bool doubleJumped;

	private float distance = 2.2f;
	private RaycastHit hit;
	private Rigidbody playerBody;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
		playerBody = GetComponent<Rigidbody>();
		lives = 3;
        
	}

    // This function checks to see if the Player is touching the ground and
    // sets the ground variable to true if he is.
    //private void FixedUpdate()
    //{
	//	//grounded = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, whatIsGround);
	//}
	// Update is called once per frame
	private void Update () {
		grounded = false; //temp until I fix above
		float distance = 2.2f;
		RaycastHit hit;
		Ray groundRay = new Ray(transform.position, Vector3.down);
		Debug.DrawRay(transform.position, Vector3.down * distance);
		if(Physics.Raycast(groundRay, out hit, distance)){
			if(hit.collider.tag == "Ground"){
				grounded = true;
			}
			if (hit.collider.tag == "Platform"){
				onPlatform = true;
			}
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
			if (grounded || onPlatform) {
				doubleJumped = false;
				Jump ();
			}
			else if (!doubleJumped) {
				Jump ();
				doubleJumped = true;
			}
        }

        // Left Movement
        if (Input.GetKey(KeyCode.A))
        {
			transform.eulerAngles = new Vector3(0,180,0);
			movePlayer (moveSpeed, -1, playerBody.velocity.y +.2f, 1);
        }

        // Right movement
        if (Input.GetKey(KeyCode.D))
        {
			transform.eulerAngles = new Vector3(0,0,0);
			movePlayer (moveSpeed, 1, playerBody.velocity.y +.2f, 1);
        }

       
        if (Input.GetKeyDown(KeyCode.P))
        {
            anim.SetBool("MeleeAttack", true);
        }
	}

	//On maintained collision with another object
	void OnCollisionStay(Collision col)
	{
		//Determines the angle of the collision
		Vector3 dir = col.gameObject.transform.position - transform.position;
		dir = col.gameObject.transform.InverseTransformDirection(dir);
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
//		Debug.Log("Angle of collision " + angle);

		if (col.gameObject.tag == "Enemy") {
			//if at a downward, vertical angle, destroy enemy, else hurt player
			if (angle <= -60 && angle >= -120) {
				Destroy (col.gameObject);
				gameManager.changeScore (100);
			}
			else {
				//if player is to left of enemy, push left, otherwise right
				if (transform.position.x <= col.gameObject.transform.position.x)
					movePlayer (moveSpeed, -1, moveSpeed, 1);
				else
					movePlayer (moveSpeed, 1, moveSpeed, 1);
				changeHealth(-10);

			}
		}
		if (col.gameObject.tag == "Platform"){
			float diffX = col.rigidbody.velocity.x-playerBody.velocity.x;
			playerBody.velocity = new Vector2(playerBody.velocity.x + diffX, playerBody.velocity.y);
		}
	}
	void OnCollisionExit(Collision col)
	{
		if (col.gameObject.tag == "Platform"){
			onPlatform = false;
		}
	}
    // Jump function.
    public void Jump()
    {
        playerBody.velocity = new Vector2(0, jumpHeight);  
    }
    
    // Move function.
	private void movePlayer(float xMoveSpeed, int xDirection, float yMoveSpeed, int yDirection)
	{
		if (onPlatform && xDirection > 0)
			playerBody.velocity = new Vector2(xMoveSpeed + Mathf.Abs(playerBody.velocity.x), yMoveSpeed * yDirection);
		else if (onPlatform && xDirection < 0)
			playerBody.velocity = new Vector2(-(xMoveSpeed + Mathf.Abs(playerBody.velocity.x)), yMoveSpeed * yDirection);
		else
			playerBody.velocity = new Vector2(xMoveSpeed * xDirection, yMoveSpeed * yDirection);
	}

	public void changeHealth(int change){
		//If the player has taken damage recently we dont want him to take it again
		if(takenDamage){
			return;
		}
		anim.SetBool("TakenDamage",true);
		health += change;
		takenDamage = true;

		if (health <= 0) {
			gameManager.RespawnPlayer();
			takenDamage = true;
		} else if (health > 100)
			health = 100;
	}
	// Getters
	public int getLives()
	{
		return lives;
	}


	public int getHealth()
	{
		return health;
	}
}
