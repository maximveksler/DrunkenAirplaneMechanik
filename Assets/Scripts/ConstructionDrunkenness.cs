using UnityEngine;
using System.Collections;

public class ConstructionDrunkenness : MonoBehaviour {
	
	float stumbleBlurMax = 0;
	float stumbleCountdown = 0;
	float stumbleBlurTime = 0;
	
	Vector3 randomVect = Vector3.one;
	float randomVectTimer = 0.0f;
	
	float drunkenness = 0.75f;
	
	// Use this for initialization
	void Start () {
		randomVect = Random.onUnitSphere;
	}
	
	// Update is called once per frame
	void Update () {
		if(randomVectTimer <= 0) {
			randomVect = Random.onUnitSphere;	
			randomVectTimer = Random.Range(0.2f, 0.8f);
		}
		if(stumbleCountdown <= 0) {
			stumbleBlurMax = Mathf.Pow(Random.Range(0.0f,1.0f),1.5f);
			stumbleCountdown = Random.Range(0.5f, 4.0f - drunkenness);
			stumbleBlurTime = 0;
		}
		if(stumbleBlurTime < 1) {
			stumbleBlurTime += Time.deltaTime / (stumbleBlurMax + 0.5f);
			GetComponent<Blur>().blurSize = Mathf.Sin(stumbleBlurTime * 3.14159f) * stumbleBlurMax * 4 * drunkenness;
			GetComponent<Blur>().blurEnabled = true;
			
			Camera.main.transform.RotateAround(Camera.main.transform.position, randomVect, Time.deltaTime * stumbleBlurMax * 4 * drunkenness);
		}
		else {
			GetComponent<Blur>().blurEnabled = false;
		}
		stumbleCountdown -= Time.deltaTime;
		
		Camera.main.transform.RotateAround(Camera.main.transform.position, randomVect, Time.deltaTime * 0.5f * drunkenness);
		
		Vector3 camForward = GameObject.Find("Airplane").transform.position - Camera.main.transform.position;
			
		Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, Quaternion.LookRotation(camForward, Vector3.up), 0.05f);
		randomVectTimer -= Time.deltaTime;
	}
	
	void OnGUI() {
		if(GUI.Button(new Rect(Screen.width / 2 - 60, 20, 120, 50), "DRINK!")) {
			drunkenness += 0.25f;	
		}
	}
}
