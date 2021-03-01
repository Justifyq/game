using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField]
    private float spawnRadius = 7f, time = 1.5f;
    
    public GameObject[] enemies;

    private void Start() {
        StartCoroutine(SpawnAnEnemy());    
    }

    IEnumerator SpawnAnEnemy() {
        Vector2 spawnPositin = GameObject.Find("Player").transform.position;
        spawnPositin += Random.insideUnitCircle.normalized * spawnRadius;

        if (GameplayManager.instance.spawn)
            if (Random.value < 0.2 && GameplayManager.instance.levelNumber >= 2)
                Instantiate(enemies[2]);
            else 
                Instantiate(enemies[Random.Range(0, enemies.Length - 1)],spawnPositin, Quaternion.identity);

        yield return new WaitForSeconds(time);
        StartCoroutine(SpawnAnEnemy());
    }
 }
