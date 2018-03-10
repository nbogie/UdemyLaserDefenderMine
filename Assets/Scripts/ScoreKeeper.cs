using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreKeeper : MonoBehaviour
{
	public Text text;
	private static int score;

	public void Score (int points)
	{
		score += points;
		UpdateText ();

	}

	private void UpdateText ()
	{
		text.text = "" + score;
	}

	void Start ()
	{
		ResetScore ();
	}

    public void ResetScoreAndUpdateText()
    {
        ResetScore();
        UpdateText();
    }

    public static void ResetScore()
    {
        score = 0;
    }
    public static int GetScore()
    {
        return score;
    }

}
