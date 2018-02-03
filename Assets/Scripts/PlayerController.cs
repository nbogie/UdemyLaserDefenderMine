using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public KeyCode leftKey;
	public KeyCode rightKey;
	public KeyCode upKey;
	public KeyCode downKey;
	public KeyCode fireKey;
	public LevelManager levelManager;

	public float moveSpeed;
	public float health;

	public Rigidbody2D laserPrefab;

	private float xMin;
	private float xMax;

	private float timeLaserIsNextReady = -1;
	[Range (0.1f, 2)]
	public float laserCooloffTime = 0.5f;

	public float laserMoveSpeed = 3;
	public AudioClip[] dmgSounds;
	public AudioClip[] deathSounds;

	public ParticleSystem dmgParticlesPrefab;
	private ParticleSystem dmgParticles;

	private CameraShaker cameraShaker;

	void PlayDamageSound (Vector3 pos)
	{
		AudioSource.PlayClipAtPoint (dmgSounds [Random.Range (0, dmgSounds.Length)], pos);
	}

	void PlayDeathSound (Vector3 pos)
	{
		AudioSource.PlayClipAtPoint (deathSounds [Random.Range (0, deathSounds.Length)], pos);
	}

	void Start ()
	{		
		dmgParticles = Instantiate (dmgParticlesPrefab) as ParticleSystem;
		cameraShaker = Camera.main.GetComponent<CameraShaker> ();
		levelManager = FindObjectOfType<LevelManager> ();
		health = 100;
		float zDist = transform.position.z - Camera.main.transform.position.z;
		xMin = Camera.main.ViewportToWorldPoint (new Vector3 (0.05f, 0, zDist)).x;
		xMax = Camera.main.ViewportToWorldPoint (new Vector3 (0.95f, 0, zDist)).x;
	}

	void move (Vector2 direction)
	{
		transform.Translate (direction * moveSpeed * Time.deltaTime);
		float x = Mathf.Clamp (transform.position.x, xMin, xMax);
		transform.position = new Vector2 (x, transform.position.y);
	}

	void Update ()
	{
		if (Input.GetKey (upKey)) {
			move (Vector2.up);
		}
		if (Input.GetKey (downKey)) {
			move (Vector2.down);
		}
		if (Input.GetKey (leftKey)) {
			move (Vector2.left);
		}
		if (Input.GetKey (rightKey)) {
			move (Vector2.right);
		}
		if (Input.GetKey (fireKey)) {
			fireWeapon ();
		}
	}


	void fireWeapon ()
	{

		if (Time.time > timeLaserIsNextReady) {
			Vector3 zOffset = Vector3.forward;
			Rigidbody2D bullet = Instantiate (laserPrefab, transform.position + zOffset, Quaternion.identity) as Rigidbody2D;
			bullet.GetComponent<Rigidbody2D> ().velocity = Vector2.up * laserMoveSpeed;
			timeLaserIsNextReady = Time.time + laserCooloffTime;

		}
	}

	void TakeDamage (float damage)
	{
		health -= damage;
		if (health <= 0) {
			Die ();
		} else {
			PlayDamageSound (transform.position);
			cameraShaker.ShakeCamera (1.0f, 0.4f);

		}
	}

	void Die ()
	{

		levelManager.GotoLoseScreenAfterDelay (3);
		PlayDeathSound (transform.position);
		Destroy (gameObject);
	}

	void OnTriggerEnter2D (Collider2D other)
	{

		EnemyLaser enemyLaser = other.GetComponent<EnemyLaser> ();
		if (enemyLaser) {
			TakeDamage (enemyLaser.GetDamage ());
			dmgParticles.transform.position = other.transform.position;
			dmgParticles.Play ();
			enemyLaser.Hit ();
		}
	}

}
