using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController instance;
    public BossPattern[] patterns;
    public BossSequence[] phases;
    public int currentPhase;

    public int curreentHealth;
    public GameObject deathEffect;
    public GameObject hitEffect;
    public GameObject levelExit;

    private Vector2 moveDirection;
    private int currentPattern;
    private float patternCounter;
    private float shotCounter;
    private Rigidbody2D rb;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        patterns = phases[currentPhase].actions;
        patternCounter = patterns[currentPattern].patternLength;

        UISystem.instance.bossHealth.maxValue = curreentHealth;
        UISystem.instance.bossHealth.value = curreentHealth;
    }

    void Update()
    {
        if(patternCounter > 0){
            patternCounter -= Time.deltaTime;

            moveDirection = Vector2.zero;

            if(patterns[currentPattern].shouldMove){
                if(patterns[currentPattern].shouldChasePlayer){
                    moveDirection = PlayerMovement.instance.transform.position - transform.position;
                    moveDirection.Normalize();
                }else if(patterns[currentPattern].shouldPatrol 
                        && Vector3.Distance(transform.position, patterns[currentPattern].pointToMoveTo.position) > 0.5f){
                    moveDirection = patterns[currentPattern].pointToMoveTo.position - transform.position;
                    moveDirection.Normalize();
                }
            }


            rb.velocity = moveDirection * patterns[currentPattern].bossSpeed;

            if(patterns[currentPattern].shouldShoot){
                shotCounter -= Time.deltaTime;

                if(shotCounter <= 0){
                    shotCounter = patterns[currentPattern].timeBtwShots;

                    foreach (Transform bulletPoint in patterns[currentPattern].shotPoints){
                        Instantiate(patterns[currentPattern].itemToShoot, bulletPoint.position, bulletPoint.rotation);
                    }
                }
            }
        }else{
            currentPattern++;

            if(currentPattern >= patterns.Length){
                currentPattern = 0;
            }

            patternCounter = patterns[currentPattern].patternLength;
        }

        if(!PlayerMovement.instance.gameObject.activeInHierarchy){
            moveDirection = Vector2.zero;
            patterns[currentPattern].shouldShoot = false;
            patterns[currentPattern].shouldMove = false;
            patterns[currentPattern].shouldChasePlayer = false;
            patterns[currentPattern].shouldPatrol = false;
        }
    }

    public void DamageBoss(int damageAmount)
    {
        curreentHealth -= damageAmount;

        if(curreentHealth <= 0){
            gameObject.SetActive(false);

            Instantiate(deathEffect, transform.position, transform.rotation);

            if(Vector3.Distance(PlayerMovement.instance.transform.position, levelExit.transform.position) < 2f){
                levelExit.transform.position += new Vector3(4f, 0f, 0f);
            }
            
            levelExit.SetActive(true);

            UISystem.instance.winScreen.SetActive(true);
            UISystem.instance.bossHealth.gameObject.SetActive(false);
        }else{
            if(curreentHealth <= phases[currentPhase].endSequenceHealth 
                && currentPhase < phases.Length - 1){
                currentPhase++;
                patterns = phases[currentPhase].actions;

                currentPattern = 0;
                patternCounter = patterns[currentPattern].patternLength;
            }
        }

        UISystem.instance.bossHealth.value = curreentHealth;
    }
}

[Serializable]
public class BossPattern
{
    [Header("Pattern")]
    public float patternLength;

    [Header("How To Move")]
    public bool shouldMove;
    public bool shouldChasePlayer;
    public bool shouldPatrol;
    public bool shouldShoot;

    [Header("Movement")]
    public float bossSpeed;
    public Transform pointToMoveTo;

    [Header("Shooting")]
    public GameObject itemToShoot;
    public float timeBtwShots;
    public Transform[] shotPoints;
}

[Serializable]
public class BossSequence
{
    [Header("Sequence")]
    public BossPattern[] actions;

    public int endSequenceHealth;
}