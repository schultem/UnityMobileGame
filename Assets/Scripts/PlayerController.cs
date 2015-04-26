using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public float jump_offset_y = 0f;
	public float roll_angular_drag = 0f;
	public float max_air_angular_velocity = 0f;
	public float max_air_velocity = 0f;
	public bool mirrored = false;
	float _gy = 0;
	int _ground_collision_count = 0;
	bool _on_hook = false;
	bool _on_ground = false;
	
	Camera main_camera;
	Rigidbody2D rigid_body;
	Animator animator;
	CircleCollider2D circle_collider;
	PolygonCollider2D polygon_collider;
	Dictionary<string, Transform> children = new Dictionary<string, Transform>();

	void Start(){
		main_camera = Camera.main;

		foreach(Transform t in transform)
		{
			children.Add(t.name, t);
		}

		rigid_body = GetComponent<Rigidbody2D>();
		rigid_body.collisionDetectionMode = CollisionDetectionMode2D.Continuous; 
		rigid_body.interpolation = RigidbodyInterpolation2D.Extrapolate;

		max_air_velocity = max_air_velocity * max_air_velocity;
		_gy = Physics2D.gravity.y * rigid_body.gravityScale;

		circle_collider = GetComponent<CircleCollider2D>();
		circle_collider.enabled = false;

		polygon_collider = GetComponent<PolygonCollider2D>();
		polygon_collider.enabled = true;

		animator = children["SpritePlayer"].gameObject.GetComponent<Animator>();
	}
	
	void Update() {

		if (Input.GetMouseButtonUp(0)) {
			if (_on_hook || _on_ground){
				Jump(main_camera.ScreenToWorldPoint(Input.mousePosition));
			}
			else{
				rigid_body.AddTorque(-(main_camera.ScreenToWorldPoint(Input.mousePosition).x-transform.position.x));
			}
		}
	}

	void FixedUpdate(){
		if (!_on_ground && !_on_hook) {

			rigid_body.angularVelocity = Mathf.Clamp (rigid_body.angularVelocity, -max_air_angular_velocity, max_air_angular_velocity);

			if(rigid_body.velocity.sqrMagnitude > max_air_velocity){
				rigid_body.velocity *= 0.9f;
			}
		}
	}

	void Mirror(float x_diff){
		bool toggle = mirrored ? (x_diff>=0 ? true : false) : (x_diff>=0 ? false : true);
		mirrored = x_diff>0 ? false : true;

		if (toggle) {
			transform.localScale = x_diff>0 ? new Vector3(1,1,1) : new Vector3(-1,1,1);
		}
	}

	void Jump(Vector3 world_jump_position){
		float x_diff = world_jump_position.x-transform.position.x;
		float y_diff = world_jump_position.y-transform.position.y;
		float angle = Mathf.Atan2(y_diff+jump_offset_y,x_diff);

		if (angle != Mathf.PI/2) {
			float speed = Mathf.Sqrt(_gy*Mathf.Pow(x_diff,2)/(2*y_diff*Mathf.Pow(Mathf.Cos(angle),2)-x_diff*Mathf.Sin(2*angle)));
			
			polygon_collider.enabled = false;
			rigid_body.velocity = new Vector2(speed*Mathf.Cos(angle),speed*Mathf.Sin(angle));
			Mirror(x_diff);

			if (_on_ground){ //auto rotate towards target when jumping from ground
				float start_angle = VectorEulerAngle(rigid_body.velocity);
				float end_angle   = VectorEulerAngle(new Vector2(rigid_body.velocity.x, (rigid_body.velocity.y+_gy*(x_diff/rigid_body.velocity.x))));

				SetRotationToVector(rigid_body.velocity);
	
				rigid_body.angularVelocity = (end_angle-start_angle)/(x_diff/rigid_body.velocity.x);
				children["SpriteRearLeg"].gameObject.GetComponent<Animator>().SetTrigger("Jump");
			}
		}
	}

	float VectorEulerAngle(Vector2 vector){
		float angle = -Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg;
		return angle<0 ? angle+=90 : angle-=90;
	}

	void SetRotationToVector(Vector2 vector){
		transform.rotation = new Quaternion(0,0,0,0);
		transform.Rotate (Vector3.forward * VectorEulerAngle(vector));
	}

	void SetOnHook(bool on_hook) {
		_on_hook = on_hook;
		polygon_collider.sharedMaterial.friction = 0.01f;
	}

	void SetNearHook(bool near_hook) {
		if (!near_hook) {
			polygon_collider.enabled = true;
			polygon_collider.sharedMaterial.friction = 0.5f;
		}
		_ground_collision_count = 0;
	}

	void SetNearGround(bool near_ground) {
		if (near_ground) {
			children["SpriteRearLeg"].gameObject.GetComponent<SpriteRenderer>().enabled = false;
			children["SpriteHead"].gameObject.GetComponent<SpriteRenderer>().enabled = false;
			polygon_collider.enabled = false;
			circle_collider.enabled = true;
			rigid_body.angularDrag = roll_angular_drag;
		} else {
			children["SpriteRearLeg"].gameObject.GetComponent<SpriteRenderer>().enabled = true;
			children["SpriteHead"].gameObject.GetComponent<SpriteRenderer>().enabled = true;
			polygon_collider.enabled = true;
			circle_collider.enabled = false;
			rigid_body.angularDrag = 0;
			_ground_collision_count = 0;
		}
		animator.SetBool ("near_ground",near_ground);
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Ground") {
			_ground_collision_count+=1;
			_on_ground = (_ground_collision_count>0 ? true : false);
			animator.SetBool ("on_ground",_on_ground);
		}
	}

	void OnCollisionExit2D(Collision2D other){
		if (other.gameObject.tag == "Ground") {
			_ground_collision_count-=1;
			_on_ground = (_ground_collision_count>0 ? true : false);
			animator.SetBool ("on_ground",_on_ground);
		}
	}

}