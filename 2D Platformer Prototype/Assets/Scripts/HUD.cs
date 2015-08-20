using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {
	
	public Text txt;
	private int currentScore=0;
	private int currentHealth=100;
	private int currentLives = 3;
	
	// Use this for initialization
	void Start()
	{
		//txt = gameObject.GetComponent<Text>();
		txt.text = "Score : " + currentScore + "   Health : " + currentHealth;
	}
	
	// Update is called once per frame
	void Update () {
		//txt.text="Score : " + currentScore + "   Health : " + currentHealth;
		currentScore = GameObject.FindObjectOfType<GameManager> ().GetScore ();
		currentHealth = GameObject.FindObjectOfType<Player> ().GetHealth ();
		currentLives = GameObject.FindObjectOfType<Player> ().GetLives ();
		txt.text = "Lives : " + currentLives + "\nScore : " + currentScore + "\nHealth : " + currentHealth;
	}
}
