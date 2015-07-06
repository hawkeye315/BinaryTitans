using UnityEngine;
using System.Collections;

public class UiManager : MonoBehaviour {

	public Animator MainButtonGroupAnimator;
	public Animator OptionsGroupAnimator;

	//Connected to BtnStartGame
	public void StartGame(){
		Application.LoadLevel("Level1");
	}

	//Connected to BtnQuit
	public void QuitGame(){
		Application.Quit();
	}

	//Connected to BtnOptions
	public void OpenOptions(){
		MainButtonGroupAnimator.SetBool("MainButtonsIsHidden", true);
		OptionsGroupAnimator.SetBool("OptionsIsHidden", false);
	}

	public void BackFromOptions(){
		OptionsGroupAnimator.SetBool("OptionsIsHidden", true);
		MainButtonGroupAnimator.SetBool("MainButtonsIsHidden", false);
	}
}
