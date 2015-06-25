using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Fields
	// Do not set these in the editor, they change anyway
	public int health;
	public int lives;
	// These are fine to change
	public float moveSpeed;
	public float jumpHeight;
	// Private variables
	private float moveVelocity;
	private State currentState;

	private Animator anim;

	public enum ControlKeys {
		Jump = KeyCode.Space,
		Move_Left = KeyCode.A,
		Move_Right = KeyCode.D,
		Attack = KeyCode.P
	}

	public enum State {
		Idle,
		Jumping,
		DoubleJumping,
		Moving
	}

	// Use this for initialization
	void Start () {
		health = 100;
		lives = 3;

	
	}
	
	// Update is called once per frame
	void Update () {
		switch(currentState){
			case State.Idle:
				//From here we can go to moving, or jumping 
				break;
			case State.Jumping:
				//From here we can go to double jumping, idle, moving
				break;
			case State.DoubleJumping:
				//From here we can go to idle, moving
				break;
			case State.Moving:
				//From here we can go to idle or jumping
				break;
		}
	}
}
