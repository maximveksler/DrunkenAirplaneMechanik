using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Camera.main.transform.position = transform.position + transform.TransformPoint(new Vector3(0, 10, -20));
		Camera.main.transform.LookAt(transform);
		
		rigidbody.AddForce(new Vector3(0, 0, 1));
	}
}

