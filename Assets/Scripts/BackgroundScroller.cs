using UnityEngine;
using System.Collections;

public class BackgroundScroller : MonoBehaviour
{
	public Transform follow;
	public float y_offset = 0;
	public float speed = 0;

	private float initial_x_offset;
	private float last_x_offset;
	private float last_x;
	private float x_size;

	void Start (){
		x_size = GetComponent<SpriteRenderer>().sprite.bounds.size.x;

		last_x = follow.position.x;
		last_x_offset = transform.position.x-follow.position.x;
		initial_x_offset = last_x_offset;
	}

	void Update (){
		float x_offset = last_x_offset + (follow.position.x - last_x)*speed;

		transform.position = new Vector3(follow.position.x-Mathf.Repeat(x_offset,x_size)+initial_x_offset, follow.position.y+y_offset, follow.position.z);

		last_x = follow.position.x;
		last_x_offset = x_offset;
	}
}