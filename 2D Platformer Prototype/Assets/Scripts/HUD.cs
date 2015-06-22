<<<<<<< HEAD
using UnityEngine;
=======
﻿using UnityEngine;
>>>>>>> origin/master
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

	private GameManager gameManager;

	Text txt;
<<<<<<< HEAD
	private int currentScore=0;
	private int currentHealth=100;

	// Use this for initialization
	void Start () {
		txt = gameObject.GetComponent<Text>(); 
		txt.text="Score : " + currentScore + "   Health : " + currentHealth;
		gameManager = FindObjectOfType<GameManager>();
=======

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager>();
		txt = gameObject.GetComponent<Text>(); 
>>>>>>> origin/master
	}

	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
		txt.text="Score : " + currentScore + "   Health : " + currentHealth;
		currentScore = gameManager.getScore();
		currentHealth = gameManager.getHealth();
	}
}
=======
		txt.text="Lives : " + gameManager.getLives() + "\nScore : " + gameManager.getScore() + "\nHealth : " + gameManager.getHealth();
	}
}
>>>>>>> origin/master
