using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BossController : MonoBehaviour {

	public float speed;
	public float health;
	public GameObject bullet;

	public Image healthBar;
	public TextMeshProUGUI healthText;

	[Header ("Clamp")]
	public float xMin;
	public float xMax;

	public GameObject bulletSpawnOL1, bulletSpawnOL2;
	public GameObject BSOR1, BSOR2;
	public GameObject BSIL1, BSIL2;
	public GameObject BSIR1, BSIR2;
	public GameObject BSmid;

	GameObject gameController;
	GameController gc;

	float startHealth;
	Rigidbody2D rb;
	bool coRuStarted = false;
	float posX;

	float randPercent;


	bool bossFightStart = false;
	bool bossPatternComplete = false;

	void Start ()
	{
		rb = GetComponent <Rigidbody2D> ();

		gameController = GameObject.FindGameObjectWithTag ("GameController");
		gc = gameController.GetComponent <GameController> ();

		// startHealth is used for healthbars
		startHealth = health;

		StartCoroutine (BossStart());
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
			gc.AddScore (666);
			gc.bossDead = true;
			Destroy (gameObject);
		}

		if (bossFightStart && bossPatternComplete == true) 
		{
			StartCoroutine (BossPattern());
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


	IEnumerator BossStart ()
	{
		rb.velocity = Vector2.down * speed;
		yield return new WaitForSeconds (3f);
		bossFightStart = true;
		bossPatternComplete = true;
	}

	IEnumerator BossPattern ()
	{
		bossPatternComplete = false;

		rb.velocity = Vector2.left * speed;
		yield return new WaitForSeconds (0.5f);
		GameObject.Instantiate (bullet, bulletSpawnOL1.transform);
		GameObject.Instantiate (bullet, bulletSpawnOL2.transform);
		GameObject.Instantiate (bullet, BSOR1.transform);
		GameObject.Instantiate (bullet, BSOR2.transform);

		yield return new WaitForSeconds (0.5f);
		rb.velocity = Vector2.right * speed;
		yield return new WaitForSeconds (0.5f);

		GameObject.Instantiate (bullet, BSIL1.transform);
		GameObject.Instantiate (bullet, BSIL2.transform);
		GameObject.Instantiate (bullet, BSIR1.transform);
		GameObject.Instantiate (bullet, BSIR2.transform);
		yield return new WaitForSeconds (0.5f);

		rb.velocity = Vector2.left * speed;

		yield return new WaitForSeconds (0.5f);

		GameObject.Instantiate (bullet, BSmid.transform);
		yield return new WaitForSeconds (0.05f);
		GameObject.Instantiate (bullet, BSmid.transform);
		yield return new WaitForSeconds (0.05f);
		GameObject.Instantiate (bullet, BSmid.transform);
		yield return new WaitForSeconds (0.05f);
		GameObject.Instantiate (bullet, BSmid.transform);
		yield return new WaitForSeconds (0.05f);
		GameObject.Instantiate (bullet, BSmid.transform);
		yield return new WaitForSeconds (0.05f);

		rb.velocity = Vector2.right * speed;

		bossPatternComplete = true;

	}
}
