using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMover : MonoBehaviour {

	public float tempo;

	GameObject gameController;
	GameController gc;

	GameObject beatSpawn;
	bool moveBeat;

	Rigidbody2D rb;
	Vector3 startPos;


	void Start ()
	{
		rb = gameObject.GetComponent <Rigidbody2D> ();

		beatSpawn = GameObject.FindGameObjectWithTag ("BeatSpawner");

		gameController = GameObject.FindGameObjectWithTag ("GameController");
		gc = gameController.GetComponent <GameController> ();

		startPos = gameObject.transform.position;
	}

	void FixedUpdate ()
	{
		//Finds correct speed for a given tempo
		float speed = (tempo / 60) * 5;

		if (gc.gameStarted) 
		{
			rb.velocity = (Vector2.left * speed);
		}

		//Moves beat back to screen right when it hits teleporter
		if (moveBeat) 
		{
			rb.position = beatSpawn.transform.position;
		}
	}

	//Checks for beat teleporter
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("BeatPorter"))
			{
				moveBeat = true;
			}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.CompareTag ("BeatPorter")) 
		{
			moveBeat = false;
		}
	}

	//Used for GameController RestartGame ()
	public void ResetPosition ()
	{
		rb.transform.position = startPos;
	}

}
