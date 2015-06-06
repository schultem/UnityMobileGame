using UnityEngine;
using System.Collections;

public class EnableOnStart : MonoBehaviour {
	public GameObject game_object_to_enable;

	void Start () {
		game_object_to_enable.SetActive(true);
	}
	

}
