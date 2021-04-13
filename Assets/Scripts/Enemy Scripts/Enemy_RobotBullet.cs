using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RobotBullet : MonoBehaviour {
    public float speed;
    private Rigidbody2D rb;    
    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start() {
        rb.velocity = transform.up * -speed;
    }
}
