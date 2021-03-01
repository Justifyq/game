using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    private Rigidbody2D myBody;
    public float movementSpeed;
    private Vector2 moveVelocity;
    private Animator legAnim;
    [SerializeField]
    private int health;
    public Weapon currentWeapon;
    private float nextTimeOfFire = 0;
    
    private bool hit = true;
    private Animator anim;

    public AudioClip hitClip;
    public AudioClip deathClip;

    private void Awake() {
        myBody = GetComponent<Rigidbody2D>();
        legAnim = transform.GetChild(2).GetComponent<Animator>();
        anim = GetComponent<Animator>();
        transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = currentWeapon.currentWeaponSpr; 
    }
    private void Update() {
        if (health > 0)
        Rotations();
        Bounds();
        // Shoot
        if (Input.GetMouseButton(0) && health > 0) {
            if (Time.time >= nextTimeOfFire) {
                currentWeapon.Shoot();
                nextTimeOfFire = Time.time + 1 / currentWeapon.fireRate;
            }
        }
    }
    void FixedUpdate() {
        // Momement 
        if (health > 0)
        Movement();
    }
    // WASD movemement
    void Movement() {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * movementSpeed;
        myBody.MovePosition(myBody.position + moveVelocity * Time.fixedDeltaTime);
        // Animation correct change.
        if (moveVelocity == Vector2.zero) {
            legAnim.SetBool("Moving", false);
        }
        else {
            legAnim.SetBool("Moving", true);
        }
    }
    // Player facing mouse position
    void Rotations() {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg + 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);
    }
    // Map bounds
    void Bounds() {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -17.7f, 17.7f), Mathf.Clamp(transform.position.y, -7.2f, 7.2f));
    }
    IEnumerator HitBoxOff() {
        hit = false;
        anim.SetTrigger("Hit");
        yield return new WaitForSeconds(0.7f);
        hit = true;
    }
    void OnTriggerStay2D(Collider2D target) {
       if(target.tag == "Enemy") {
           if (hit) {
               StartCoroutine(HitBoxOff());
               SoundManager.instance.PlaySoundFX(hitClip);
               health--;
               if (health > -1)
               Destroy(GameObject.Find("Life-Box").transform.GetChild(0).gameObject);  
           }
       }
       if(health < 1) {
           StartCoroutine(Death());
       }
    }
    IEnumerator Death() {
        SoundManager.instance.PlaySoundFX(deathClip);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}