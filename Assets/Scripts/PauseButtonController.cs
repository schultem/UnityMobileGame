using UnityEngine;
using System.Collections;

public class PauseButtonController : MonoBehaviour {
	public GameController game_controller;

	void OnMouseDown(){
		game_controller.Pause();
	}

	void Start () {
		Vector3 bottom_left_corner = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
		transform.position = new Vector3(bottom_left_corner.x+0.25f, bottom_left_corner.y+0.25f, transform.position.z);
	}
}
