using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

    public GameManager gameManager;

	// Use this for initialization
	void Start () {
        gameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Any game object that has this script if it has a trigger collider on it
    // if the player collides with it then it will call game manager RespawnPlayer function
    // and set the player to what ever the current checkpoint is assigned to.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            gameManager.RespawnPlayer();
        }
    }
}
