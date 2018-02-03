using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	public class Part
	{
		public Direction direction;
		public int numFrames;
		public float relativeSpeed;

		public Part (Direction d, int numFrames, float spd)
		{
			this.direction = d;
			this.numFrames = numFrames;
			this.relativeSpeed = spd;
		}
	}

	public enum Direction
	{
		East,
		Down,
		West}

	;

	public Part currentPart;
	public Part[] pattern;
	private int stateIx;
	public float basicMoveSpeed;
	public float health;
	public int scoreValue;

	public ScoreKeeper scoreKeeper;

	public Rigidbody2D projectilePrefab;
	public float projectileSpeed = 2f;

	public AudioClip[] deathSounds;

	void Start ()
	{		
		scoreValue = Random.Range (1, 10) * 100;
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
		health = Random.Range (9, 13);
		pattern = new Part[] { 
			new Part (Direction.East, 30, 1f), 
			new Part (Direction.Down, 10, 1f), 
			new Part (Direction.West, 30, 1f), 
			new Part (Direction.Down, 10, 1f)
		};
		stateIx = 0;
		currentPart = pattern [stateIx];
		InvokeRepeating ("FireAtPlayer", Random.Range(0.2f, 2), Random.Range(2.0f, 4));
	}

	void FireAtPlayer(){
		Vector3 zOffset = Vector3.forward;
		Rigidbody2D projectile = Instantiate (projectilePrefab, transform.position + zOffset, Quaternion.identity) as Rigidbody2D;
		projectile.velocity = Vector2.down * projectileSpeed;
	}

	bool isTimeToChange ()
	{
		return (Time.frameCount > 0 && Time.frameCount % 30 == 0);
	}

	void Update ()
	{
		if (isTimeToChange ()) {
			stateIx++;
			if (stateIx >= pattern.Length) {
				stateIx = 0;
			}
			currentPart = pattern [stateIx];
		}
		//moveForPart (currentPart);
	}

	void TakeDamage (float amount)
	{
		health -= amount;
		if (health <= 0) {
			Debug.Log ("Enemy died by taking damage");
			scoreKeeper.Score (scoreValue);
			AudioSource.PlayClipAtPoint(deathSounds[0], transform.position);
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		Laser laser = other.GetComponent<Laser> ();
		if (laser) {
			TakeDamage (laser.GetDamage());
			laser.Hit ();
		}

	}

	void moveForPart (Part p)
	{
		Direction d = p.direction;
		float moveSpeed = basicMoveSpeed * p.relativeSpeed;
		switch (d) {
		case Direction.East: 
			transform.Translate (Vector2.right * moveSpeed * Time.deltaTime);
			break;
		case Direction.West: 
			transform.Translate (Vector2.left * moveSpeed * Time.deltaTime);
			break;
		case Direction.Down: 
			transform.Translate (Vector2.down * moveSpeed * Time.deltaTime);
			break;
		default:
			Debug.LogError ("no such direction: " + d);
			break;
		}
	}
}
