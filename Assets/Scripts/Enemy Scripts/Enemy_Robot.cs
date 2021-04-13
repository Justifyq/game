﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Robot : MonoBehaviour
{

    public Transform bulletPos1, bulletPos2;
    public GameObject bullet;

    private Transform playerPos;
    private Rigidbody2D rb;

    public float speed = .3f;

    private bool isInRange = false;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start() {
        StartCoroutine(Shoot());
    }

    void Update() {
        if (Vector2.Distance(transform.position, playerPos.position) > 2.5f) { 
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
            isInRange = false;
        }
        else {
            isInRange = true;
        }
    }

    void FixedUpdate() {
        Rotation();
    }

    void Rotation() {
        Vector2 direction = (playerPos.gameObject.GetComponent<Rigidbody2D>().position - rb.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        rb.rotation = angle;
    }

    IEnumerator Shoot() {
        if (isInRange)
            Instantiate(bullet, bulletPos1.position, transform.rotation);
        yield return new WaitForSeconds(.3f);
        if (isInRange)
            Instantiate(bullet, bulletPos2.position, transform.rotation);
        yield return new WaitForSeconds(.3f);
        StartCoroutine(Shoot());
    }
}
