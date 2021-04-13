using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public Weapon currentWeapon;
    private Rigidbody2D myBody;
    private Animator anim;
    [HideInInspector]
    public Animator legAnim;
    public float speed;
    public int health;
    private Vector2 moveVelocity;
    private float nextTimeOfFire = 0;
    private bool hit = true;
    public AudioClip hitClip, deathClip;
    private float Shots;
    [HideInInspector]
    public bool reload, isAble = true;

	void Awake () {
        myBody = GetComponent<Rigidbody2D>();
        legAnim = transform.GetChild(2).GetComponent<Animator>();
        anim = GetComponent<Animator>();
        transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = currentWeapon.currentWeaponSpr;
    }

    private void Update() {
        Bounds();
        if (health > 0 & isAble) {
            Rotation();
            Shoot();
        }
    }

    void FixedUpdate () {
        if (health > 0 & isAble)
            Movement();
	}

    void Rotation() {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);
    }

    void Movement() {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        myBody.MovePosition(myBody.position + moveVelocity * Time.fixedDeltaTime);
      //  transform.position = myBody.transform.position;

        if (moveVelocity == Vector2.zero)
            legAnim.SetBool("Moving", false);
        else
            legAnim.SetBool("Moving", true);

    }

    void Shoot() {
        if (reload)
            StartCoroutine(Reload());

        if (Input.GetMouseButton(0) & !reload) {
            if (Shots <= currentWeapon.weaponAmmu) {
                if (Time.time >= nextTimeOfFire) {
                    Shots++;
                    currentWeapon.Shoot();
                    nextTimeOfFire = Time.time + 1 / currentWeapon.fireRate;
                }
            }
        }
    }
    
    void Bounds() {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -6.2f, 6.2f), Mathf.Clamp(transform.position.y, -4.72f, 4.72f));
    }

    IEnumerator HitBoxOff() {
        hit = false;
        anim.SetTrigger("Hit");
        yield return new WaitForSeconds(2);
        anim.ResetTrigger("Hit");
        hit = true;
    }

    void OnTriggerEnter2D(Collider2D target) {
        if(target.tag == "Enemy") {
            if (hit) {
                SoundManager.instance.PlaySoundFX(hitClip);
                StartCoroutine(HitBoxOff());
                health--;
                Destroy(GameObject.Find("Life-Box").transform.GetChild(0).gameObject);
                if (health < 1) {
                    StartCoroutine(Death());
                }
            }
        }
    }

    IEnumerator Death() {
        SoundManager.instance.PlaySoundFX(deathClip);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator Reload() {
        //Debug.Log("Reloading....");
        //SoundManager.instance.PlaySoundFX(currentWeapon.reloadClip);
        //yield return new WaitForSeconds(currentWeapon.reloadClip.length);
        yield return new WaitForSeconds(1);
        reload = false;
        //Debug.Log("Ready to fire!");
        Shots = 0;
    }
}