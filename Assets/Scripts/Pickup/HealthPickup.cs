using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 1;
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
            if(PlayerHealthSystem.instance.currentHealth != PlayerHealthSystem.instance.maxHealth){
                PlayerHealthSystem.instance.HealPlayer(healAmount);
                AudioController.instance.PlaySFX(7);
                Destroy(gameObject);
            }
        }
    }
}
