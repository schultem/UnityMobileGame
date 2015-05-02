using UnityEngine;
using System.Collections;

public class HeadController : MonoBehaviour {
	public Rigidbody2D parent_rigid_body;
	public PlayerController parent_controller;
	public float min_angle = 0.0f;
	public float max_angle = 0.0f;
	public float lerp_scale= 1.0f;

	float last_angle = 0.0f;
	
	void Update () {
		if (parent_rigid_body.velocity.sqrMagnitude > 0) {
			LerpRotationToVector( MirrorVector( new Vector2(parent_rigid_body.velocity.x,parent_rigid_body.velocity.y) ) );
		}
	}
	
	float VectorEulerAngle(Vector2 vector){
		float angle = -Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg;
		return angle<0 ? angle+=90 : angle-=90;
	}

	Vector2 MirrorVector(Vector2 vector){
		return (parent_controller.mirrored ? new Vector2(-vector.x,vector.y) : vector);
	}

	void LerpRotationToVector(Vector2 vector){

		float angle = Mathf.Clamp ( Mathf.Lerp(last_angle, VectorEulerAngle(vector), Time.deltaTime*lerp_scale) , min_angle, max_angle) ;
		last_angle = angle;
		
		transform.rotation = new Quaternion (0, 0, 0, 0);
		transform.Rotate (Vector3.forward * (angle));
	}

}
