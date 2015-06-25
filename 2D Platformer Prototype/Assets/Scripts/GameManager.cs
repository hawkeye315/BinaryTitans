using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    // Current Checkpoint 
    public GameObject currentCheckpoint;

    // Player controller script, will assign on start
    private PlayerController player;
    public float respawnDelay;

	//Player lives
	private int lives = 3;
	private int health = 100;
	private int score = 0;

	// Find the player script in game.
	void Start () {
        player = FindObjectOfType<PlayerController>();
		lives = 3;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RespawnPlayer()
    {
        StartCoroutine("RespawnPlayerCo");
    }

    // Respawn Player at current checkpoint assigned.
    // Once out of lives, respawn is at starting checkpoint. Resets lives to 3.
    public IEnumerator RespawnPlayerCo()
    {
        health = 100;
		lives -= 1;
        yield return new WaitForSeconds(respawnDelay);
		if (lives >= 0)
			player.transform.position = currentCheckpoint.transform.position;
		else {
			player.transform.position = GameObject.Find("Checkpoint").transform.position;
			lives = 3;
			score = 0;
		}
        Debug.Log("Respawn Player.");
    }

	public int getScore()
	{
		return score;
	}

	public void changeScore(int change){
		score += change;
	}

	public int getHealth()
	{
		return health;
	}

	public void changeHealth(int change){
		health += change;
		if (health <= 0) {
			health = 100;
			RespawnPlayer ();
		} else if (health > 100)
			health = 100;
	}
	public int getLives()
	{
		return lives;
	}
<<<<<<< HEAD
}
=======
}
>>>>>>> 6a34c751724e856cee7d1d8cf35229e4c6c47c88
