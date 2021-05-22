using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyController : MonoBehaviour {

	public float speed;
	public float health;
	public GameObject bullet;
	public GameObject bulletSpawner;

	public Image healthBar;
	public TextMeshProUGUI healthText;

	[Header ("Clamp")]
	public float xMin;
	public float xMax;

	GameObject gameController;
	GameController gc;

	float startHealth;
	Rigidbody2D rb;
	bool coRuStarted = false;
	float posX;

	float randPercent;

	void Start ()
	{
		rb = GetComponent <Rigidbody2D> ();

		gameController = GameObject.FindGameObjectWithTag ("GameController");
		gc = gameController.GetComponent <GameController> ();

		// startHealth is used for healthbars
		startHealth = health;

		if (gameObject.name == "enemy1(Clone)") 
		{
			StartCoroutine (Enemy1 ());
		}

		if (gameObject.name == "enemy2(Clone)") 
		{
			StartCoroutine (Enemy2 ());
		}

		if (gameObject.name == "enemy3(Clone)") 
		{
			StartCoroutine (Enemy3 ());
		}

		if (gameObject.name == "enemy4(Clone)") 
		{
			StartCoroutine (Enemy4 ());
		}
	}

	void Update ()
	{
		randPercent = Random.Range (1, 100);

		posX = rb.transform.position.x;

		//Stops enemies flying offscreen
		rb.position = new Vector2 (Mathf.Clamp (rb.position.x, xMin, xMax), rb.position.y);

		// Healthbar
		healthBar.fillAmount = health / startHealth;
		healthText.text = health + "/" + startHealth;

		if (health <= 0) 
		{
			// Gamecontroller add score
			if (gameObject.name == "enemy1(Clone)") 
			{
				gc.AddScore (10);
			}

			if (gameObject.name == "enemy2(Clone)") 
			{
				gc.AddScore (15);
			}
				
			if (gameObject.name == "enemy3(Clone)") 
			{
				gc.AddScore (20);
			}

			if (gameObject.name == "enemy4(Clone)") 
			{
				gc.AddScore (25);
			}
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("Shot")) 
		{
			ShotScript shot = other.GetComponentInChildren <ShotScript> ();
			health = health - shot.damage;
		}

		if (other.CompareTag ("Killer")) 
		{
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.CompareTag ("Player"))
			{
				Destroy (gameObject);
			}
	}

	//Enemy behaviours
	IEnumerator Enemy1 ()
	{
		rb.velocity = Vector2.down * speed;
		yield return null;
	}

	IEnumerator Enemy2 ()
	{
		rb.velocity = Vector2.down * speed;
		yield return new WaitForSeconds (2.5f);

		if (randPercent < 50)
		{
			rb.velocity = Vector2.left * speed;
		}
		if (randPercent > 50)
		{
			rb.velocity = Vector2.right * speed;
		}

		yield return new WaitForSeconds (1f);
		rb.velocity = Vector2.down * speed;

		yield return null;
	}

	IEnumerator Enemy3 ()
	{
		rb.velocity = Vector2.down * speed;
		yield return new WaitForSeconds (1.2f);

		if (randPercent < 50)
		{
			rb.velocity = Vector2.left * speed;

			yield return new WaitForSeconds (0.6f);

			rb.velocity = Vector2.right * speed;

			yield return new WaitForSeconds (0.5f);

			GameObject.Instantiate (bullet, bulletSpawner.transform);

			yield return new WaitForSeconds (0.6f);

			rb.velocity = Vector2.down * speed;	
		}
	
		if (randPercent > 50) 
		{
			rb.velocity = Vector2.right * speed;

			yield return new WaitForSeconds (0.6f);

			rb.velocity = Vector2.left * speed;

			yield return new WaitForSeconds (0.3f);

			GameObject.Instantiate (bullet, bulletSpawner.transform);

			yield return new WaitForSeconds (0.6f);

			rb.velocity = Vector2.down * speed;

		}
	}

	IEnumerator Enemy4 ()
	{
		rb.velocity = Vector2.down * speed;
		yield return new WaitForSeconds (1.2f);

		if (randPercent < 50)
		{
			rb.velocity = Vector2.left * speed;

			yield return new WaitForSeconds (0.6f);

			rb.velocity = Vector2.right * speed;

			yield return new WaitForSeconds (0.5f);

			GameObject.Instantiate (bullet, bulletSpawner.transform);
			yield return new WaitForSeconds (0.1f);
			GameObject.Instantiate (bullet, bulletSpawner.transform);
			yield return new WaitForSeconds (0.1f);
			GameObject.Instantiate (bullet, bulletSpawner.transform);

			yield return new WaitForSeconds (0.6f);

			rb.velocity = Vector2.down * speed;	
		}

		if (randPercent > 50) 
		{
			rb.velocity = Vector2.right * speed;

			yield return new WaitForSeconds (0.6f);

			rb.velocity = Vector2.left * speed;

			yield return new WaitForSeconds (0.3f);

			GameObject.Instantiate (bullet, bulletSpawner.transform);
			yield return new WaitForSeconds (0.1f);
			GameObject.Instantiate (bullet, bulletSpawner.transform);
			yield return new WaitForSeconds (0.1f);
			GameObject.Instantiate (bullet, bulletSpawner.transform);

			yield return new WaitForSeconds (0.6f);

			rb.velocity = Vector2.down * speed;

		}
	}
}
