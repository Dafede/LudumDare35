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
		Vector3 objectivePosition = new Vector3 (objective.transform.position.x, transform.position.y, objective.transform.position.z);
		transform.position = Vector3.MoveTowards (transform.position, objectivePosition, Time.deltaTime * movSpeed);

		transform.LookAt (new Vector3(objective.transform.position.x, transform.position.y, objective.transform.position.z));
	
	}
}
