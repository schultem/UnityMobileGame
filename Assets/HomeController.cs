using UnityEngine;
using System.Collections;

public class HomeController : MonoBehaviour {
	public GameController game_controller;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Player")){
			game_controller.jumps=0;
			game_controller.next_level+=1;
		}
	}
}
