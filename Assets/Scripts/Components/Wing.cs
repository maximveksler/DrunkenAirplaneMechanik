using UnityEngine;
using System.Collections;

public class Wing : AirplaneComponent {
	
	public AnimationCurve liftCurve, dragCurve;
	public float liftFactor = 0.1f, dragFactor = 0.1f;
	public bool flip = false;
	float aileronAngle = 0;
	
	protected override void SimUpdate() {
		if (Input.GetKey (KeyCode.DownArrow))
			aileronAngle-= 50 * Time.deltaTime;
		if (Input.GetKey (KeyCode.UpArrow))
			aileronAngle+= 50 * Time.deltaTime;
		if (Input.GetKey (KeyCode.LeftArrow)) {
			aileronAngle += ((flip) ? 15 : -15) * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			aileronAngle += ((flip) ? -15 : 15) * Time.deltaTime;
		}
		aileronAngle *= 0.96f;
		aileronAngle = Mathf.Clamp(aileronAngle, -45, 45);
		
		float flipSign = (flip) ? -1 : 1;
		
		float speed = Vector3.Dot(GetAirplane().rigidbody.velocity, transform.right * flipSign);
		
		float curveIndex = Vector3.Dot(GetAirplane().rigidbody.velocity.normalized, -transform.up);
		//Vector3.Angle(GetAirplane().rigidbody.velocity, transform.right * flipSign) / 180 * 3.14159f;
		float lift = liftFactor * liftCurve.Evaluate(curveIndex + aileronAngle / 45 * 0.5f) * speed;
		float drag = dragFactor * dragCurve.Evaluate(curveIndex + aileronAngle / 45 * 0.5f) * speed;
		Debug.Log(curveIndex);
		Bounds combinedBounds = new Bounds(transform.position, Vector3.zero);
		foreach (Renderer r in GetComponentsInChildren<Renderer>()) {
		    combinedBounds.Encapsulate(r.bounds);
		}
		Vector3 liftForce = lift * transform.up;
		Vector3 dragForce = drag * -transform.right * flipSign;
		GetAirplane().rigidbody.AddForceAtPosition(liftForce, combinedBounds.center);
		GetAirplane().rigidbody.AddForceAtPosition(dragForce, combinedBounds.center);
		//Vector3 aileronNormal = Quaternion.AngleAxis(aileronAngle * (flip ? -1 : 1), transform.forward) * transform.up;
		//GetAirplane().rigidbody.AddForceAtPosition(aileronNormal * lift * (flip ? -1 : 1), combinedBounds.center);
		Debug.DrawLine(transform.position, transform.position + liftForce / 3, new Color(0,1,0,1));
		Debug.DrawLine(transform.position, transform.position + dragForce / 3, new Color(1,0,0,1));
	}
}
