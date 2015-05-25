﻿using UnityEngine;
using System.Collections;

public class SeedController : MonoBehaviour {
	public GameController game_controller;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
			game_controller.jumps+=1;
			Destroy(gameObject);
		}
	}
}