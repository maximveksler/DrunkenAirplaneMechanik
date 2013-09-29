using UnityEngine;
using System.Collections;

public class Wing : AirplaneComponent {
	
	public AnimationCurve liftCurve, dragCurve;
	public float liftFactor = 0.1f, dragFactor = 0.1f;
	public bool flip = false;
	float aileronAngle = 0;
	
	protected override void SimUpdate() {
		if (Input.GetKey (KeyCode.DownArrow))
			aileronAngle-= 5;
		if (Input.GetKey (KeyCode.UpArrow))
			aileronAngle+= 5;
		aileronAngle = Mathf.Clamp(aileronAngle, -45, 45);
		
		float curveIndex = Vector3.Angle(GetAirplane().rigidbody.velocity, transform.right) / Mathf.PI;
		float lift = liftFactor * liftCurve.Evaluate(curveIndex);
		float drag = liftFactor * dragCurve.Evaluate(curveIndex);
		Bounds combinedBounds = new Bounds(transform.position, Vector3.zero);
		foreach (Renderer r in GetComponentsInChildren<Renderer>()) {
		    combinedBounds.Encapsulate(r.bounds);
		}
		Vector3 liftForce = lift * transform.up * ((flip) ? -1 : 1);
		Vector3 dragForce = drag * -transform.right * ((flip) ? -1 : 1);
		GetAirplane().rigidbody.AddForceAtPosition(liftForce, combinedBounds.center);
		GetAirplane().rigidbody.AddForceAtPosition(dragForce, combinedBounds.center);
		//Vector3 aileronNormal = Quaternion.AngleAxis(aileronAngle * (flip ? -1 : 1), transform.forward) * transform.up;
		//GetAirplane().rigidbody.AddForceAtPosition(aileronNormal * lift * (flip ? -1 : 1), combinedBounds.center);
		Debug.DrawLine(transform.position, transform.position + liftForce, new Color(0,1,0,1));
		Debug.DrawLine(transform.position, transform.position + dragForce, new Color(1,0,0,1));
	}
}
