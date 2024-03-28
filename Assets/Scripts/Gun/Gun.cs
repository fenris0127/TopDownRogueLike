using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("gun movement")]
    public GameObject bullet;
    public Transform[] firePoints;

    [Header("fire gun")]
    public float timeBtwShots;
    private float shotCounter;

    [Header("gun")]
    public string weaponName;
    public Sprite gunUI;

    [Header("gun shop")]
    public int itemCost;
    public Sprite gunShopSprite;
    
    void Update()
    {
        if(PlayerMovement.instance.canMove && !LevelController.instance.isPaused){
            if(shotCounter > 0){
                shotCounter -= Time.deltaTime;
            }else{
                if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)){
                    for (int i = 0; i < firePoints.Length; i++){
                        Instantiate(bullet, firePoints[i].position, firePoints[i].rotation);
                    }
                    
                    shotCounter = timeBtwShots;
                    AudioController.instance.PlaySFX(12);
                }
            }
        }
    }
}
