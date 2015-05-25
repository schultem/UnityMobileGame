using UnityEngine;
using System.Collections;

public class BackgroundScroller : MonoBehaviour
{
	public Transform follow;
	public float x_speed = 0;
	public float y_speed = 0;

	private float initial_x_offset;
	private float initial_y_offset;
	private float x_offset;
	private float y_offset;
	private float last_x;
	private float last_y;
	private float x_size;
	private float y_size;

	void Start (){

		last_x = follow.position.x;
		last_y = follow.position.y;

		x_offset = transform.position.x-follow.position.x;
		y_offset = 0;

		initial_x_offset = x_offset;
		initial_y_offset = transform.position.y-follow.position.y;

		x_size = GetComponent<SpriteRenderer>().sprite.bounds.size.x;
		y_size = GetComponent<SpriteRenderer>().sprite.bounds.size.y/2;

	}

	void Update (){
		x_offset += (follow.position.x - last_x)*x_speed;
		y_offset += (follow.position.y - last_y)*y_speed;

		transform.position = new Vector3(follow.position.x-Mathf.Repeat(x_offset,x_size)+initial_x_offset, follow.position.y-Mathf.Clamp(y_offset,0,y_size)+initial_y_offset, follow.position.z);

		last_x = follow.position.x;
		last_y = follow.position.y;
	}
}