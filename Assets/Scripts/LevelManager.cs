using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

	public void LoadLevel (string name)
	{
		Debug.Log ("New Level load: " + name);
		SceneManager.LoadScene (name);
	}

	public void GotoLoseScreen ()
	{
		SceneManager.LoadScene ("LoseScreen");
	
	}

	public void GotoLoseScreenAfterDelay (float time)
	{
		Invoke ("GotoLoseScreen", time);
	}

	public void QuitRequest ()
	{
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

}
