using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
	private float damage;

	void Start ()
	{
		damage = Random.Range (1, 10);
	}

	public float GetDamage ()
	{
		return damage;
	}

	void Update ()
	{
	}

	public void Hit ()
	{
		Destroy (gameObject);
	}

}