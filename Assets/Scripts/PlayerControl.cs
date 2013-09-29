using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	GameObject airplane;
	
	void PositionCamera()
	{
		Camera mainCamera = Camera.main;
		mainCamera.transform.localPosition = new Vector3(0, 2, -10);
		Vector3 position = mainCamera.transform.position;
		position.y = Mathf.Max(position.y, 5);
		mainCamera.transform.position = position;
		mainCamera.transform.LookAt(airplane.transform);
	}
	
	// Use this for initialization
	void Start ()
	{
		// If we are starting in the editor from the simulation scene, load the editor first
		if (!Core.SimulationMode)
		{
			Destroy(gameObject);
			Application.LoadLevel(0);
		}
		else
		{
			GameObject spawnPoint = GameObject.Find("Spawn Point");
			airplane = GameObject.Find("Airplane");
			airplane.rigidbody.useGravity = true;
			airplane.transform.position = spawnPoint.transform.position;
			airplane.transform.eulerAngles = spawnPoint.transform.eulerAngles;
			Camera.main.transform.parent = airplane.transform;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		PositionCamera();
	}
}

