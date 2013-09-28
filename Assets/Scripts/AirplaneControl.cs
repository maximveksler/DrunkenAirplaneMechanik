using UnityEngine;
using System.Collections;

public class AirplaneControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float lift = gameObject.rigidbody.velocity.x * 3;
		gameObject.rigidbody.AddRelativeForce(10, lift, 0, ForceMode.VelocityChange);
	}
}
