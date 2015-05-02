using UnityEngine;
using System.Collections;

public class TriggerNearHook : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Hook")){
			transform.root.gameObject.SendMessage("SetNearHook", true);
		}
		if (other.CompareTag("Branch")){
			transform.root.gameObject.SendMessage("SetNearBranch", true);
		}
		if (other.CompareTag("Ground")){
			transform.root.gameObject.SendMessage("SetOnGround", true);
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag("Hook")){
			transform.root.gameObject.SendMessage("SetNearHook", false);
		}
		if (other.CompareTag("Branch")){
			transform.root.gameObject.SendMessage("SetNearBranch", false);
		}
		if (other.CompareTag("Ground")){
			transform.root.gameObject.SendMessage("SetOnGround", false);
		}
	}
}
