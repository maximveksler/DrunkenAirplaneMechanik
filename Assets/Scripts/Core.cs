using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Core : MonoBehaviour {
	
	public static bool SimulationMode = false;
	List<KeyCode> camCodes;
	
	// Use this for initialization
	void Start () {
		rigidbody.centerOfMass = new Vector3(0,0,0);
		camCodes.Add(KeyCode.Alpha1);
		camCodes.Add(KeyCode.Alpha2);
		camCodes.Add(KeyCode.Alpha3);
		camCodes.Add(KeyCode.Alpha4);
		camCodes.Add(KeyCode.Alpha5);
		camCodes.Add(KeyCode.Alpha6);
		camCodes.Add(KeyCode.Alpha7);
		camCodes.Add(KeyCode.Alpha8);
		camCodes.Add(KeyCode.Alpha9);
	}
	
	// Update is called once per frame
	void Update () {
		//transform.rotation.SetLookRotation(rigidbody.velocity.normalized, transform.up);
		
		int i = 0;
		foreach(KeyCode kc in camCodes) {
			if(Input.GetKeyDown(kc)) {
				Debug.Log(i);
				Camera[] cams = (Camera[])FindObjectsOfType(typeof(Camera));
				foreach(Camera c in cams) { c.enabled = false; }
				if(cams.Length < i) {
					cams[i].enabled = true;
				}
			}
			i++;
		}
	}
}
