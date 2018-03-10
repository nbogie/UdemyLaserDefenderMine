using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	void Start () {
        Text t = GetComponent<Text>();
        t.text = "" + ScoreKeeper.GetScore();
	}
	
	void Update () {
		
	}
}
