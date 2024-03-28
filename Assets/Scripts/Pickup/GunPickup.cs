using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public Gun gun;
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
            bool hasGun = false;
            foreach(Gun gunCheck in PlayerMovement.instance.availableGuns){
                if(gun.weaponName == gunCheck.weaponName){
                    hasGun = true;
                }
            }

            if(!hasGun){
                Gun gunClone = Instantiate(gun);
                gunClone.transform.parent = PlayerMovement.instance.gun;
                gunClone.transform.position = PlayerMovement.instance.gun.position;
                gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                gunClone.transform.localScale = Vector3.one;

                PlayerMovement.instance.availableGuns.Add(gunClone);
                PlayerMovement.instance.currentGun = PlayerMovement.instance.availableGuns.Count - 1;
                PlayerMovement.instance.SwitchGun();
            }
            AudioController.instance.PlaySFX(6);
            Destroy(gameObject);
        }
    }
}
