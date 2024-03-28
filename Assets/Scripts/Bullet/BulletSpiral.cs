using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpiral : MonoBehaviour
{
    private float angle = 0f;
    private Vector2 bulletMoveDirection;

    void Start()
    {
        //InvokeRepeating("Fire", 0f, 0.2f);
    }

    void Update()
    {
        
    }

    public void FireDoubleSprial()
    {
        for (int i = 0; i <= 1; i++){
            float bulletDirectionX = transform.position.x + Mathf.Sin(((angle + 180f * i) * Mathf.PI) / 180f);
            float bulletDirectionY = transform.position.y + Mathf.Cos(((angle + 180f * i) * Mathf.PI) / 180f);

            Vector3 bulletMoveVector = new Vector3(bulletDirectionX, bulletDirectionY, 0f);
            Vector2 bulletDirection = (bulletMoveVector - transform.position).normalized;

            GameObject bullet = BulletPool.instance.GetBullet();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<BossBullet>().SetMoveDirection(bulletDirection);
        }

        angle += 10f;

        if(angle >= 360f){ angle = 0f; }
    }

    public void FireSprial()
    {
        float bulletDirectionX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
        float bulletDirectionY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

        Vector3 bulletMoveVector = new Vector3(bulletDirectionX, bulletDirectionY, 0f);
        Vector2 bulletDirection = (bulletMoveVector - transform.position).normalized;

        GameObject bullet = BulletPool.instance.GetBullet();
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.SetActive(true);
        bullet.GetComponent<BossBullet>().SetMoveDirection(bulletDirection);

        angle += 10f;
    }
}
