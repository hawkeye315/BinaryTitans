using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    // Player controller script, will assign on start
    private Player player;
	private CameraController camera;
    public float respawnDelay;
	private float[] checkpoint = {0,3,20,3};
	private int currentCheckpoint = 0;

	private int score = 0;


	// Find the player script in game.
	void Start () {
        player = FindObjectOfType<Player>();
		player.transform.position = new Vector3(2, 3, 0);
		camera = FindObjectOfType<CameraController>();
	}
	
	// Update is called once per frame
	void Update () {

		if (player.transform.position.y <= -10) {
			RespawnPlayer ();
		}
		Debug.Log("Player y= " + player.transform.position.y);

	}

    public void RespawnPlayer()
    {
		float[] cameraPosition = new float[3];
		cameraPosition [0] = checkpoint[2*currentCheckpoint];
		cameraPosition [1] = checkpoint[2*currentCheckpoint+1];
		camera.SetCameraPosition (cameraPosition);
		StartCoroutine("RespawnPlayerCo");
    }

    // Respawn Player at current checkpoint assigned.
    // Once out of lives, respawn is at starting checkpoint. Resets lives to 3.
    public IEnumerator RespawnPlayerCo()
    {
        player.health = 100;
		player.lives -= 1;
        yield return new WaitForSeconds(respawnDelay);
		if (player.lives >= 0)
			player.transform.position = new Vector3(checkpoint[2*currentCheckpoint], checkpoint[2*currentCheckpoint+1], 0);
		else {
			player.transform.position = new Vector3(0, 3, 0);
			player.lives = 3;
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




}