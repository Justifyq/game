using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;
    public AudioSource soundFX, audioTheme;
    public AudioClip[] themeSongs;

    private void Awake() {
        instance = this;    
    }
    void Start() {
        if (!audioTheme.playOnAwake) {
            audioTheme.clip = themeSongs[Random.Range(0, themeSongs.Length)];
            audioTheme.pitch = 0.5f;
            audioTheme.volume = 0.5f;
            audioTheme.Play();
        }
        
    }

    // Update is called once per frame
    void Update() {
         if(!audioTheme.isPlaying) {
             audioTheme.clip = themeSongs[Random.Range(0, themeSongs.Length)];
             audioTheme.Play();
         }
    }
    
    public void PlaySoundFX(AudioClip clip) {
        soundFX.clip = clip;
        soundFX.volume = Random.Range(0.5f, 0.7f);
        soundFX.pitch = Random.Range(0.8f, 1f);
        soundFX.Play();
    }
}
