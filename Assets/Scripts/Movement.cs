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

	public float movSpeed = 20.0f;
	public float anglesRotate = 180.0f;
	public float sprintSpeed = 15.0f;

	public GameObject explosionParticles = null;
	public GameObject enemyExplosionParticles = null;
	private ObjectPool enemyExplosionsParticlesObjectPool = null;
	public GameObject humanMorph = null;
	public GameObject lightMorph = null;
	Animator animator;

		
	// Use this for initialization
	void Start () {
		animator = humanMorph.GetComponent<Animator>();

		enemyExplosionsParticlesObjectPool = new ObjectPool (enemyExplosionParticles, null, true, 100);
	}

	// Update is called once per frame
	void Update () {
		// User Input
		animator.SetBool ("Run", false);
		if (Input.GetKey (KeyCode.W)) {
			animator.SetBool ("Run", true);
			currentKeyPressed=KeyCode.W;
			transform.position += transform.forward * (movSpeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.S)) {
			currentKeyPressed=KeyCode.S;
			transform.position += -transform.forward * (movSpeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.A)) {
			currentKeyPressed = KeyCode.A;
			transform.Rotate (new Vector3(0, -anglesRotate * Time.deltaTime, 0));
		}
		if (Input.GetKey (KeyCode.D)) {
			currentKeyPressed = KeyCode.D;
			transform.Rotate (new Vector3(0, anglesRotate * Time.deltaTime, 0));
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			currentPosition=this.transform.position;

			dash = true;
			startTime = Time.time;
			endMarker= this.transform.position;
			Vector3 auxEnd = endMarker + transform.forward * sprintSpeed;
			endMarker = auxEnd;
			startMarker = this.transform.position;
			journeyLength = Vector3.Distance(startMarker,endMarker);

			// change model
			humanMorph.SetActive (false);
			lightMorph.SetActive (true);
			Instantiate(explosionParticles, transform.position, Quaternion.identity);
		}

		// Logic
				
		if (dash) {
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			if (fracJourney >= 0.9){
				dash = false;
				// back model
				humanMorph.SetActive (true);
				lightMorph.SetActive (false);
			}
			transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Enemy"){
			if (dash == true) {
				GameObject o = enemyExplosionsParticlesObjectPool.GetPooledObject ();
				o.transform.position = other.transform.position;
				o.SetActive (true);
				Destroy (other.gameObject);	
			} else {
				// HURTED BY ENEMY
				//Destroy(gameObject);
			}

		}
	}
}
