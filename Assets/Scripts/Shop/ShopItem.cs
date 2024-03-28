using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public GameObject notification;

    [Header("choose item character")]
    public bool isHealthRestore;
    public bool isHealthUpgrade;
    public bool isDamageUpgrade;
    public bool isWeapon;

    [Header("item value")]
    public int itemCost;
    public int healthUpgradeAmount;
    public int damageUpgradeAmount;

    [SerializeField] private bool inBuyZone;

    [Header("weapon")]
    public Gun[] potentialGuns;
    public SpriteRenderer gunSprite;
    public TextMeshProUGUI infoTxt;
    private Gun gun;

    void Start()
    {
        if(isWeapon){
            int selectedGun = Random.Range(0, potentialGuns.Length);
            gun = potentialGuns[selectedGun];

            gunSprite.sprite = gun.gunShopSprite;
            infoTxt.text = gun.weaponName + "\n - " + gun.itemCost + "Gold - ";
            itemCost = gun.itemCost;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && inBuyZone){
            if(LevelController.instance.currentCoins >= itemCost){
                if(isHealthRestore){
                    if(PlayerHealthSystem.instance.currentHealth < PlayerHealthSystem.instance.maxHealth){
                        PlayerHealthSystem.instance.HealPlayer(1);
                        LevelController.instance.SpendCoins(itemCost);
                        gameObject.SetActive(false);
                        AudioController.instance.PlaySFX(17);
                    }else{
                        Debug.Log("Full health");
                        AudioController.instance.PlaySFX(18);
                    }
                }else if(isHealthUpgrade){
                    PlayerHealthSystem.instance.IncreaseMaxHealth(healthUpgradeAmount);
                    LevelController.instance.SpendCoins(itemCost);
                    AudioController.instance.PlaySFX(17);
                    gameObject.SetActive(false);
                }else if(isDamageUpgrade){
                    PlayerBullet.instance.BulletDamageUpgrade(damageUpgradeAmount);
                    LevelController.instance.SpendCoins(itemCost);
                    AudioController.instance.PlaySFX(17);
                    gameObject.SetActive(false);
                }else if(isWeapon){
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
                        LevelController.instance.SpendCoins(itemCost);
                        AudioController.instance.PlaySFX(17);
                        gameObject.SetActive(false);
                    }else{
                        Debug.Log("I have this gun");
                        AudioController.instance.PlaySFX(18);
                    }
                }else{
                    AudioController.instance.PlaySFX(18);
                }
                
                inBuyZone = false;
            }
        }
    }

     private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            notification.SetActive(true);
            inBuyZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            notification.SetActive(false);
            inBuyZone = false;
        }
    }
}
