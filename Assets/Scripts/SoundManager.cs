using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance = null;

    public AudioSource musicSource;
    public AudioSource efxSource;

    void Awake () {
	    if(instance == null) {
            instance = this;
        }
        else {
            if(instance != this) {
                Destroy(this.gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
	}

    public void PlaySingle(AudioClip clip) {
        if (clip == null) return;
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void PlayMusic(AudioClip clip) {
        if (clip == null) return;
        musicSource.clip = clip;
        musicSource.Play();
    }

}
