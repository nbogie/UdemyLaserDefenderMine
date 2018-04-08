using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayScript : MonoBehaviour {
    private Text text;
    public PlayerController player;

	void Start () {
    text = GetComponent<Text>();

	}
	
	void Update () {
        text.text = "Health: "+ Mathf.Round(player.GetHealth());
	}
}
