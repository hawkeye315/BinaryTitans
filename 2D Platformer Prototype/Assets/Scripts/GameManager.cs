using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    // Current Checkpoint 
    public GameObject currentCheckpoint;

    // Player controller script, will assign on start
    private PlayerController player;

	// Find the player script in game.
	void Start () {
        player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Respawn Player at current checkpoint assigned.
    public void RespawnPlayer()
    {
        player.transform.position = currentCheckpoint.transform.position;
        Debug.Log("Respawn Player.");
    }
}
