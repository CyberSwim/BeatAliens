using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotScript : MonoBehaviour {

	public float speed;

	Rigidbody2D rb;

	void Start ()
	{
		rb = GetComponent <Rigidbody2D> ();
	}

	void FixedUpdate ()
	{
		rb.velocity = Vector2.down * speed;
	}

	void Update ()
	{
		Destroy (gameObject, 10f);
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.CompareTag ("Player"))
		{
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("Killer"))
			{
				Destroy (gameObject);
			}

		//Stops shots colliding
		if (other.CompareTag ("Shot")) 
		{
			Destroy (gameObject);
		}
	}
}
