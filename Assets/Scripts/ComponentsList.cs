using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComponentsList : MonoBehaviour {

	public GameObject[] components;
	
	public GameObject pieceToPlace;
	public GameObject airplane;
	
	void Start () {
		airplane = GameObject.Find("Airplane");
	}

	void Update () {
		if(pieceToPlace) {
			RaycastHit rhit = new RaycastHit();
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rhit, 1000, (1<<8))) {
				pieceToPlace.transform.position = rhit.point;
				pieceToPlace.transform.rotation = Quaternion.LookRotation(rhit.normal);
			}
			
			if(Input.GetMouseButtonDown(0)) {
				pieceToPlace.transform.parent = airplane.transform;
				foreach(Collider mc in pieceToPlace.GetComponentsInChildren<Collider>()) {
					mc.gameObject.layer = 8;
				}
				pieceToPlace = null;
			}
		}
	}
	
	void OnGUI() {
		int y = 0;
		foreach(GameObject g in components) {
			if(GUI.Button(new Rect(0, y, 200, 32), g.name)) {
				pieceToPlace = (GameObject)GameObject.Instantiate(g);	
			}
			y += 32;
		}
	}
	
	
}
