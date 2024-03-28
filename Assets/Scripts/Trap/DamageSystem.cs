using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            PlayerHealthSystem.instance.DamagePlayer();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            PlayerHealthSystem.instance.DamagePlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player"){
            PlayerHealthSystem.instance.DamagePlayer();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player"){
            PlayerHealthSystem.instance.DamagePlayer();
        }
    }
}
