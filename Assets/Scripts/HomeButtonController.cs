using UnityEngine;
using System.Collections;

public class HomeButtonController : MonoBehaviour {
	public GameController game_controller;
	
	void OnMouseDown(){
		game_controller.UnPause();
		game_controller.GotoMainMenu();
	}
}
