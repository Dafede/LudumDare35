using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	Vector3 pos;
	float spacing = 0.1f;
	bool dash=false;
	KeyCode currentKeyPressed;
	Vector3 currentPosition;
	private float startTime;
	float speed = 50.0F;
	private float journeyLength;
	 Vector3 startMarker;
	Vector3 endMarker;
		
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (KeyCode.W)) {
			currentKeyPressed=KeyCode.W;
			pos = transform.position;
			pos.z += spacing;
			pos.x += spacing;
			//transform.LookAt (Vector3.Lerp(transform.position, pos, fracJourney));
			transform.position = pos;

		}
		if (Input.GetKey (KeyCode.S)) {
			currentKeyPressed=KeyCode.S;
			pos = transform.position;
			pos.z -= spacing;
			pos.x -= spacing;
			transform.position = pos;
		}
		if (Input.GetKey (KeyCode.A)) {
			currentKeyPressed=KeyCode.A;
			pos = transform.position;
			pos.x -= spacing;
			pos.z += spacing;
			transform.position = pos;
		}
		if (Input.GetKey (KeyCode.D)) {
			currentKeyPressed=KeyCode.D;
			pos = transform.position;
			pos.x += spacing;
			pos.z -= spacing;
			transform.position = pos;
		}
				
		if (dash) {
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			if (fracJourney >= 0.9)
				dash = false;
			transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
		}
		if (Input.GetKeyDown(KeyCode.Space)) {
			currentPosition=this.transform.position;
			dash = true;
			startTime = Time.time;
			endMarker= this.transform.position;
			Vector3 auxEnd = endMarker;
			auxEnd.x += 15;
			endMarker = auxEnd;
			startMarker = this.transform.position;
			journeyLength = Vector3.Distance(startMarker,endMarker);
		}
	}
}
