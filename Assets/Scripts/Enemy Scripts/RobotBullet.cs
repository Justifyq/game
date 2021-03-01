using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBullet : MonoBehaviour {
    [SerializeField]
    private float speed;
    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();    
    }
    private void Start() {
        rb.velocity = transform.up * -speed;
    }
    private void Update() {
        
    }
}
