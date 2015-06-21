using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

	private GameManager gameManager;

	Text txt;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager>();
		txt = gameObject.GetComponent<Text>(); 
	}

	// Update is called once per frame
	void Update () {
		txt.text="Lives : " + gameManager.getLives() + "\nScore : " + gameManager.getScore() + "\nHealth : " + gameManager.getHealth();
	}
}