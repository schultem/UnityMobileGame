using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerNearGround : MonoBehaviour {
	
	List<GameObject> others = new List<GameObject>();

	public void Reset(){
		transform.root.gameObject.SendMessage("SetNearGround", (others.Count>0 ? true : false));
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Ground") || other.CompareTag ("Home")) {
			if (!others.Contains(other.gameObject)) {
				others.Add(other.gameObject);
				transform.root.gameObject.SendMessage ("SetNearGround", (others.Count > 0 ? true : false));

			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag("Ground") || other.CompareTag("Home")){
			if (others.Contains(other.gameObject)) {
				others.Remove(other.gameObject);
				transform.root.gameObject.SendMessage("SetNearGround", (others.Count>0 ? true : false));
			}
		}
	}
}
