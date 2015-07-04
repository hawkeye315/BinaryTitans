using UnityEngine;
using System.Collections;

public class UiManager : MonoBehaviour {

	//Connected to BtnStartGame
	public void StartGame(){
		Application.LoadLevel("Level1");
	}

	//Connected to BtnQuit
	public void QuitGame(){
		Application.Quit();
	}
}
