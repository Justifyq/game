using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour {
    private bool isFullScreen = true;
    Resolution[] rsl;
    List<string> resolutions;
    public Dropdown dropdown;
    public AudioSource au;
    [HideInInspector]
    public float audioValue;
    private void Awake() {
        resolutions = new List<string>();
        rsl = Screen.resolutions;
        foreach(var i in rsl) {
            resolutions.Add(i.width + "x" + i.height);
        }
        dropdown.ClearOptions();
        dropdown.AddOptions(resolutions);
    }
    public void FullScreen() {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }
    public void Audio(float value) {
        au.volume = value;
    }
    public void Quality(int q) {
        QualitySettings.SetQualityLevel(q);
    }
    public void Resolutin(int r) {
        Screen.SetResolution(rsl[r].width, rsl[r].height, isFullScreen);
    }
}
 