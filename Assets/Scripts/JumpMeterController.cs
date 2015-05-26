using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class JumpMeterController : MonoBehaviour {
	public GameController game_controller;
	public GameObject jump_meter;
	public GameObject jump_meter_counter_0;
	public GameObject jump_meter_counter_1;
	public GameObject jump_meter_counter_2;
	public GameObject jump_meter_counter_3;
	public GameObject jump_meter_counter_4;
	public GameObject jump_meter_counter_5;
	public GameObject jump_meter_counter_6;
	public GameObject jump_meter_counter_7;
	public GameObject jump_meter_counter_8;
	public GameObject jump_meter_counter_9;
	public GameObject jump_meter_counter_10;
	public GameObject jump_meter_counter_11;
	public GameObject jump_meter_counter_12;
	public GameObject jump_meter_counter_13;
	public GameObject jump_meter_counter_14;
	public GameObject jump_meter_counter_15;
	public GameObject jump_meter_counter_16;
	public GameObject jump_meter_counter_17;
	public GameObject jump_meter_counter_18;
	public GameObject jump_meter_counter_19;
	public GameObject jump_meter_counter_20;
	public GameObject jump_meter_counter_21;
	public GameObject jump_meter_counter_22;
	public GameObject jump_meter_counter_23;
	public GameObject jump_meter_counter_24;
	public GameObject jump_meter_counter_25;
	public GameObject jump_meter_counter_26;
	public GameObject jump_meter_counter_27;
	public GameObject jump_meter_counter_28;
	public GameObject jump_meter_counter_29;

	List<GameObject> counters = new List<GameObject>();
	int active_counters = 0;

	void Start () {
		Vector3 bottom_left_corner = Camera.main.ViewportToWorldPoint(new Vector3(0,0,Camera.main.nearClipPlane));
		transform.position = new Vector3(bottom_left_corner.x+0.75f, bottom_left_corner.y+0.25f, transform.position.z);
		InitCounters();
	}

	void FixedUpdate () {
		if (game_controller.jumps != active_counters) {
			UpdateCounters();
		}
	}

	void UpdateCounters(){
		for (int i=0; i<counters.Count; i++) {
			if (i < game_controller.jumps){
				counters[i].SetActive (true);
			} else {
				counters[i].SetActive (false);
			}
		}
		active_counters = game_controller.jumps;
	}

	void InitCounters(){
		counters = new List<GameObject>();
		counters.Add(jump_meter_counter_0);
		counters.Add(jump_meter_counter_1);
		counters.Add(jump_meter_counter_2);
		counters.Add(jump_meter_counter_3);
		counters.Add(jump_meter_counter_4);
		counters.Add(jump_meter_counter_5);
		counters.Add(jump_meter_counter_6);
		counters.Add(jump_meter_counter_7);
		counters.Add(jump_meter_counter_8);
		counters.Add(jump_meter_counter_9);
		counters.Add(jump_meter_counter_10);
		counters.Add(jump_meter_counter_11);
		counters.Add(jump_meter_counter_12);
		counters.Add(jump_meter_counter_13);
		counters.Add(jump_meter_counter_14);
		counters.Add(jump_meter_counter_15);
		counters.Add(jump_meter_counter_16);
		counters.Add(jump_meter_counter_17);
		counters.Add(jump_meter_counter_18);
		counters.Add(jump_meter_counter_19);
		counters.Add(jump_meter_counter_20);
		counters.Add(jump_meter_counter_21);
		counters.Add(jump_meter_counter_22);
		counters.Add(jump_meter_counter_23);
		counters.Add(jump_meter_counter_24);
		counters.Add(jump_meter_counter_25);
		counters.Add(jump_meter_counter_26);
		counters.Add(jump_meter_counter_27);
		counters.Add(jump_meter_counter_28);
		counters.Add(jump_meter_counter_29);

		//f (game_controller.jumps <= 0) {
		//	jump_meter_counter.SetActive (false);
		// else {
		//	jump_meter_counter.SetActive (true);
		//
		//
		//or (int i=1; i<max_counters; i++){
		//	GameObject last_counter = counters.Last();
		//	Vector3 last_counter_position = Camera.main.WorldToScreenPoint(last_counter.transform.position);
		//	counter = Object.Instantiate(last_counter,last_counter.transform.position,last_counter.transform.rotation) as GameObject;
		//	counter.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(last_counter_position.x,last_counter_position.y+counter_size,last_counter_position.z));
		//	counter.transform.parent = game_controller.transform;
		//	if (i < game_controller.jumps){
		//		counter.SetActive (true);
		//	} else {
		//		counter.SetActive (false);
		//	}
		//	counters.Add(counter);
		//
	}
}
