using UnityEngine;
using System.Collections;

public class RestartButtonController : MonoBehaviour {
	public GameController game_controller;
	
	void OnMouseDown(){
		game_controller.UnPause();
		game_controller.RestartLevel();
	}
}
