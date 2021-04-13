using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField]
    private float spawnRadius = 7, time = 1.5f;
    public GameObject[] enemies;

    void Start() {
        StartCoroutine(SpawnAnEnemy());
    }

    IEnumerator SpawnAnEnemy() {
        Vector2 spawnPos = GameObject.Find("Player").transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
        if (enemies.Length < 5)
            Instantiate(enemies[Random.Range(0, enemies.Length - 1)], spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(time);
        StartCoroutine(SpawnAnEnemy());
    }
}
