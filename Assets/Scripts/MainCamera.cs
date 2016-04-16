using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

	float hSpeed = 0.25f;
	float vSpeed = 0.25f;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		
		float h = hSpeed * Input.GetAxis ("Mouse X");
		float v = hSpeed * Input.GetAxis ("Mouse Y");
		Debug.Log (v);
		this.transform.Translate (h,0,v);
		Vector3 auxPos = this.transform.position;
		auxPos.y = 0;
		this.transform.position = auxPos;

	}
}
