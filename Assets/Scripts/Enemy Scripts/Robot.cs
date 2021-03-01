using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour {
    private Transform Player;
    private Rigidbody2D rb;
    [SerializeField]
    private float speed;
    public Transform bulletPos1, bulletPos2;
    public GameObject bullet;
    private bool inRange = false;
    public AudioClip[] shootClips;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Start() {
        StartCoroutine(Shoot());
    }
    private void Update() {
        if(Vector2.Distance(transform.position, Player.position) > 2.5f) {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
            inRange = false;
        }
        else {
            inRange = true;
        }
    }
    private void FixedUpdate() {
        Rotations();
    }
    private void Rotations() {
        Vector2 directions = (Player.gameObject.GetComponent<Rigidbody2D>().position - rb.position).normalized;

        float angle = Mathf.Atan2(directions.y, directions.x) * Mathf.Rad2Deg + 90;
        rb.rotation = angle;
    }
    IEnumerator Shoot () {
        if (inRange) {
            Instantiate(bullet, bulletPos1.position, transform.rotation);
            SoundManager.instance.PlaySoundFX(shootClips[Random.Range(0, shootClips.Length)]);
        }
        yield return new WaitForSeconds(0.5f);
        if (inRange) {
            Instantiate(bullet, bulletPos2.position, transform.rotation);
            SoundManager.instance.PlaySoundFX(shootClips[Random.Range(0, shootClips.Length)]);
        }
        yield return new WaitForSeconds(0.5f); 
        StartCoroutine(Shoot());
    }
}