using UnityEngine;
using System.Collections;

public class Stabilizer : AirplaneComponent {
	
	public float forceMul = 0.0f;
	public float levelFactor = 1.0f;
	
	public AnimationCurve liftCurve, dragCurve;
	public float liftFactor = 0.1f, dragFactor = 0.1f;	
	public float aileronAngle;
	
	protected override void SimUpdate ()
	{
		/*
		Bounds combinedBounds = new Bounds(transform.position, Vector3.zero);
		foreach (Renderer r in GetComponentsInChildren<Renderer>()) {
		    combinedBounds.Encapsulate(r.bounds);
		}			
		GetAirplane().rigidbody.AddForceAtPosition(
			new Vector3(0, forceMul * GetAirplane().rigidbody.angularVelocity.x, 0),
			transform.position);
		GetAirplane().rigidbody.AddForceAtPosition(
			new Vector3(0, -forceMul * GetAirplane().rigidbody.angularVelocity.x, 0),
			transform.position);
		*/		
		
		Rigidbody rb = GetAirplane().rigidbody;
		
		Debug.Log(forceMul * GetAirplane().rigidbody.angularVelocity.x);
		
		//rb.AddRelativeTorque(new Vector3(-forceMul * GetAirplane().rigidbody.angularVelocity.x,0,0));
		//rb.AddRelativeTorque(new Vector3(0, forceMul * GetAirplane().rigidbody.angularVelocity.y,0));
		//rb.AddRelativeTorque(new Vector3(0, 0, forceMul * GetAirplane().rigidbody.angularVelocity.z));
		
		Vector3 rotAxis = Vector3.Cross(rb.transform.up, Vector3.up);
		float dir = Vector3.Dot(rb.transform.forward.normalized, Vector3.up);
		
		//rb.AddRelativeTorque(dir * levelFactor, 0, 0);
		//rb.AddForceAtPosition(transform.forward * dir * levelFactor, transform.position);
		
		rotAxis = Vector3.Cross(rb.transform.right, Vector3.right);
		dir = Vector3.Dot(rb.transform.up.normalized, Vector3.right);
		
		//rb.AddRelativeTorque(0, 0, dir * levelFactor);		
		//Debug.Log(dir);
		
		//Debug.Log(GetAirplane().rigidbody.angularVelocity.x);
		
		if (Input.GetKey (KeyCode.DownArrow))
			aileronAngle+= 5;
		if (Input.GetKey (KeyCode.UpArrow))
			aileronAngle-= 5;
		
		aileronAngle *= 0.9f;
		aileronAngle = Mathf.Clamp(aileronAngle, -45, 45);
		
		float speed = Vector3.Dot(GetAirplane().rigidbody.velocity, -transform.right);
		
		float curveIndex = Vector3.Dot(GetAirplane().rigidbody.velocity.normalized, -transform.forward);
		//Vector3.Angle(GetAirplane().rigidbody.velocity, transform.right * flipSign) / 180 * 3.14159f;
		float lift = liftFactor * liftCurve.Evaluate(curveIndex + aileronAngle / 45 * 0.5f) * speed;
		float drag = dragFactor * dragCurve.Evaluate(curveIndex + aileronAngle / 45 * 0.5f) * speed;
		Bounds combinedBounds = new Bounds(transform.position, Vector3.zero);
		foreach (Renderer r in GetComponentsInChildren<Renderer>()) {
		    combinedBounds.Encapsulate(r.bounds);
		}
		Vector3 liftForce = lift * transform.forward;
		Vector3 dragForce = drag * transform.right;
		GetAirplane().rigidbody.AddForceAtPosition(liftForce, combinedBounds.center);
		GetAirplane().rigidbody.AddForceAtPosition(dragForce, combinedBounds.center);
		//Vector3 aileronNormal = Quaternion.AngleAxis(aileronAngle * (flip ? -1 : 1), transform.forward) * transform.up;
		//GetAirplane().rigidbody.AddForceAtPosition(aileronNormal * lift * (flip ? -1 : 1), combinedBounds.center);
		Debug.DrawLine(transform.position, transform.position + liftForce / 3, new Color(0,1,0,1));
		Debug.DrawLine(transform.position, transform.position + dragForce / 3, new Color(1,0,0,1));	
	}
}
