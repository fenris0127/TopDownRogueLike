using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickup : MonoBehaviour
{
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
            if(gameObject.name == "BluePotion"){
                Destroy(gameObject);
            }else if(gameObject.name == "PinkPotion"){
                Destroy(gameObject);
            }else if(gameObject.name == "OrangePotion"){
                Destroy(gameObject);
            }else if(gameObject.name == "GreenPotion"){
                Destroy(gameObject);
            }else if(gameObject.name == "WhitePotion"){
                Destroy(gameObject);
            }
        }
    }
}
