using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Follow : MonoBehaviour {

    private List<Rigidbody2D> EnemyRBs;
    private Rigidbody2D rb;
    public float speed;
    private float repelRange = .5f;
    private Transform playerPos;

    void Awake() {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        if (EnemyRBs == null) {
            EnemyRBs = new List<Rigidbody2D>();
        }

        EnemyRBs.Add(rb);
    }

    void OnDestroy() {
        EnemyRBs.Remove(rb);
    }

    void Update() {
        if (Vector2.Distance(transform.position, playerPos.position) > 0.15f & Vector2.Distance(transform.position, playerPos.position) < 2.5f)
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
    }

    void FixedUpdate() {
        Vector2 repelForce = Vector2.zero;
        foreach (Rigidbody2D enemy in EnemyRBs) {
            if (enemy == rb)
                continue;

            if (Vector2.Distance(enemy.position, rb.position) <= repelRange) {
                Vector2 repelDir = (rb.position - enemy.position).normalized;
                repelForce += repelDir;
            }

        }
    }
}
