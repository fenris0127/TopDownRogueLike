using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool instance;

    public GameObject pooledBullet;
    private bool notEnoughBulletsInPool = true;
    private List<GameObject> bullets;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        bullets = new List<GameObject>();
    }

    public GameObject GetBullet()
    {
        if(bullets.Count > 0){
            for (int i = 0; i < bullets.Count; i++){
                if(!bullets[i].activeInHierarchy){
                    return bullets[i];
                }
            }
        }

        if(notEnoughBulletsInPool){
            GameObject bullet = Instantiate(pooledBullet);
            bullet.SetActive(false);
            bullets.Add(bullet);
            return bullet;
        }

        return null;
    }
}
