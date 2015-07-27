using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	//scale is 0-1 (0-100% of speed on axis)
	public float scaleX = 1, scaleY = 0, velocity = 20f;

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//manually changes the position after each frame. Only rigidbodies have a velocity method.
		transform.position = new Vector3((velocity * Time.deltaTime * scaleX) + transform.position.x, (velocity * Time.deltaTime *scaleY) + transform.position.y, transform.position.z);
	}
	void OnCollisionEnter(Collision col){
		Vector3 dir = col.gameObject.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x);

		if (col.gameObject.tag == "Player") {
			if (angle > (Mathf.PI / 2) || angle < -(Mathf.PI / 2))
				col.gameObject.GetComponent<Player> ().HurtPlayer ("Left", 10);
			else
				col.gameObject.GetComponent<Player> ().HurtPlayer ("Right", 10);
		}
		Destroy (gameObject);
	}
	void OnBecameInvisible(){
		Destroy (gameObject);
	}
}
