using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public AudioSource music;
	public AudioSource deathSound;

	[HideInInspector]
	public bool gameStarted = false;
	public bool bossDead = false;
	[HideInInspector]
	public int score = 0;

	[Header ("UI")]
	public TextMeshProUGUI scoreText;
	public GameObject startInstructionsUI;
	public GameObject gameOverUI;
	public GameObject gameWinUI;

	bool gameOver = false;


	GameObject[] enemies;
	GameObject enemy;

	GameObject player;
	PlayerController pc;

	GameObject[] beats;
	GameObject beat;


	void Awake ()
	{
		Time.timeScale = 0;
	}

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		pc = player.GetComponentInChildren <PlayerController> ();
	}

	void Update ()
	{
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		enemy = GameObject.FindGameObjectWithTag ("Enemy");
		beats = GameObject.FindGameObjectsWithTag ("Beat");
		beat = GameObject.FindGameObjectWithTag ("Beat");

		if (gameStarted == false && Input.GetKeyDown (KeyCode.Space)) 
		{
			Time.timeScale = 1;
			music.Play ();
			gameStarted = true;
			startInstructionsUI.SetActive (false);
		}

		if (pc.dead) 
		{
			if (gameOver == false) 
			{
				deathSound.Play ();
			}
			GameOver ();
		}

		if (gameOver && Input.GetKeyDown (KeyCode.Space)) 
		{
			Restart ();
		}

		if (bossDead) 
		{
			GameWin ();
		}

		scoreText.text = score.ToString ();

	}

	void GameOver ()
	{
		gameOver = true;
		music.Stop ();
		gameOverUI.SetActive (true);
		foreach (GameObject enemy in enemies)
		{
			Destroy (enemy);
		}

		Time.timeScale = 0;

	}

	void GameWin ()
	{
		gameOver = true;
		music.Stop ();
		gameWinUI.SetActive (true);
		Time.timeScale = 0;
	}

	void Restart ()
	{
		player.transform.position = new Vector2 (0, 0);
		music.time = 0;
		score = 0;
		gameOver = false;
		gameStarted = false;
		startInstructionsUI.SetActive (true);
		gameOverUI.SetActive (false);
		gameWinUI.SetActive (false);

		foreach (GameObject beat in beats) 
		{
			BeatMover bm = beat.GetComponent <BeatMover> ();
			bm.ResetPosition ();
		}

		pc.Respawn ();
	}

	public void AddScore (int addPoints)
	{
		score += addPoints;
	}

}
