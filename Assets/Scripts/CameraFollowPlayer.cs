using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour {
	public Transform target;
	public PlayerController player_controller;
	public float z_offset;
	public float y_offset;
	public float x_offset;

	void Update()
	{
		transform.position = new Vector3 (target.position.x - x_offset, target.position.y - y_offset, target.position.z - z_offset);
	}
}
