using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayManager : MonoBehaviour {
    public static GameplayManager instance;
    private int score;
    public int levelNumber;
    public bool spawn = true;
    public GameObject[] enemies;
    public Level[] level;
    
    public Text scoreCurrenttText, scoreMaxText;
    public TextMeshProUGUI roundComplete;

    private void Awake() {
        instance = this;
    }
    private void Start() {
        levelNumber = 0;
    }
    void Update() {
        ScoreHolder();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (score >= level[levelNumber].scoreMax) {
            StartCoroutine(LevelUpgrade());
        }
    }
    public void AddScore(int amount) {
        score += amount;
    }
    IEnumerator LevelUpgrade() {
        roundComplete.gameObject.SetActive(true);
        roundComplete.text = "Round " + (levelNumber + 1) + " Complete!";

        score = 0;
        levelNumber++;
        GameObject.Find("Player").GetComponent<Player>().currentWeapon = level[levelNumber].weapon;
        DestoyAllEnemies();
        spawn = false;
        yield return new WaitForSeconds(2);
        roundComplete.gameObject.SetActive(false);
        spawn = true;

    }
    private void DestoyAllEnemies() {
        for (int i = 0; i < enemies.Length; i++) {
            Destroy(enemies[i]);
        }
    }
    public void ScoreHolder() {
        scoreCurrenttText.text = score.ToString();
        scoreMaxText.text = level[levelNumber].scoreMax.ToString();
    }
}
