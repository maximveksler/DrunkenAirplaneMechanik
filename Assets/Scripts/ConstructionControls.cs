using UnityEngine;
using System.Collections;

public class ConstructionControls : MonoBehaviour {
	
	float camYaw = 1;
	float camPitch = -.5f;
	float camDistance = 10;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Camera.main.transform.position = 
			Quaternion.Euler(0,camYaw * 180 / 3.1415f,0) *
			Quaternion.Euler(camPitch * 180 / 3.1415f,0,0) *
			new Vector3(0, 0, camDistance);
		Camera.main.transform.LookAt(new Vector3(0,0,0));
	}
	
	void OnGUI() {
		float x = Screen.width - 200;
		float y = 100;
		if(GUI.Button(new Rect(x, y + 30, 30, 30), "<")) {
			camYaw -= Mathf.PI / 6.0f;	
		}
		if(GUI.Button(new Rect(x + 60, y + 30, 30, 30), ">")) {
			camYaw += Mathf.PI / 6.0f;	
		}
		if(GUI.Button(new Rect(x+30, y, 30, 30), "^")) {
			camPitch= Mathf.Max(camPitch - 0.1f, -Mathf.PI + 0.2f);
		}
		if(GUI.Button(new Rect(x+30, y + 60, 30, 30), "v")) {
			camPitch= Mathf.Min(camPitch + 0.1f, Mathf.PI - 0.2f);
		}
		if(GUI.Button(new Rect(x, y + 90, 60, 30), "go!")) {
			Destroy(gameObject);
			GameObject.Find("Airplane").AddComponent<PlayerControl>();
			Core.SimulationMode = true;
		}
	}
}
