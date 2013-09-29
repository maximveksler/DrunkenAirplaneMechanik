using UnityEngine;
using System.Collections;

public class Stabilizer : AirplaneComponent {
	
	float forceMul = 0.0f;
	float levelFactor = 20.0f;
	
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
		
		rb.AddRelativeTorque(new Vector3(forceMul * GetAirplane().rigidbody.angularVelocity.x,0,0));
		rb.AddRelativeTorque(new Vector3(0, forceMul * GetAirplane().rigidbody.angularVelocity.y,0));
		rb.AddRelativeTorque(new Vector3(0, 0, forceMul * GetAirplane().rigidbody.angularVelocity.z));
		
		Vector3 rotAxis = Vector3.Cross(rb.transform.up, Vector3.up);
		float dir = Vector3.Dot(rb.transform.forward.normalized, Vector3.up);
		
		rb.AddRelativeTorque(dir * levelFactor, 0, 0);
		
		rotAxis = Vector3.Cross(rb.transform.right, Vector3.right);
		dir = Vector3.Dot(rb.transform.up.normalized, Vector3.right);
		
		rb.AddRelativeTorque(0, 0, dir * levelFactor);		
		Debug.Log(dir);
		
		//Debug.Log(GetAirplane().rigidbody.angularVelocity.x);
	}
}
