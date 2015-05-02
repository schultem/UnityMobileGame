using UnityEngine;
using System.Collections;

public class TriggerOnHook : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Hook")){
			transform.root.gameObject.SendMessage("SetOnHook", true);
		}else if (other.CompareTag("Branch")){
			transform.root.gameObject.SendMessage("SetOnBranch", true);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag("Hook")){
			transform.root.gameObject.SendMessage("SetOnHook", false);
		}else if (other.CompareTag("Branch")){
			transform.root.gameObject.SendMessage("SetOnBranch", false);
		}
	}
}
