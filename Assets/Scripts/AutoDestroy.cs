using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

	public float lifeTimeSeconds = 2.0f;
	public bool shouldDestroy = true;
	private WaitForSeconds waitLifeTime;

	void OnEnable () {
		waitLifeTime = new WaitForSeconds (lifeTimeSeconds);
		StartCoroutine (Autodestroy ());
	}

	IEnumerator Autodestroy(){
		yield return waitLifeTime;
		if (shouldDestroy)
			Destroy (gameObject);
		else
			gameObject.SetActive (false);
	}
}


