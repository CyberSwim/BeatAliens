using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BeatCheck : MonoBehaviour {

	public TextMeshProUGUI multText;
	public GameObject shot;
	[HideInInspector]
	public int multiplier;

	public Sprite successSprite;
	public Sprite failSprite;

	public GameObject[] beats;
	public GameObject beatObj;
	BeatMover bm;


	GameObject shotSpawner;
	SpriteRenderer beater;
	bool beat;


	void Start ()
	{
		shotSpawner = GameObject.FindGameObjectWithTag ("ShotSpawner");
		beater = GetComponentInChildren <SpriteRenderer> ();

		beats = GameObject.FindGameObjectsWithTag ("Beat");
	}

	void Update ()
	{
		//Shot spawn position
		Transform shotSpawn = shotSpawner.transform;

		beatObj = GameObject.FindGameObjectWithTag ("Beat");


		//Checks beatmatch + instantiates bullet if successful
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (beat) 
			{
				GameObject.Instantiate (shot, shotSpawn);

				if (multiplier < 25) 
				{

					multiplier = multiplier + 1;
				}

				beater.sprite = successSprite;

				//Audio and beats were losing sync over time, this is the only fix I could think of! D:
				foreach (GameObject beat in beats) 
				{
					bm = beat.GetComponent <BeatMover> ();
					bm.ResetPosition ();
				}

				//Prevents multiple shots
				beat = false;

				//Stops multiplier resetting
				return;
			}
			if (beat == false)
			{
				beater.sprite = failSprite;
				multiplier = 0;
			}

		}

		// Multiplier GUI
		multText.text = "X " + multiplier;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("Beat"))
		{
			beat = true;
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.CompareTag ("Beat")) 
		{
			beat = false;
		}

	}
}
