using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float xMin, xMax, yMin, yMax;

	[HideInInspector]
	public bool dead = false;

	public TextMeshProUGUI healthText;
	public Image healthBar;

	int health = 10;
	int startHealth;
	Rigidbody2D rb;

	GameObject beatChecker;
	BeatCheck bc;

	void Start ()
	{
		rb = GetComponent <Rigidbody2D> ();
		startHealth = health;
		beatChecker = GameObject.FindGameObjectWithTag ("BeatChecker");
		bc = beatChecker.GetComponent <BeatCheck> ();
	}

	void FixedUpdate ()
	{
		// Move player up down left right * speed
		if (Input.GetKey (KeyCode.A)) 
		{
			rb.AddForce (Vector2.left * speed);
		}
		if (Input.GetKey (KeyCode.D))
		{
			rb.AddForce (Vector2.right * speed);
		}
		if (Input.GetKey (KeyCode.W)) 
		{
			rb.AddForce (Vector2.up * speed);
		}
		if (Input.GetKey (KeyCode.S)) 
		{
			rb.AddForce (Vector2.down * speed);
		}

		// Clamps player within defined area
		rb.position = new Vector2 (Mathf.Clamp (rb.position.x, xMin, xMax), Mathf.Clamp (rb.position.y, yMin, yMax));
	}

	void Update ()
	{
		// Health and health UI
		healthText.text = health + " / " + startHealth;

		if (health <= 0) 
		{
			gameObject.SetActive (false);
			dead = true;
			healthText.text = "R.I.P";
		}

		healthBar.fillAmount = (float)health / (float)startHealth;

	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.CompareTag ("Enemy"))
		{
			health = health - 2;
			bc.multiplier = 0;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("EnemyShot"))
			{
				health = health - 1;
			bc.multiplier = 0;
			}
	}

	// Used for GameController RestartGame ()
	public void Respawn ()
	{
		dead = false;
		health = 10;
		rb.transform.position = new Vector2 (0, 0);
		gameObject.SetActive (true);
	}
}
