using UnityEngine;
using System.Collections;

public class TriggerNearGround : MonoBehaviour {

	int near_ground_count = 0;

	public void Reset(){
		near_ground_count=0;
		transform.root.gameObject.SendMessage("SetNearGround", (near_ground_count>0 ? true : false));
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Ground")){
			near_ground_count+=1;
			transform.root.gameObject.SendMessage("SetNearGround",(near_ground_count>0 ? true : false));
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag("Ground")){
			near_ground_count-=1;
			transform.root.gameObject.SendMessage("SetNearGround", (near_ground_count>0 ? true : false));
		}
		if (near_ground_count < 0) {
			near_ground_count=0;
		}
	}

}
