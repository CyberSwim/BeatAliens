using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour {

	public float shotSpeed;
	[HideInInspector]
	public int damage;

	GameObject beatChecker;
	BeatCheck bc;
	Rigidbody2D rb;

	void Start ()
	{
		rb = GetComponent <Rigidbody2D> ();

		beatChecker = GameObject.FindGameObjectWithTag ("BeatChecker");
		bc = beatChecker.GetComponentInChildren <BeatCheck> ();

		// Set in Start () so that damage won't increase after it has been created (shot)
		damage = bc.multiplier;
	}

	void Update ()
	{
		// Cleanup for stray shots
		Destroy (gameObject, 5f);
	}

	void FixedUpdate ()
	{
		rb.velocity = (Vector2.up * shotSpeed);
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.CompareTag ("Enemy")) 
		{
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("BulletSponge")) 
		{
			Destroy (gameObject);
		}

		//Stops shots colliding
		if (other.CompareTag ("EnemyShot")) 
		{
			Destroy (gameObject);
		}
	}
}
