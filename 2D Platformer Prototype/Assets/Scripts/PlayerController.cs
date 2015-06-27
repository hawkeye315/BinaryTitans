using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    
    // Move controls
    public float moveSpeed;
    public float jumpHeight;
    private float moveVelocity;
	private GameManager gameManager;
    private Animator anim;

    // Ground check gameobject under Player
    public Transform groundCheck;
    // Ground Layer
    public LayerMask whatIsGround;
    // Amount of space to check to see if touching the ground
    public float groundCheckRadius;
    // Is the player touching the ground?
    private bool grounded;
    // Is the player allowed to doublejump?
    private bool doubleJumped;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
        
	}

    // This function checks to see if the Player is touching the ground and
    // sets the ground variable to true if he is.
    private void FixedUpdate()
    {
//        grounded = Physics.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
		grounded = true; //temp until I fix above
    }
	// Update is called once per frame
	void Update () {
        // Jump code. If space is pressed and the player is touching the ground then run Jump function.
        if (Input.GetKeyDown(KeyCode.Space))
        {
			if (grounded) {
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
			movePlayer (moveSpeed, -1, GetComponent<Rigidbody>().velocity.y, 1);
        }

        // Right movement
        if (Input.GetKey(KeyCode.D))
        {
			movePlayer (moveSpeed, 1, GetComponent<Rigidbody>().velocity.y, 1);
        }

        if (anim.GetBool("MeleeAttack"))
        {
            anim.SetBool("MeleeAttack", false);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            anim.SetBool("MeleeAttack", true);
        }
	}

	//On maintained collision with another object
	void OnCollisionStay(Collision col)
	{
		if (col.gameObject.tag == "Enemy") {

			//Determines the angle of the collision
			Vector3 dir = col.gameObject.transform.position - transform.position;
			dir = col.gameObject.transform.InverseTransformDirection(dir);
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

			//if at a downward, vertical angle, destroy enemy, else hurt player
			if (angle <= -60 && angle >= -120) {
				Destroy (col.gameObject);
				Debug.Log("Angle of attack was " + angle);
				gameManager.changeScore (100);
			}
			else {
				gameManager.changeHealth(-10);
				//if player is to left of enemy, push left, otherwise right
				if (transform.position.x <= col.gameObject.transform.position.x)
					movePlayer (moveSpeed, -1, moveSpeed, 1);
				else
					movePlayer (moveSpeed, 1, moveSpeed, 1);
			}
		}
		Debug.Log("Contact with " + col.gameObject.name);
	}

    // Jump function.
    public void Jump()
    {
        GetComponent<Rigidbody>().velocity = new Vector2(0, jumpHeight);  
    }
    
    // Move function.
	private void movePlayer(float xMoveSpeed, int xDirection, float yMoveSpeed, int yDirection)
	{
		GetComponent<Rigidbody>().velocity = new Vector2(xMoveSpeed * xDirection, yMoveSpeed * yDirection);
	}
}
