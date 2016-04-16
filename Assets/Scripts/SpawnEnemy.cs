using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

	public GameObject objectiveOfEnemy;
	public GameObject enemyToSpawn;
	public float timeBetweenSpawn = 5.0f;

	private float timeSinceBeggining = 0.0f;
	private float timeLastSpawn = 0.0f;

	// Use this for initialization
	void Start () {
		timeSinceBeggining = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - timeLastSpawn >= timeBetweenSpawn) {
			GameObject pointer = Instantiate (enemyToSpawn, transform.position, Quaternion.identity) as GameObject;
			pointer.GetComponent<MoveTowards> ().objective = objectiveOfEnemy;
			timeLastSpawn = Time.time;
		}

	}
}
