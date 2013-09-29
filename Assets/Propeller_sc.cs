using UnityEngine;
using System.Collections;
 
public class Propeller_sc : AirplaneComponent {
	
 	public float thrustFactor = 10000.0f;
	public float positionalThrustness = 0.1f;
	public float maximumRotateSpeed = 40;
    public float minimumTimeToReachTarget = 0.5f;
    Transform _transform;
    Transform _cameraTransform;
    float _velocity;

	// Use this for initialization
	void Start () {
		//_transform = transform;
        //_cameraTransform = Camera.main.transform;
	}
    // Update is called once per frame
   protected override void SimUpdate () {
       //var newRotation = Quaternion.LookRotation(_cameraTransform.position - _transform.position).eulerAngles;
       // var angles = _transform.rotation.eulerAngles;
        //_transform.rotation = Quaternion.Euler(angles.x, Mathf.SmoothDampAngle(angles.y, newRotation.y, ref _velocity, minimumTimeToReachTarget, maximumRotateSpeed),
         //   angles.z);
		 var newRotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position).eulerAngles;
        newRotation.x = 0;
        newRotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRotation), Time.deltaTime);
   
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