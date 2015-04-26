using UnityEngine;
using System.Collections;

public class TriggerNearHook : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Hook")){
			transform.root.gameObject.SendMessage("SetNearHook", true);
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag("Hook")){
			transform.root.gameObject.SendMessage("SetNearHook", false);
		}
	}
}
