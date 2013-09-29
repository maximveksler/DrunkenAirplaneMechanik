using UnityEngine;
using System.Collections;

public class ConstructionControls : MonoBehaviour {
	
	float camYaw = 1;
	float camTargetYaw = 1;
	float camPitch = -.5f;
	float camTargetPitch = -0.5f;
	float camDistance = 10;
	
	Vector3 targetCameraPos;
	
	public Texture2D cursorImage;
	public Texture2D cursorImageClick;
	
	Vector3 drunkMouse;
	
	// Use this for initialization
	void Start () {
		Screen.showCursor = false;
		drunkMouse = Input.mousePosition;
		Camera.main.transform.LookAt(new Vector3(0,0,0));
	}
	
	// Update is called once per frame
	void Update () {
		Camera.main.transform.position = 
			Quaternion.Euler(0,camYaw * 180 / 3.1415f,0) *
			Quaternion.Euler(camPitch * 180 / 3.1415f,0,0) *
			new Vector3(0, 0, camDistance);
		
		Vector3 offset = Input.mousePosition - drunkMouse;
		float dist = offset.magnitude;
		offset.Normalize();
		drunkMouse += offset * dist * 7 * Time.deltaTime;
		drunkMouse.x += Mathf.Sin(Time.time) * 2;
		drunkMouse.y += Mathf.Cos(Time.time) * 2;
		
		camYaw = camYaw * 0.9f + camTargetYaw * 0.1f;
		camPitch = camPitch * 0.9f + camTargetPitch * 0.1f;
	}
	
	void OnGUI() {
		float x = Screen.width - 200;
		float y = 100;
		if(GUI.Button(new Rect(x, y + 30, 30, 30), "<")) {
			camTargetYaw -= Mathf.PI / 6.0f;	
		}
		if(GUI.Button(new Rect(x + 60, y + 30, 30, 30), ">")) {
			camTargetYaw += Mathf.PI / 6.0f;	
		}
		if(GUI.Button(new Rect(x+30, y, 30, 30), "^")) {
			camTargetPitch= Mathf.Max(camTargetPitch - 0.1f, -Mathf.PI / 2 + 0.2f);
		}
		if(GUI.Button(new Rect(x+30, y + 60, 30, 30), "v")) {
			camTargetPitch= Mathf.Min(camTargetPitch + 0.1f, 0.2f);
		}
		if(GUI.Button(new Rect(x, y + 90, 60, 30), "go!")) {
			GameObject airplane = GameObject.Find("Airplane");
			DontDestroyOnLoad(airplane);
			Core.SimulationMode = true;
			Application.LoadLevel("simulator");
		}
		
		GUI.depth = 0;
		if(Input.GetMouseButton(0)) {
			GUI.Label(new Rect(drunkMouse.x - 2,Screen.height - drunkMouse.y - 2,64,64),cursorImageClick);		
		}
		else {
			GUI.Label(new Rect(drunkMouse.x - 2,Screen.height - drunkMouse.y - 2,64,64),cursorImage);		
		}
	}
	
}
