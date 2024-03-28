using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    private Vector3 directionToPlayer;

    void Start()
    {
        directionToPlayer = PlayerMovement.instance.transform.position - transform.position;
        directionToPlayer.Normalize();
    }

    void Update()
    {
        transform.position += directionToPlayer * bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall"){
            AudioController.instance.PlaySFX(4);
            Destroy(gameObject);
        }

        if(collision.tag == "Player"){
            PlayerHealthSystem.instance.DamagePlayer();
            AudioController.instance.PlaySFX(4);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible(){
        Destroy(gameObject);
    }
}
