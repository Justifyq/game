using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource soundFX, audioTheme;

    public AudioClip[] themeSongs;

    void Awake() {
        instance = this;
    }

    private void Start() {
        if (!audioTheme.playOnAwake) {
            audioTheme.clip = themeSongs[Random.Range(0, themeSongs.Length)];
            audioTheme.Play();
        }
    }

    void Update() {
        if (!audioTheme.isPlaying) {
            audioTheme.clip = themeSongs[Random.Range(0, themeSongs.Length)];
            audioTheme.Play();
        }
    }

    public void PlaySoundFX(AudioClip clip) {
        soundFX.clip = clip;
        soundFX.volume = Random.Range(.3f, .5f);
        soundFX.Play();
    }

}
