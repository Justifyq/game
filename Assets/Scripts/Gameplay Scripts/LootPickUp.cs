using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPickUp : MonoBehaviour {
    public bool ammo, hp;
    private GameObject player, lifebox, lifebar;
    public GameObject life;

    void Awake() {
        player = GameObject.Find("Player");    
        lifebox = GameObject.Find("Life-Box");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            if (hp)
                Health();
            if (ammo)
                Ammo();
        }
    }

    private void Health() {
        Destroy(this.gameObject);
        if (player.GetComponent<Player>().health < 8) {
            Vector3 zeroPos = new Vector3 (1,1,1);
            player.GetComponent<Player>().health++;
            lifebar = Instantiate(life, zeroPos, Quaternion.identity);
            lifebar.transform.SetParent(lifebox.transform);
            lifebar.transform.localScale = zeroPos;
        }
        
    }   
    private void Ammo() {
        Destroy(this.gameObject);
        player.GetComponent<Player>().reload = true;
    }
}
