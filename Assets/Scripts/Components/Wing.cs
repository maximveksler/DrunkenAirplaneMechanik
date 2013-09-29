using UnityEngine;
using System.Collections;

public class Wing : AirplaneComponent {
	
	public float liftFactor = 0.01f;
	public bool flip = false;
	
	protected override void SimUpdate() {
		float speed = Vector3.Dot(GetAirplane().rigidbody.velocity, transform.right);
		float lift = speed * liftFactor;
		Bounds combinedBounds = new Bounds(transform.position, Vector3.zero);
		foreach (Renderer r in GetComponentsInChildren<Renderer>()) {
		    combinedBounds.Encapsulate(r.bounds);
		}		
		GetAirplane().rigidbody.AddForceAtPosition(transform.up * lift * (flip ? -1 : 1), combinedBounds.center);
		Debug.Log(lift);
	}
}
