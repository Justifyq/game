using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAreaTarget : MonoBehaviour{
    public Vector2 top_right_corner;
    public Vector2 bottom_left_corner;
    List<GameObject> enemies;
    
    private Collider2D[] __colider;

    private void Update() {
        __colider = Physics2D.OverlapAreaAll(top_right_corner, bottom_left_corner);
        
        if ( __colider != null) {
            foreach(Collider2D col in __colider) {
                if (col.gameObject.tag == "Enemy") {
                    Debug.Log(col.gameObject.name);
                }
            }
        }
        foreach (Collider2D col in __colider) {
            if (col.tag == "Enemy") {

            }
        }
    }

    private void OnDrawGizmos() {
        CheckEnenemyArea.ColiderArea(top_right_corner, bottom_left_corner);
    }
    private void EnemyChecker() {
        
    }
}
