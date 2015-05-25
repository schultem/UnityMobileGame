using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerNearFeature : MonoBehaviour {
	
	List<GameObject> ground_features = new List<GameObject>();

	void OnTriggerEnter2D(Collider2D other) {
		GameObject go = other.gameObject;
		if (other.CompareTag ("Ground")) {
			if (!ground_features.Contains (go)) {
				ground_features.Add (go);
			}
			transform.root.gameObject.SendMessage ("SetOnGround", true);
		} else if (other.CompareTag ("Hook")) {
			transform.root.gameObject.SendMessage ("SetNearHook", true);
		} else if (other.CompareTag ("Branch")) {
			transform.root.gameObject.SendMessage ("SetNearBranch", true);
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag ("Ground")) {
			ground_features.Remove (other.gameObject);
			if (ground_features.Count > 0) {
				transform.root.gameObject.SendMessage ("SetOnGround", true);
			} else if (ground_features.Count == 0) {
				transform.root.gameObject.SendMessage ("SetOnGround", false);
			} else {
				ground_features = new List<GameObject> ();
				transform.root.gameObject.SendMessage ("SetOnGround", false);
			}
		} else if (other.CompareTag ("Hook")) {
			transform.root.gameObject.SendMessage ("SetNearHook", false);
		} else if (other.CompareTag ("Branch")) {
			transform.root.gameObject.SendMessage ("SetNearBranch", false);
		}
	}
}
