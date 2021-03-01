using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
    private bool isPause = false;
    private bool isSettings = false;
    public GameObject pauseMenu;
    public GameObject settingMenu;
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPause) {
                if (isSettings) {
                    settingMenu.SetActive(false);
                    pauseMenu.SetActive(true);
                    isSettings = !isSettings;
                }
                else
                    Resume();
            }
            else
                SetPause();
        } 
    }
    private void SetPause() {
        isPause = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void Resume() {
        isPause = false;
        pauseMenu.SetActive(false);
        settingMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ExitPressed() {
        Application.Quit();
    }
    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
    public void Settings() {
        settingMenu.SetActive(true);
        pauseMenu.SetActive(false);
        isSettings = !isSettings;
    }
}
