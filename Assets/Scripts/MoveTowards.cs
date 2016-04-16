using UnityEngine;
using System.Collections;

public class MoveTowards : MonoBehaviour {

	public GameObject objective;
	public float movSpeed = 2.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, objective.transform.position, Time.deltaTime * movSpeed);
		Debug.Log (transform.position);
	}
}
