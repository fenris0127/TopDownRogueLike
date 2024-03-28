using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public static PlayerHealthSystem instance;
    public int currentHealth;
    public int maxHealth;

    public float damageInvincLenghth = 1f;
    private float invincCounter;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        maxHealth = PlayerTracker.instance.maxHealth;
        currentHealth = PlayerTracker.instance.currentHealth;

        UISystem.instance.healthSlider.maxValue = maxHealth;
        UISystem.instance.healthSlider.value = currentHealth;
        UISystem.instance.healthTxt.text = currentHealth + "/" + maxHealth;
    }

    void Update()
    {
        if(invincCounter > 0){
            invincCounter -= Time.deltaTime;

            if(invincCounter <= 0){
                PlayerMovement.instance.sr.color = new Color(PlayerMovement.instance.sr.color.r, 
                                                        PlayerMovement.instance.sr.color.g, 
                                                        PlayerMovement.instance.sr.color.b, 
                                                        1f);
            }
        }
    }

    public void DamagePlayer()
    {
        if(invincCounter <= 0){
            currentHealth--;
            AudioController.instance.PlaySFX(10);

            invincCounter = damageInvincLenghth;
            PlayerMovement.instance.sr.color = new Color(PlayerMovement.instance.sr.color.r, 
                                                        PlayerMovement.instance.sr.color.g, 
                                                        PlayerMovement.instance.sr.color.b, 
                                                        0.5f);

            if(currentHealth <= 0){
                PlayerMovement.instance.gameObject.SetActive(false);
                UISystem.instance.deathScreen.SetActive(true);
                AudioController.instance.PlaySFX(9);
                AudioController.instance.PlayGameOver();
            }

            UISystem.instance.healthSlider.value = currentHealth;
            UISystem.instance.healthTxt.text = currentHealth + "/" + maxHealth;
        }
    }

    public void MakeInvincible(float length)
    {
        invincCounter = length;
        PlayerMovement.instance.sr.color = new Color(PlayerMovement.instance.sr.color.r, 
                                                        PlayerMovement.instance.sr.color.g, 
                                                        PlayerMovement.instance.sr.color.b, 
                                                        0.5f);
    }

    public void HealPlayer(int heal)
    {
        currentHealth += heal;

        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }

        UISystem.instance.healthSlider.value = currentHealth;
        UISystem.instance.healthTxt.text = currentHealth + "/" + maxHealth;
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        currentHealth += amount;
        // currentHealth = maxHealth; // 체렉을 업그레이드하면 체력이 꽉 참

        UISystem.instance.healthSlider.maxValue = maxHealth;
        UISystem.instance.healthSlider.value = currentHealth;
        UISystem.instance.healthTxt.text = currentHealth + "/" + maxHealth;
    }
}

