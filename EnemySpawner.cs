using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemy1;
	public float xMin, xMax;
	public float spawnTime;

	public GameObject[] enemies1;
	public GameObject[] enemies2;
	public GameObject[] enemies3;
	public GameObject[] enemies4;

	public GameObject boss;
	public GameObject bossSpawner;

	bool bossSpawned = false;

	GameObject enemySpawn;
	GameObject gameController;
	GameController gc;
	IEnumerator currentCo;

	bool enemySpawned = false;


	void Start ()
	{
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		gc = gameController.GetComponentInChildren <GameController> ();
		enemySpawn = GameObject.FindGameObjectWithTag ("EnemySpawner");
	}

	void Update ()
	{
		Vector3 spawnPos = new Vector3 (Random.Range (xMin, xMax), 6.5f, 0f);
		Quaternion spawnRot = new Quaternion ();

		if (gc.gameStarted) 
		{
			//Check game progress and spawn enemies
			if (gc.score < 50 && enemySpawned == false) 
			{
				currentCo = SpawnEnemies (enemies1, spawnPos, spawnRot, spawnTime);
				StartCoroutine (currentCo);
			}

			if (gc.score >= 50 && gc.score < 100 && enemySpawned == false) 
			{
				if (currentCo != null) 
				{
					StopCoroutine (currentCo);
				}

				currentCo = SpawnEnemies (enemies2, spawnPos, spawnRot, spawnTime);
				StartCoroutine (currentCo);
			}

			if (gc.score >= 100 && gc.score < 250 && enemySpawned == false) 
			{
				if (currentCo != null) 
				{
					StopCoroutine (currentCo);
				}

				currentCo = SpawnEnemies (enemies3, spawnPos, spawnRot, spawnTime);
				StartCoroutine (currentCo);
			}

			if (gc.score >= 250 && gc.score < 500 && enemySpawned == false) 
			{
				if (currentCo != null) 
				{
					StopCoroutine (currentCo);
				}

				currentCo = SpawnEnemies (enemies4, spawnPos, spawnRot, spawnTime);
				StartCoroutine (currentCo);
			}
			//
			if (gc.score >= 500 && bossSpawned == false)
			{
				GameObject.Instantiate (boss, bossSpawner.transform);
				bossSpawned = true;
			}
		}

	}

	IEnumerator SpawnEnemies (GameObject[] enemies, Vector3 spawnPosition, Quaternion spawnRotation, float spawnDelay)
	{
		int enemyNumber = Random.Range (0, enemies.Length);
		GameObject.Instantiate (enemies[enemyNumber], spawnPosition, spawnRotation);
		enemySpawned = true;
		yield return new WaitForSeconds (spawnDelay);
		enemySpawned = false;
	}
}
