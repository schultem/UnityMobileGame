using UnityEngine;
using System.Collections;

public class JointCorrector : MonoBehaviour { 

	private float _referenceAngle; 
	private float _lowerLimit; 
	private float _upperLimit;
	private HingeJoint2D _joint;

	void Start() {
		_joint = GetComponent<HingeJoint2D>();
		
		if (_joint != null) {
			_referenceAngle = _joint.referenceAngle;
			
			var localLimits = _joint.limits;
			_lowerLimit = _referenceAngle + localLimits.min;
			_upperLimit = _referenceAngle + localLimits.max;
		}
	}
	
	void Update() {
		if (_joint == null) {
			return;
		}
		
		float newAngle = _joint.referenceAngle;
		if (_referenceAngle != newAngle) {
			var limits = new JointAngleLimits2D();
			
			limits.min = Angle(_lowerLimit - newAngle);
			limits.max = Angle(_upperLimit - newAngle);
			
			_joint.limits = limits;
			_referenceAngle = newAngle;
		}
	}
	
	public static float Angle(float angle) {
		angle = Mathf.Repeat(angle, 360f);
		if (angle > 180f) {
			angle -= 360f;
		}
		return angle;
	}
}