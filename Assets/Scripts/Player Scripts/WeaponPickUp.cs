using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour {
    public Weapon weapon;

    private void OnTriggerEnter2D(Collider2D player) {
        if (player.tag == "Player") {
            player.GetComponent<Player>().currentWeapon = weapon;
            player.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = weapon.currentWeaponSpr;
            Destroy(gameObject);
        }
    }
}
