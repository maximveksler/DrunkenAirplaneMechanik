using UnityEngine;
using System.Collections;

public class Jet : AirplaneComponent {
	
	public float thrustFactor = 10000.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	protected override void SimUpdate () {
		if (Input.GetKeyDown(KeyCode.W))
		{
			Bounds combinedBounds = new Bounds(transform.position, Vector3.zero);
			foreach (Renderer r in GetComponentsInChildren<Renderer>())
				combinedBounds.Encapsulate(r.bounds);
			GetAirplane().rigidbody.AddForceAtPosition(transform.forward * -thrustFactor, combinedBounds.center);
		}
	}
}
