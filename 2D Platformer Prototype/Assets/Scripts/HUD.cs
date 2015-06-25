using UnityEngine;
<<<<<<< HEAD
﻿using UnityEngine;
=======
>>>>>>> 6a34c751724e856cee7d1d8cf35229e4c6c47c88
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

	private GameManager gameManager;

<<<<<<< HEAD
	public Text txt;
=======
	Text txt;
>>>>>>> 6a34c751724e856cee7d1d8cf35229e4c6c47c88
	private int currentScore=0;
	private int currentHealth=100;

	// Use this for initialization
<<<<<<< HEAD
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
=======
	void Start () {
		gameManager = FindObjectOfType<GameManager>();
		txt = gameObject.GetComponent<Text>(); 
	}

	// Update is called once per frame
	void Update () {
		txt.text="Lives : " + gameManager.getLives() + "\nScore : " + gameManager.getScore() + "\nHealth : " + gameManager.getHealth();
	}
}
>>>>>>> 6a34c751724e856cee7d1d8cf35229e4c6c47c88
