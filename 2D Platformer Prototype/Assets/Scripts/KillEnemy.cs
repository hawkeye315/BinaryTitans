using UnityEngine;
using System.Collections;

public class KillEnemy : MonoBehaviour {

    // Add Kill enemy sound
    private AudioSource killSound;
	// Use this for initialization
	void Start () {
	    //killSound = GetComponent<AudioSource>
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
