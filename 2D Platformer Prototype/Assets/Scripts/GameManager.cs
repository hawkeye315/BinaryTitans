using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    // Player controller script, will assign on start
    private Player player;
    public float respawnDelay;

	private int score = 0;


	// Find the player script in game.
	void Start () {
        player = FindObjectOfType<Player>();
		player.transform.position = GameObject.FindGameObjectWithTag("PlayerStartPoint").transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		if (player.transform.position.y <= -10) {
			player.changeHealth(- player.health);
		}
		//Debug.Log("Player y= " + player.transform.position.y);

	}

    //public void RespawnPlayer()
    //{
	//	StartCoroutine("RespawnPlayerCo");
    //}

    // Respawn Player at current checkpoint assigned.
    // Once out of lives, respawn is at starting checkpoint. Resets lives to 3.
    //public IEnumerator RespawnPlayerCo()
	public void RespawnPlayer()
    {
		player.lives -= 1;
		player.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        //yield return new WaitForSeconds(respawnDelay);
		if (player.lives >= 0)
			player.transform.position = GameObject.FindGameObjectWithTag("PlayerStartPoint").transform.position; 
		else {
			player.transform.position = GameObject.FindGameObjectWithTag("PlayerStartPoint").transform.position;  
			player.lives = 3;
			score = 0;
		}
        player.health = 100;
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