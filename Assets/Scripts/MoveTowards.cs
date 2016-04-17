using UnityEngine;
using System.Collections;

public class MoveTowards : MonoBehaviour {

	public GameObject objective;
	public float movSpeed = 2.0f;

	private bool stopTime = false;
	public bool StopTime
	{
		get { return stopTime;}
		set { stopTime = value;}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!stopTime) {
			Vector3 objectivePosition = new Vector3 (objective.transform.position.x, transform.position.y, objective.transform.position.z);
			transform.position = Vector3.MoveTowards (transform.position, objectivePosition, Time.deltaTime * movSpeed);

			transform.LookAt (new Vector3(objective.transform.position.x, transform.position.y, objective.transform.position.z));
		}

	
	}
}
