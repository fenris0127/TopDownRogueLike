using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    private Vector3 direction;

    void Start()
    {
        direction = transform.right;
    }

    void Update()
    {
        transform.position += direction * bulletSpeed * Time.deltaTime;

        if(!BossController.instance.gameObject.activeInHierarchy){
            Destroy(this.gameObject);
        }
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

    public void SetMoveDirection(Vector2 direction)
    {
        this.direction = direction;
    }
}
