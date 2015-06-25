using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

	private GameManager gameManager;
	
	public Text txt;
	private int currentScore=0;
	private int currentHealth=100;

	// Use this for initialization
    void Start()
    {
        txt = gameObject.GetComponent<Text>();
        txt.text = "Score : " + currentScore + "   Health : " + currentHealth;
        gameManager = FindObjectOfType<GameManager>();
    }

	// Update is called once per frame
	void Update () {
		txt.text="Score : " + currentScore + "   Health : " + currentHealth;
		currentScore = gameManager.getScore();
		currentHealth = gameManager.getHealth();
		txt.text="Lives : " + gameManager.getLives() + "\nScore : " + gameManager.getScore() + "\nHealth : " + gameManager.getHealth();
	}
}
