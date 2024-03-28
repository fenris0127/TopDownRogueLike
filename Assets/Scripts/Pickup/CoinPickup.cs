using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinValue = 1;
    public float waitToBeCollected = 0.5f;

    void Update()
    {
        if(waitToBeCollected > 0){
            waitToBeCollected -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && waitToBeCollected <= 0){
            LevelController.instance.GetCoins(coinValue);
            AudioController.instance.PlaySFX(5);
            Destroy(gameObject);
        }
    }
}
