using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GunPickup[] potentialGuns;
    public SpriteRenderer sr;
    public Sprite openedChest;
    public float scaleSpeed = 2f;

    public GameObject notification;
    public Transform spawnPoint;
    private bool canOpen;
    private bool isOpened;

    void Update()
    {
        if(canOpen && !isOpened){
            if(Input.GetKeyDown(KeyCode.E)){
                int gunSelect = Random.Range(0, potentialGuns.Length);

                Instantiate(potentialGuns[gunSelect], spawnPoint.position, spawnPoint.rotation);

                sr.sprite = openedChest;
                isOpened = true;
                transform.localScale = new Vector3(3f, 3f, 3f);
            }
        }

        if(isOpened){
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(2f, 2f, 2f), Time.deltaTime * scaleSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            notification.SetActive(true);
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            notification.SetActive(false);
            canOpen = false;
        }
    }
}
