using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnlock : MonoBehaviour
{
    private bool canUnlock;
    public GameObject message;

    public PlayerSelector[] selectors;
    private PlayerSelector playerToUnlock;
    public SpriteRenderer cagedSR;

    void Start()
    {
        playerToUnlock = selectors[Random.Range(0, selectors.Length)];
        cagedSR.sprite = playerToUnlock.playerToSpwan.sr.sprite;
    }

    void Update()
    {
        if(canUnlock){
            if(Input.GetKeyDown(KeyCode.E)){
                PlayerPrefs.SetInt(playerToUnlock.playerToSpwan.name, 1);
                Instantiate(playerToUnlock, transform.position, transform.rotation);

                gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            canUnlock = true;
            message.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            canUnlock = false;
            message.SetActive(false);
        }
    }
}
