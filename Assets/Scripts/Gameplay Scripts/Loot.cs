using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour {
    public GameObject[] loot;
    private float health = 2;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Bullet") {
            health--;
            if (health < 1)
                Destroy(this.gameObject);
        }
    }
    private void OnDestroy() {
        Instantiate(loot[Random.Range(0, loot.Length)], transform.position, Quaternion.identity);
    }
}
