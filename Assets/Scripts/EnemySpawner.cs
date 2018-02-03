using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject enemyPrefab;
	public float width = 8;
	public float height = 5;
	private float rightEdge;
	private float leftEdge;
	public float moveSpeed;
	public bool movingRight;
	public float spawnDelay = 0.4f;


	void Start ()
	{
		SpawnUntilFull ();
	}

	Transform FindNextEmptyPosition ()
	{
		foreach (Transform childPosition in transform) {
			if (childPosition.childCount == 0) {
				return childPosition;
			}
		}
		return null;
	}

	void SpawnUntilFull ()
	{
		leftEdge = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0)).x;
		rightEdge = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, 0)).x;
		Transform nextSlot = FindNextEmptyPosition ();
		if (nextSlot) {
			GameObject enemy = Instantiate (enemyPrefab, nextSlot) as GameObject;
			enemy.transform.parent = nextSlot;
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}

	void Update ()
	{
		if (transform.position.x + width / 2 >= rightEdge) {
			movingRight = false;
		}
		if (transform.position.x - width / 2 <= leftEdge) {
			movingRight = true;
		}
		transform.Translate (new Vector3 (movingRight ? 1 : -1, 0, 0) * moveSpeed * Time.deltaTime);
		if (AllMembersAreDead ()) {
			SpawnUntilFull ();
		}

	}

	void OnDrawGizmos ()
	{
		Gizmos.DrawWireCube (transform.position, new Vector3 (width, height, 0));

	}

	bool AllMembersAreDead ()
	{
		foreach (Transform childPosition in transform) {
			if (childPosition.childCount > 0) {
				return false;

			}
		}
		return true;
	}
}

