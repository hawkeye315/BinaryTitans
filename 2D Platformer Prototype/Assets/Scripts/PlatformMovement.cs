using UnityEngine;
using System.Collections;

public class PlatformMovement : MonoBehaviour {
	public enum PlatformType {
		Fixed,
		Horizontal,
		Vertical
	}
	public float range, speed;
	public PlatformType platformType;
	private Vector3 startPosition, nextPosition;
	// Use this for initialization
	void Start () {
//		GetComponent<Rigidbody>().velocity = new Vector3(speed, speed, 0);
		startPosition = GetComponent<Rigidbody>().position;
	}
	
	// Update is called once per frame
	void Update () {
		switch (platformType) {
		case PlatformType.Fixed:
			break;
		case PlatformType.Horizontal:
			if (transform.position.x >= startPosition.x + range - 0.1) {
				nextPosition = new Vector3 (startPosition.x, startPosition.y, startPosition.z);
			} else if (transform.position.x <= startPosition.x + 0.1) {
				nextPosition = new Vector3 (startPosition.x + range, startPosition.y, startPosition.z);
			}
			break;
		case PlatformType.Vertical:
			if (transform.position.y >= startPosition.y + range - 0.1) {
				nextPosition = new Vector3 (startPosition.x, startPosition.y, startPosition.z);
			} else if (transform.position.y <= startPosition.y + 0.1) {
				nextPosition = new Vector3 (startPosition.x, startPosition.y + range, startPosition.z);
			}
			break;
		}
		transform.position = Vector3.MoveTowards (transform.position, nextPosition, Time.deltaTime * speed);
	}
}
