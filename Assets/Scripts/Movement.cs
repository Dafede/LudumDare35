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
	public Canvas interfaceCanvas = null;
	WaitForSeconds waitLifeTime = null;
	bool specialMode = false;
	Animator animator;

	Rigidbody rb;

	// invulnerability
	private float timeSinceLastHit = 0.0f;

		
	// Use this for initialization
	void Start () {
		animator = humanMorph.GetComponent<Animator>();

		enemyExplosionsParticlesObjectPool = new ObjectPool (enemyExplosionParticles, null, true, 100);
		rb = GetComponent<Rigidbody> ();
		timeSinceLastHit = Time.time;
	}

	// Update is called once per frame
	void Update () {
		// debug input
		if(Input.GetKey(KeyCode.P))
		{
			// can enter mode
			Debug.Log("SCENE");
			TimeStop.BeginScene();
		}

		if(Input.GetKey(KeyCode.Q) && interfaceCanvas.GetComponent<Interface> ().energyBar.sizeDelta.x == 100)
		{
			// can enter mode
			TimeStop.StopTime ();
			waitLifeTime = new WaitForSeconds (5.0f);
			humanMorph.SetActive (false);
			lightMorph.SetActive (true);
			specialMode = true;
			interfaceCanvas.GetComponent<Interface> ().activeEnergy ();
			movSpeed = movSpeed * 1.25f;
			StartCoroutine (ExistSpecialMode());
		}


		// User Input
		animator.SetBool ("Run", false);
		if (Input.GetKey (KeyCode.W)) {
			animator.SetBool ("Run", true);
			currentKeyPressed=KeyCode.W;
			// transform.position += -transform.right * (movSpeed * Time.deltaTime);
			Vector3 velocity = -transform.right * movSpeed * Time.deltaTime;
			rb.MovePosition (rb.position + velocity);


			Transform childTransform = transform.FindChild ("characterFinalV2");
			Vector3 newDir = Vector3.RotateTowards (childTransform.forward, transform.forward, 2.0f * Time.deltaTime, 0.0f);
			childTransform.transform.rotation = Quaternion.LookRotation (newDir);


		}
		if (Input.GetKey (KeyCode.S)) {
			currentKeyPressed=KeyCode.S;
			animator.SetBool ("Run", true);
			// transform.position += transform.right * (movSpeed * Time.deltaTime);
			Vector3 velocity = transform.right * movSpeed * Time.deltaTime;
			rb.MovePosition (rb.position + velocity);

			Transform childTransform = transform.FindChild ("characterFinalV2");
			Vector3 newDir = Vector3.RotateTowards (childTransform.forward, -transform.forward, 2.0f * Time.deltaTime, 0.0f);
			childTransform.transform.rotation = Quaternion.LookRotation (newDir);

		}
		if (Input.GetKey (KeyCode.A)) {
			currentKeyPressed = KeyCode.A;
			transform.Rotate (new Vector3(0, -anglesRotate * Time.deltaTime, 0));
		}
		if (Input.GetKey (KeyCode.D)) {
			currentKeyPressed = KeyCode.D;
			transform.Rotate (new Vector3(0, anglesRotate * Time.deltaTime, 0));
		}

		/*if (Input.GetKeyDown(KeyCode.Space)) {
			currentPosition=this.transform.position;

			dash = true;
			startTime = Time.time;
			endMarker= this.transform.position;
			Vector3 auxEnd = endMarker + -transform.right * sprintSpeed;
			endMarker = auxEnd;
			startMarker = this.transform.position;
			journeyLength = Vector3.Distance(startMarker,endMarker);

			// change model
			humanMorph.SetActive (false);
			lightMorph.SetActive (true);
			Instantiate(explosionParticles, transform.position, Quaternion.identity);
			// Poner aqui el sonido de entrar al modo dash
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
				// poner aqui el sonido de salir del modo dash
			}
			transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
		}*/
		if (Input.GetKeyDown(KeyCode.Space) && specialMode == false) {
			currentPosition=this.transform.position;
			dash = true;
			startTime = Time.time;
			endMarker = this.transform.position + -transform.right * sprintSpeed;
			startMarker = this.transform.position;
			journeyLength = Vector3.Distance(startMarker,endMarker);

			// change model
			humanMorph.SetActive (false);
			lightMorph.SetActive (true);
			Instantiate(explosionParticles, transform.position, Quaternion.identity);
			// Poner aqui el sonido de entrar al modo dash
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
				// poner aqui el sonido de salir del modo dash
			}
			// transform.position = ;
			rb.MovePosition(Vector3.Lerp(startMarker, endMarker, fracJourney));
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Enemy"){
			if (dash == true || specialMode == true) {
				GameObject o = enemyExplosionsParticlesObjectPool.GetPooledObject ();
				o.transform.position = other.transform.position;
				o.SetActive (true);
				other.gameObject.SetActive (false);
				// poner aqui el sonido de matar a un enemigo
			} else {
				if (Time.time - timeSinceLastHit > 2.0f) {
					// HURTED BY ENEMY
					timeSinceLastHit = Time.time;
					interfaceCanvas.GetComponent<Interface> ().getHit();
					// poner aqui el sonido de ser golpeado por un enemigo
				}

			}

		}
	}

	IEnumerator ExistSpecialMode(){
		yield return waitLifeTime;
		specialMode = false;
		humanMorph.SetActive (true);
		lightMorph.SetActive (false);
		movSpeed = movSpeed / 1.25f;
		TimeStop.PlayTime ();
	}

	/*void OnCollisionEnter(Collision collision) {
		Vector3 cd = (collision.transform.position - transform.position).normalized;
		Debug.Log (cd);
		rb.velocity = new Vector3 (0.0f, 0.0f, 0.0f);
		rb.angularVelocity = new Vector3 (0.0f, 0.0f, 0.0f);
		rb.AddForce (2.0f * cd);
		rb.AddRelativeForce(2.0f * cd);

	}*/
}
