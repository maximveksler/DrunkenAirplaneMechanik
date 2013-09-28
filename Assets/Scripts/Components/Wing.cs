using UnityEngine;
using System.Collections;

public class Wing : AirplaneComponent {
	
	public float liftFactor = 0.1f;
	
	protected override void SimUpdate() {
		float speed = Vector3.Dot(GetAirplane().rigidbody.velocity, transform.right);
		float lift = speed * liftFactor;
		Bounds combinedBounds = new Bounds(transform.position, Vector3.zero);
		foreach (Renderer r in GetComponentsInChildren<Renderer>()) {
		    combinedBounds.Encapsulate(r.bounds);
		}		
		GetAirplane().rigidbody.AddForceAtPosition(transform.up, combinedBounds.center);
	}
}
