using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {
    private float speed = 4f;
    private Vector2 dir;
    private Vector2 curDir;

    void Start() {
        dir = GameObject.Find("Direction").transform.position;
        transform.position = GameObject.Find("FirePoint").transform.position;
        transform.eulerAngles = new Vector3(0, 0, GameObject.Find("Player").transform.eulerAngles.z);
    }

    void Update() {
        transform.position = Vector2.MoveTowards(transform.position, dir, speed * Time.deltaTime);
        // Self Destroying
        curDir = transform.position;
        if (curDir == dir) {
            Destroy(this.gameObject);
        }
    }
}
