using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    // Player controller script, will assign on start
    private PlayerController player;
    public float respawnDelay;
	private float[] checkpoint = {0,3,20,3};
	private int currentCheckpoint = 0;

	//Player lives
	private int lives = 3;
	private int health = 100;
	private int score = 0;
	private Vector3 cameraVector;
	private float[] cameraPosition = new float[3];
	public float forwardCameraBuffer, rearCameraBuffer;

	// Find the player script in game.
	void Start () {
        player = FindObjectOfType<PlayerController>();
		player.transform.position = new Vector3(2, 3, 0);
		lives = 3;
		cameraVector =GameObject.FindGameObjectWithTag("MainCamera").transform.position;
		cameraPosition [0] = player.transform.position.x + rearCameraBuffer;
		cameraPosition [1] = player.transform.position.y;
		cameraPosition [2] = GameObject.FindGameObjectWithTag ("MainCamera").transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		if ((player.transform.position.x - GameObject.FindGameObjectWithTag ("MainCamera").transform.position.x) > forwardCameraBuffer)
			cameraPosition [0] = player.transform.position.x - forwardCameraBuffer;
		else if ((player.transform.position.x - GameObject.FindGameObjectWithTag ("MainCamera").transform.position.x) < -rearCameraBuffer)
			cameraPosition [0] = player.transform.position.x + rearCameraBuffer;
		if ((player.transform.position.y - GameObject.FindGameObjectWithTag ("MainCamera").transform.position.y) > 5)
			cameraPosition [1] = player.transform.position.y - 5;
		else if ((player.transform.position.y - GameObject.FindGameObjectWithTag ("MainCamera").transform.position.y) < 0 && player.transform.position.y > -3)
			cameraPosition [1] = player.transform.position.y;
		cameraPosition [1] += 5;
		SetCameraPosition (cameraPosition);
		if (player.transform.position.x <= -10)
			RespawnPlayer ();
	}

    public void RespawnPlayer()
    {
		cameraPosition [0] = checkpoint[2*currentCheckpoint];
		cameraPosition [1] = checkpoint[2*currentCheckpoint+1];
		SetCameraPosition (cameraPosition);
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
			player.transform.position = new Vector3(checkpoint[2*currentCheckpoint], checkpoint[2*currentCheckpoint+1], 0);
		else {
			player.transform.position = new Vector3(0, 1, 0);
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
	public void SetCameraPosition(float[] array)
	{
		GameObject.FindGameObjectWithTag ("MainCamera").transform.position = new Vector3 (array[0], array[1], array[2]);
	}
}