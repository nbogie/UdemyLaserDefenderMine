using UnityEngine;
using UnityEngine.UI;

public class WaveDisplay : MonoBehaviour {
    public EnemySpawner spawner;
    public Text waveText;

    void Update () {
        int wc = spawner.GetWaveCount();
        waveText.text = "Wave "+wc;
	}
}
