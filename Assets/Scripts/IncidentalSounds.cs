using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncidentalSounds : MonoBehaviour
{

	public AudioClip[] titleSounds;

	void Start ()
	{
		PlayTitleScreenSound ();	
	}

	void PlayTitleScreenSound ()
	{
		AudioSource.PlayClipAtPoint (titleSounds [Random.Range (0, titleSounds.Length)], new Vector3 (0, 0, 0));

	}
}
