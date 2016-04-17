using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class TimeStop {

	public static void StopTime(){

		// stop spawners
		GameObject[] spawnersArray = GameObject.FindGameObjectsWithTag("EnemySpawner");
		for (int i = 0; i < spawnersArray.Length; ++i) {
			spawnersArray [i].GetComponent<SpawnEnemy> ().StopTime = true;
		}

		// stop enemmies
		GameObject[] enemiesArray = GameObject.FindGameObjectsWithTag("Enemy");
		for (int i = 0; i < enemiesArray.Length; ++i) {
			enemiesArray [i].GetComponent<MoveTowards> ().StopTime = true;
			enemiesArray [i].GetComponentInChildren<Animator>().enabled = false;
		}

		// stop explosion particles
		GameObject[] explosionArray = GameObject.FindGameObjectsWithTag("ExplosionParticles");
		for (int i = 0; i < explosionArray.Length; ++i) {
			explosionArray [i].GetComponent<ParticleSystem> ().Pause ();
		}

		// stop enemy explosion particles
		GameObject[] enemyExplosionArray = GameObject.FindGameObjectsWithTag("EnemyExplosionParticles");
		for (int i = 0; i < enemyExplosionArray.Length; ++i) {
			enemyExplosionArray [i].GetComponent<ParticleSystem> ().Pause ();
		}

		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<InvertColorsEffect> ().enabled = true;

		return;
	}

	public static void PlayTime(){

		// stop spawners
		GameObject[] spawnersArray = GameObject.FindGameObjectsWithTag("EnemySpawner");
		for (int i = 0; i < spawnersArray.Length; ++i) {
			spawnersArray [i].GetComponent<SpawnEnemy> ().StopTime = false;
		}

		// stop enemmies
		GameObject[] enemiesArray = GameObject.FindGameObjectsWithTag("Enemy");
		for (int i = 0; i < enemiesArray.Length; ++i) {
			enemiesArray [i].GetComponent<MoveTowards> ().StopTime = false;
			enemiesArray [i].GetComponentInChildren<Animator> ().enabled = true;
		}

		// stop explosion particles
		GameObject[] explosionArray = GameObject.FindGameObjectsWithTag("ExplosionParticles");
		for (int i = 0; i < explosionArray.Length; ++i) {
			explosionArray [i].GetComponent<ParticleSystem> ().Play ();
		}

		// stop enemy explosion particles
		GameObject[] enemyExplosionArray = GameObject.FindGameObjectsWithTag("EnemyExplosionParticles");
		for (int i = 0; i < enemyExplosionArray.Length; ++i) {
			enemyExplosionArray [i].GetComponent<ParticleSystem> ().Play ();
		}

		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<InvertColorsEffect> ().enabled = false;

		return;
	}

}
