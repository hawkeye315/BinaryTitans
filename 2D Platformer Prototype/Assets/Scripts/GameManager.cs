using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    // Current Checkpoint 
    public GameObject currentCheckpoint;

    // Player controller script, will assign on start
    private PlayerController player;
	
	//Player lives
	private int lives;

	// Find the player script in game.
	void Start () {
        player = FindObjectOfType<PlayerController>();
		lives = 3;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Respawn Player at current checkpoint assigned.
    // Once out of lives, respawn is at starting checkpoint. Resets lives to 3.
    public void RespawnPlayer()
    {
		lives -= 1;
		if (lives >= 0)
			player.transform.position = currentCheckpoint.transform.position;
		else {
			player.transform.position = GameObject.Find("Checkpoint").transform.position;
			lives = 3;
		}
        Debug.Log("Respawn Player.");
    }
}
