using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
    public AudioClip[] soundtracks;

    void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}

    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int level = scene.buildIndex;
        Debug.Log("level loaded: " + level);
        if (level < soundtracks.Length){
            AudioClip clip = soundtracks[level];
            AudioSource source = GetComponent<AudioSource>();
            source.clip = clip;
            source.Play();
        }

	}

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
