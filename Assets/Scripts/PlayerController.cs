using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public float jump_offset_y = 0f;
	public float roll_angular_drag = 0f;
	public float max_air_angular_velocity = 0f;
	public float max_velocity = 0f;
	public float spin_torque = 0f;
	public bool mirrored = false;
	float _gy = 0;
	public bool on_hook     = false;
	public bool on_branch   = false;
	public bool on_ground   = false;
	public bool near_ground = false;
	public bool near_branch = false;
	public bool standing    = false;

	public CircleCollider2D ground_collider;
	public PolygonCollider2D hook_collider;
	public CircleCollider2D branch_collider;
	public HingeJoint2D branch_hinge;
	public Rigidbody2D branch_rigid_body;
	public GameController game_controller;
	Vector2 branch_hinge_connected_anchor_enabled;
	Vector2 branch_hinge_connected_anchor_disabled = new Vector2(0,0);
	Camera main_camera;
	Rigidbody2D rigid_body;
	Animator animator;

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

		max_velocity = max_velocity * max_velocity;
		_gy = Physics2D.gravity.y * rigid_body.gravityScale;

		branch_hinge_connected_anchor_enabled = branch_hinge.connectedAnchor;

		SetCollider("hook");

		animator = children["SpritePlayer"].gameObject.GetComponent<Animator>();
	}
	
	void Update() {
		if (Input.GetMouseButtonUp(0) && game_controller.jumps>0) {
			if ((on_hook || on_ground || on_branch)){
				SetCollider("none");
				Jump(main_camera.ScreenToWorldPoint(Input.mousePosition));
				game_controller.jumps-=1;
			}
			else{
				rigid_body.AddTorque(-spin_torque*Mathf.Sign(main_camera.ScreenToWorldPoint(Input.mousePosition).x-transform.position.x));
			}
		}

		if (animator.GetCurrentAnimatorStateInfo(0).IsName("hang_idle")){
			children["SpriteTail"].gameObject.GetComponent<SpriteRenderer>().enabled = true;
		}else{
			children["SpriteTail"].gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	void FixedUpdate(){
		if (!on_ground && !on_hook) {
			if(rigid_body.angularVelocity > max_air_angular_velocity){
				rigid_body.angularVelocity *= 0.9f;
			}
			
			if(rigid_body.angularVelocity < -max_air_angular_velocity){
				rigid_body.angularVelocity *= 0.9f;
			}
		}
		if(rigid_body.velocity.sqrMagnitude > max_velocity){
			rigid_body.velocity *= 0.9f;
		}
	}

	bool Mirror(float x_diff){
		bool toggle = mirrored ? (x_diff>=0 ? true : false) : (x_diff>=0 ? false : true);
		mirrored = x_diff>0 ? false : true;

		if (toggle) {
			transform.localScale = x_diff>0 ? new Vector3(1,1,1) : new Vector3(-1,1,1);
		}
		return toggle;
	}

	void Jump(Vector3 world_jump_position){
		float x_diff = world_jump_position.x-transform.position.x;
		float y_diff = world_jump_position.y-transform.position.y+0.1f;//0.1 to jump closer to the character's tail
		float y_offset = on_ground ? Mathf.Max(jump_offset_y,5.0f) : jump_offset_y;
		float angle = Mathf.Atan2(y_diff+y_offset,x_diff);

		if (angle != Mathf.PI/2) {
			float speed = (rigid_body.mass+branch_rigid_body.mass)*Mathf.Sqrt(_gy*Mathf.Pow(x_diff,2)/(2*y_diff*Mathf.Pow(Mathf.Cos(angle),2)-x_diff*Mathf.Sin(2*angle)));
			rigid_body.angularDrag=0;
			rigid_body.velocity = new Vector2(speed*Mathf.Cos(angle),speed*Mathf.Sin(angle));
			Mirror(x_diff);

			float start_angle = VectorEulerAngle(rigid_body.velocity);
			float end_angle   = VectorEulerAngle(new Vector2(rigid_body.velocity.x, (rigid_body.velocity.y+_gy*(x_diff/rigid_body.velocity.x))));
			SetRotationToVector(rigid_body.velocity);
			rigid_body.angularVelocity = (end_angle-start_angle)/(x_diff/rigid_body.velocity.x);

			if (on_branch || on_hook) {
				animator.SetTrigger("hang_jump");
			}
			children["SpriteRearLeg"].gameObject.GetComponent<Animator>().SetTrigger("jump");
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

	void SetOnHook(bool _on_hook) {
		if (_on_hook && hook_collider.enabled) {
			hook_collider.enabled = false;
			hook_collider.sharedMaterial.friction = 0.01f;
			hook_collider.enabled = true;
		}
		on_hook = _on_hook;
	}

	void SetOnBranch(bool _on_branch) {
		on_branch = _on_branch;
	}

	void SetNearHook(bool near_hook) {
		if (near_hook) {
			ResetOnGround();
			children["TriggerNearGround"].gameObject.GetComponent<TriggerNearGround>().Reset();
			SetNearGround(false);

		} else {
			hook_collider.enabled = false;
			hook_collider.sharedMaterial.friction = 0.75f;
			hook_collider.enabled = true;
		}
	}

	void SetNearBranch(bool _near_branch) {
		if (_near_branch) {
			ResetOnGround ();
			children["TriggerNearGround"].gameObject.GetComponent<TriggerNearGround>().Reset();
			SetNearGround (false);

			SetCollider("branch");

		} else {
			SetCollider("hook");
		}
		near_branch = _near_branch;
	}

	void SetNearGround(bool _near_ground) {
		if (_near_ground) {
			children["SpriteRearLeg"].gameObject.GetComponent<SpriteRenderer>().enabled = false;
			children["SpriteHead"].gameObject.GetComponent<SpriteRenderer>().enabled = false;
			SetCollider("ground");
			rigid_body.angularDrag = roll_angular_drag;
		} else {
			children["SpriteRearLeg"].gameObject.GetComponent<SpriteRenderer>().enabled = true;
			children["SpriteHead"].gameObject.GetComponent<SpriteRenderer>().enabled = true;
			SetCollider("hook");
			rigid_body.angularDrag = 0;
		}
		animator.SetBool ("near_ground",_near_ground);
		near_ground = _near_ground;
	}

	void SetOnGround(bool _on_ground){
		animator.SetBool ("on_ground",_on_ground);
		on_ground = _on_ground;
	}
	
	void ResetOnGround() {
		on_ground = false;
		animator.SetBool ("on_ground",false);
	}

	void SetCollider(string collider_name) {
		if (collider_name == "hook") {
			branch_rigid_body.angularVelocity = 0;
			rigid_body.angularDrag=0;
			branch_hinge.connectedAnchor = branch_hinge_connected_anchor_disabled;
			hook_collider.enabled   = true;
			ground_collider.enabled = false;
			branch_collider.enabled = false;
		} else if (collider_name == "ground") {
			branch_hinge.connectedAnchor = branch_hinge_connected_anchor_disabled;
			hook_collider.enabled   = false;
			ground_collider.enabled = true;
			branch_collider.enabled = false;
		} else if (collider_name == "branch") {
			branch_hinge.connectedAnchor = branch_hinge_connected_anchor_enabled;
			branch_rigid_body.angularDrag=50.0f;
			hook_collider.enabled   = true;
			ground_collider.enabled = false;
			branch_collider.enabled = true;
		} else if (collider_name == "none") {
			branch_hinge.connectedAnchor = branch_hinge_connected_anchor_disabled;
			hook_collider.enabled   = false;
			ground_collider.enabled = false;
			branch_collider.enabled = false;
		} else {
			Debug.LogError("Collider name not recognized: "+collider_name);
		}
	}
}