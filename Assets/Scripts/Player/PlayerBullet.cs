using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public static PlayerBullet instance;
    public float bulletSpeed = 10f;
    public int bulletDamage = 50;
    public int bulletAbility;
    public int abilityValue;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        instance = this;
    }

    void Update()
    {
        rb.velocity = transform.right * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall"){
            AudioController.instance.PlaySFX(4);
            Destroy(gameObject);
        }

        if(collision.tag == "Enemy"){
            EnemyMovement.instance.DamageByPlayer(bulletDamage);
            AudioController.instance.PlaySFX(4);
            Destroy(gameObject);
        }

        if(collision.tag == "Boss"){
            BossController.instance.DamageBoss(bulletDamage);
            AudioController.instance.PlaySFX(4);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible(){
        Destroy(gameObject);
    }

    public void BulletDamageUpgrade(int amount)
    {
        bulletDamage += amount;
    }
}
