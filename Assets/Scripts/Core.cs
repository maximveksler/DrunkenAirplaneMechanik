using UnityEngine;
using System.Collections;

public class Core : MonoBehaviour {
	
	public static bool SimulationMode = false;
	
	// Use this for initialization
	void Start () {
		rigidbody.centerOfMass = new Vector3(0,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		//transform.rotation.SetLookRotation(rigidbody.velocity.normalized, transform.up);
	}
}
