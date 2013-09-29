using UnityEngine;
using System.Collections;

public class Jet : AirplaneComponent {
	
	public float thrustFactor = 10000.0f;
	public float positionalThrustness = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	protected override void SimUpdate () {
		if (Input.GetKey(KeyCode.W))
		{
			Bounds combinedBounds = new Bounds(transform.position, Vector3.zero);
			foreach (Renderer r in GetComponentsInChildren<Renderer>())
				combinedBounds.Encapsulate(r.bounds);
			GetAirplane().rigidbody.AddForceAtPosition(-transform.right * thrustFactor * positionalThrustness, combinedBounds.center);
			GetAirplane().rigidbody.AddForce(-transform.right * thrustFactor * (1-positionalThrustness));
		}
	}
}
