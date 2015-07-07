using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

	private GameManager gameManager;
	private Player player;	
	public Text txt;
	private int currentScore=0;
	private int currentHealth=100;

	// Use this for initialization
    void Start()
    {
        //txt = gameObject.GetComponent<Text>();
        txt.text = "Score : " + currentScore + "   Health : " + currentHealth;
        gameManager = FindObjectOfType<GameManager>();
		player = FindObjectOfType<Player>();
    }

	// Update is called once per frame
	void Update () {
		//txt.text="Score : " + currentScore + "   Health : " + currentHealth;
		currentScore = gameManager.GetScore();
		currentHealth = player.GetHealth();
		txt.text="Lives : " + player.GetLives() + "\nScore : " + gameManager.GetScore() + "\nHealth : " + player.GetHealth();
	}
}
