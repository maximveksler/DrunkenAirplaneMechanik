using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComponentsList : MonoBehaviour {

	public GameObject[] components;
	
	public GameObject pieceToPlace;
	public GameObject selectedPiece;
	public GameObject airplane;
	
	List<Material> selectedOldMaterials = new List<Material>();
	
	public Material selectedMat;
	
	Vector3 spinMouseStart;
	bool spinning = false;
	Vector3 spinVelocity;
	
	void Start () {
		airplane = GameObject.Find("Airplane");
	}

	void Update () {
		if(selectedPiece != null) {
			Vector3 offset = Input.mousePosition - spinMouseStart;
			spinVelocity.x = offset.x / 20.0f;
			spinVelocity.y = offset.y / 20.0f;
			//selectedPiece.transform.RotateAround(Vector3.up, spinVelocity.x * Time.deltaTime);
			selectedPiece.transform.RotateAround(selectedPiece.transform.forward, spinVelocity.y * Time.deltaTime);
			
			if(Input.GetMouseButtonUp(0)) {
				Deselect();
			}
		}
		if(pieceToPlace) {
			GameObject hitObject = null;
			RaycastHit rhit = new RaycastHit();
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rhit, 1000, (1<<8))) {
				pieceToPlace.transform.position = rhit.point;
				hitObject = rhit.collider.gameObject;
				pieceToPlace.transform.rotation = Quaternion.LookRotation(rhit.normal);
			}
			
			if(pieceToPlace.transform.position.x < 0) {
				pieceToPlace.transform.localScale = new Vector3(-1, 1, 1);
				//pieceToPlace.transform.RotateAround(pieceToPlace.transform.right, Mathf.PI);
			}
			else {
				pieceToPlace.transform.localScale = Vector3.one;
			}
			
			if(Input.GetMouseButtonDown(0)) {
				if(hitObject != null) {
					pieceToPlace.transform.parent = hitObject.transform;
				} else {
					Debug.LogError("NO OBJECT CLICKED ON, DUMBASS");
				}
				foreach(Collider mc in pieceToPlace.GetComponentsInChildren<Collider>()) {
					mc.gameObject.layer = 8;
				}
				pieceToPlace = null;
			}
		}
		else {
			if(Input.GetMouseButtonDown(0) && GUIUtility.hotControl == 0) {
				Deselect();
				RaycastHit rhit = new RaycastHit();
				if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rhit, 1000, (1<<8))) {
					SelectPiece(rhit.collider.gameObject);
				}
				spinMouseStart = Input.mousePosition;
				spinning = true;
			}
		}
	}
	
	void Deselect() {
		if(selectedPiece) {
			int i= 0;
			foreach(Renderer r in selectedPiece.GetComponentsInChildren<Renderer>()) {
				r.sharedMaterial = selectedOldMaterials[i];
				i++;
			}
		}
		selectedPiece = null;
	}
	
	void SelectPiece(GameObject g) {
		selectedPiece = g;
		selectedOldMaterials = new List<Material>();
		foreach(Renderer r in g.GetComponentsInChildren<Renderer>()) {
			selectedOldMaterials.Add(r.sharedMaterial);
			r.sharedMaterial = selectedMat;
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
