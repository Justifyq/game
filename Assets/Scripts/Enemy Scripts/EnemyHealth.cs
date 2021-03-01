using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    [SerializeField]
    private float health;
    public int scoreReward;

    public AudioClip deathClip;

    void Start() {
        
    }

    void Update() {
        if (health < 1) {
            GameplayManager.instance.AddScore(scoreReward);
            Destroy(this.gameObject);
            SoundManager.instance.PlaySoundFX(deathClip);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Bullet") {
            health -= GameObject.Find("Player").GetComponent<Player>().currentWeapon.damage;
            Destroy(other.gameObject);
        }
    }
}
