using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject[] brokenPieces;
    public int maxPieces = 5;

    public bool shouldDropItem;
    public GameObject[] dropItems;
    public float itemDropPercent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            if(PlayerMovement.instance.dashCouter > 0){
                Smash();
            }
        }

        if(collision.tag == "PlayerBullet"){
            Smash();
        }
    }

    public void Smash()
    {
        Destroy(gameObject);
        AudioController.instance.PlaySFX(0);

        int piecesToDrop = Random.Range(1, maxPieces); 

        for (int i = 0; i < piecesToDrop; i++){
            int randomPiece = Random.Range(0, brokenPieces.Length);

            Instantiate(brokenPieces[randomPiece], transform.position, transform.rotation);
        }

        if(shouldDropItem){
            float dropChance = Random.Range(0f, 100f);

            if(dropChance < itemDropPercent){
                int randItem = Random.Range(0, dropItems.Length);

                Instantiate(dropItems[randItem], transform.position, transform.rotation);
            }
        }
    }
}
