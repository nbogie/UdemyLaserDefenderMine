using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
	private float shakeUntil;
	private float shakeAmount;

	Vector3 originalPosition;

	void Start ()
	{
		//TOO: don't assume camera won't be otherwise moved during course of game
		originalPosition = transform.position;
		shakeUntil = -1;
	}

	//amount from 0.0 to 1.0
	//duration in seconds
	public void ShakeCamera (float amount, float durationSec)
	{
		Mathf.Clamp (amount, 0f, 1f);
		shakeUntil = Time.time + durationSec;
		shakeAmount = amount;
	}


	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.S)) {
			ShakeCamera (1, Random.Range (0.3f, 3));
		}
		float sa = shakeAmount;
		Vector3 randomOffset = new Vector3 (Random.Range (-sa, sa), Random.Range (-sa, sa), 0);
		if (shakeUntil > Time.time) {
			transform.position = originalPosition + randomOffset;
		} else {
			transform.position = originalPosition;
		}
	}
}
