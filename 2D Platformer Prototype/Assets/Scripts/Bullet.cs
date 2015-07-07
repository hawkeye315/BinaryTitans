using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	private Transform camera;
	public float scaleX = 1, scaleY = 0, velocity = 20f;
	// Use this for initialization
	void Start () {
		camera = GameObject.FindObjectOfType<CameraController>().transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3((velocity * Time.deltaTime * scaleX) + transform.position.x, (velocity * Time.deltaTime *scaleY) + transform.position.y, transform.position.z);
		if (Mathf.Abs (Vector3.Distance(camera.transform.position,transform.position)) > 50) //40 worked, 50 to be safe
			Destroy (gameObject);
//		Debug.Log ("Distance to bullet from camera= " + Mathf.Abs (Vector3.Distance (camera.transform.position, transform.position)));
	}
	void OnCollisionEnter(Collision col){
		Vector3 dir = col.gameObject.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		Debug.Log("Angle of collision " + angle);

		if (col.gameObject.tag == "Player")
			if (angle > 90 || angle < -90)
				col.gameObject.GetComponent<Player>().HurtPlayer("Left", 10);
			else
				col.gameObject.GetComponent<Player>().HurtPlayer("Right", 10);
		Destroy (gameObject);
	}
}
