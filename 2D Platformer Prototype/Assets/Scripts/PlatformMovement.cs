using UnityEngine;
using System.Collections;

public class PlatformMovement : MonoBehaviour {
	public float xLeft, xRight, speed;
	private int direction = 1; // positive = Right;
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().velocity = new Vector3(speed, 0, 0);  
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x <= xLeft)
			direction = 1;
		else if (transform.position.x >= xRight)
			direction = -1;
		GetComponent<Rigidbody>().velocity = new Vector3(speed * direction, 0, 0); 
	}
}
