using UnityEngine;
using System.Collections;
 
public class Propeller_script : AirplaneComponent {
 public float thrustFactor = 10000.0f;
	public float positionalThrustness = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
    // Update is called once per frame
   protected override void SimUpdate () {
        var newRotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position).eulerAngles;
        newRotation.x = 0;
        newRotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRotation), Time.deltaTime);
		Bounds combinedBounds = new Bounds(transform.position, Vector3.zero);
			foreach (Renderer r in GetComponentsInChildren<Renderer>())
				combinedBounds.Encapsulate(r.bounds);
			GetAirplane().rigidbody.AddForceAtPosition(-transform.right * thrustFactor * positionalThrustness, combinedBounds.center);
			GetAirplane().rigidbody.AddForce(-transform.right * thrustFactor * (1-positionalThrustness));
    }
}