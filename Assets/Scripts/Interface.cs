using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour {

	public GameObject gameOverPanel;
	public GameObject h1;
	public GameObject h2;
	public GameObject h3;
	int currentLifes = 3;
	public RectTransform energyBar;
	bool activateEnergy = false;
	Vector2 normalScale = Vector2.zero;
	float valueScale;
	float decreaseValue = 0.665f;
	float increaseValue = 0.25f;
	bool setAgainEnergy = false;
	bool isGameOver = false;

	float beginActivationTime = 0.0f;
	public GameObject player = null;

	// Use this for initialization
	void Start () {
		normalScale = energyBar.sizeDelta;
	}

	// Update is called once per frame
	void Update () {

		if (isGameOver) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				//Time.timeScale = 1;
				//reiniciar juego

			}
		}

		////////PARA PROBAR/////////////////
		/*if (Input.GetKeyDown (KeyCode.Y)) {
			activeEnergy ();
		}*/
		if (Input.GetKeyDown (KeyCode.U)) {
			getHit ();
		}
		////////////////////////////////////

		if (activateEnergy) {
			if (energyBar.sizeDelta.x <= 0) {
				activateEnergy = false;
				valueScale = 0f;
				setAgainEnergy=true;
				beginActivationTime = Time.time;
			} else {
				//valueScale -= decreaseValue;
				//energyBar.sizeDelta = new Vector2 (valueScale,normalScale.y);
				energyBar.sizeDelta = Vector2.Lerp(new Vector2(100, normalScale.y), new Vector2(0, normalScale.y),(Time.time - beginActivationTime) / 5.0f);
				Debug.Log (energyBar.sizeDelta.x);
			}
		}

		if(setAgainEnergy){
			if (energyBar.sizeDelta.x >= 100) {
				setAgainEnergy = false;
			} 
			else {
				energyBar.sizeDelta = Vector2.Lerp(new Vector2(0, normalScale.y), new Vector2(100, normalScale.y),(Time.time - beginActivationTime) / 10.0f);
				//valueScale += increaseValue;
				//energyBar.sizeDelta = new Vector2 (valueScale,normalScale.y);
			}
		}
	}

	public void activeEnergy(){
		activateEnergy = true;
		valueScale = normalScale.x;
		beginActivationTime = Time.time;
	}

	public void gameOver(){
		isGameOver = true;
		gameOverPanel.SetActive (true);
		TimeStop.StopTime (false);
		player.GetComponent<Movement> ().enabled = false;
		player.transform.FindChild ("LightMorph").gameObject.SetActive (false);
		player.transform.FindChild ("characterFinalV2").gameObject.SetActive (false);
		//gameOverPanel.GetComponent<Animator> ().Play ("Gm");
	}
		
	public void getHit(){
		switch (currentLifes)
		{
		case 3:
			h3.SetActive (false);
			currentLifes--;
			break;
		case 2:
			h2.SetActive (false);
			currentLifes--;
			break;
		case 1:
			gameOver ();
			break;
		default:
			break;
		}
	}

}
