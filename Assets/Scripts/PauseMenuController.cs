using UnityEngine;
using System.Collections;

public class PauseMenuController : MonoBehaviour {
	
	void Start () {
		Vector3 middle = Camera.main.ViewportToWorldPoint(new Vector3(0.5f,0.5f,0));
		transform.position = new Vector3(middle.x, middle.y, transform.position.z);
		gameObject.SetActive (false);
	}
}
