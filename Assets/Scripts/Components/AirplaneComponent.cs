using UnityEngine;
using System.Collections;

public class AirplaneComponent : MonoBehaviour {

	float weight;
	
	public GameObject GetAirplane() {
		return GameObject.Find("Airplane");
	}
	
	protected virtual void SimUpdate() {
		
	}
	
	protected virtual void Update() {
		if(Core.SimulationMode) {
			SimUpdate();	
		}
	}
	
}
