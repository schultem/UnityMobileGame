using UnityEngine;
using System.Collections;

public class TriggerOnHook : MonoBehaviour {
	public PlayerController player_controller;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Hook")){
			transform.root.gameObject.SendMessage("SetOnHook", true);
			player_controller.SetOnHook(true);
		}else if (other.CompareTag("Branch")){
			transform.root.gameObject.SendMessage("SetOnBranch", true);
			player_controller.SetOnBranch(true);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag("Hook")){
			player_controller.SetOnHook(false);
		}else if (other.CompareTag("Branch")){
			player_controller.SetOnBranch(false);
		}
	}
}
