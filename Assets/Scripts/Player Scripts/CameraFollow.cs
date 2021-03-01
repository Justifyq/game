using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {   
    private Transform playerPos;

    private void Awake() {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update() {
        // Follow
        transform.position = new Vector3(playerPos.position.x, playerPos.position.y, transform.position.z);
        // Bounds
         transform.position = new Vector3(Mathf.Clamp(transform.position.x, -12, 12), Mathf.Clamp(transform.position.y, -4.8f, 4.8f), transform.position.z);
    }

}
