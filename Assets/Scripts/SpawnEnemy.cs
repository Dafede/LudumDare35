using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

	public GameObject objectiveOfEnemy;
	public GameObject enemyToSpawn;
	public float timeBetweenSpawn = 5.0f;

	private float timeSinceBeggining = 0.0f;
	private float timeLastSpawn = 0.0f;

	private ObjectPool enemyObjectPool = null;

	private bool stopTime = false;
	public bool StopTime
	{
		get { return stopTime;}
		set { stopTime = value;}
	}

	// Use this for initialization
	void Start () {
		timeSinceBeggining = Time.time;
		enemyObjectPool = new ObjectPool (enemyToSpawn, null, true, 500);
	}
	
	// Update is called once per frame
	void Update () {
		if (!stopTime) {
			if (Time.time - timeLastSpawn >= timeBetweenSpawn) {
				GameObject o = enemyObjectPool.GetPooledObject ();
				o.transform.position = transform.position;
				o.SetActive (true);
				o.GetComponent<MoveTowards> ().objective = objectiveOfEnemy;
				timeLastSpawn = Time.time;
			}
		}
	}
}
