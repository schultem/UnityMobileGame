using UnityEngine;
using System.Collections;

public class ResumeButtonController : MonoBehaviour {
	public GameController game_controller;

	void OnMouseDown(){
		game_controller.UnPause();
	}
}
