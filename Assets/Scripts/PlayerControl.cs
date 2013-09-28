using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	GameObject airplane;

	// Use this for initialization
	void Start ()
	{
		// If we are starting in the editor from the simulation scene, load the editor first
		if (!Core.SimulationMode)
			Application.LoadLevel(0);
		else
		{
			GameObject spawnPoint = GameObject.Find("Spawn Point");
			airplane = GameObject.Find("Airplane");
			airplane.rigidbody.useGravity = true;
			airplane.transform.position = spawnPoint.transform.position;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		Camera.main.transform.position = airplane.transform.position + new Vector3(0, 0, -10);
	}
}

