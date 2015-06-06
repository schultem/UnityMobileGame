using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerNearFeature : MonoBehaviour {
	public PlayerController player_controller;
	List<GameObject> ground_features = new List<GameObject>();

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Ground")) {
			if (!ground_features.Contains (other.gameObject)) {
				ground_features.Add (other.gameObject);
			}
			player_controller.SetOnGround(true);
		} else if (other.CompareTag ("Hook")) {
			player_controller.SetNearHook(true);
		} else if (other.CompareTag ("Branch")) {
			player_controller.SetNearBranch(true);
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag ("Ground") && ground_features.Contains(other.gameObject)) {
			ground_features.Remove (other.gameObject);
			if (ground_features.Count > 0) {
				player_controller.SetOnGround(true);
			} else if (ground_features.Count == 0) {
				player_controller.SetOnGround(false);
			} else {
				ground_features = new List<GameObject> ();
				player_controller.SetOnGround(false);
			}
		} else if (other.CompareTag ("Hook")) {
			player_controller.SetNearHook(false);
		} else if (other.CompareTag ("Branch")) {
			player_controller.SetNearBranch(false);
		}
	}
}
