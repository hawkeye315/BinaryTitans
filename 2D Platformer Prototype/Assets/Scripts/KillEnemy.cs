using UnityEngine;
using System.Collections;

public class KillEnemy : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }

	void OnCollisionEnter(Collision col){
		Debug.Log("Collision");
		if(col.gameObject.tag == "Enemy"){
			Destroy (col.gameObject);
		}
	}
}
