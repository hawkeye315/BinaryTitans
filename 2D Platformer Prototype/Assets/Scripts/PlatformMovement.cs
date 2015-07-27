using UnityEngine;
using System.Collections;

public class PlatformMovement : MonoBehaviour {
	public float range, speed;
	public int type;
	public int xDirection = 0, yDirection = 0;
	private Vector3 startPosition, nextPosition;
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().velocity = new Vector3(speed, speed, 0);
		startPosition = GetComponent<Rigidbody>().position;
	}
	
	// Update is called once per frame
	void Update () {
		if (type == 0) {
			if (transform.position.x >= startPosition.x + range - 0.1){
				nextPosition = new Vector3 (startPosition.x, startPosition.y, startPosition.z);
				xDirection = -1;
			}
			else if (transform.position.x <= startPosition.x + 0.1) {
				nextPosition = new Vector3 (startPosition.x + range, startPosition.y, startPosition.z);
				xDirection = 1;
			}
		}
		if (type == 1) {
			if (transform.position.y >= startPosition.y + range - 0.1){
				nextPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z);
				yDirection = -1;
			}
			else if (transform.position.y <= startPosition.y + 0.1){
				nextPosition = new Vector3(startPosition.x, startPosition.y + range, startPosition.z);
				yDirection = 1;
			}
		}
		transform.position = Vector3.MoveTowards (transform.position, nextPosition, Time.deltaTime * speed);
	}
}
