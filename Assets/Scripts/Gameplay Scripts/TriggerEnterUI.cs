using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerEnterUI : MonoBehaviour {
    private GameObject Player;
    private GameObject OpenZone;
    private Animator animUI;
    private bool isOpened = false;
    private TextMeshProUGUI TMPUI;
    public string[] TextInUI;
    public Sprite[] FaceWithText;
    private SpriteRenderer HeadSprR;
    private void Awake() {
        Player = GameObject.Find("Player");
        animUI = GameObject.Find("Story UI").GetComponent<Animator>();
        TMPUI = GameObject.Find("Story Text").GetComponent<TextMeshProUGUI>();
        HeadSprR = GameObject.Find("Story UI").transform.GetChild(1).GetComponent<SpriteRenderer>();
        OpenZone = transform.GetChild(0).gameObject;
        
        OpenZone.SetActive(false);  
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            if (!isOpened) {
                isOpened = true;
                PlayerAble(false);
                OpenZone.SetActive(true);
                StartCoroutine(ShowUI());
            }       
        }
    }
    IEnumerator ShowUI() { 
        TMPUI.SetText(TextInUI[0]);
        HeadSprR.sprite = FaceWithText[0];
        animUI.SetBool("Open", true);
        if(TextInUI.Length > 1) {
            for (int i = 1; i < TextInUI.Length; i++) {
                yield return new WaitForSecondsRealtime(3);
                TMPUI.SetText(TextInUI[i]);
                HeadSprR.sprite = FaceWithText[i];
                if (i + 1 == TextInUI.Length) {
                    yield return new WaitForSecondsRealtime(3);
                    animUI.SetBool("Open", false);
                    OpenZone.SetActive(false);
                    PlayerAble(true);
                }
            }
        }   
    }
    private void PlayerAble(bool able) {
        Player.GetComponent<Player>().isAble = able;
        Player.GetComponent<Player>().legAnim.SetBool("Moving", able);
    }
}
