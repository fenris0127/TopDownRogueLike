using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public static EnemyMovement instance;

    [Header("movement")]
    public float movementSpeed;

    [Header("chase")]
    public bool shouldChase;
    public float chaseRange;

    [Header("runawy")]
    public bool shouldRunaway;
    public float runawayRange;

    [Header("wander")]
    public bool shouldWander;
    public float wanderLength;
    public float pauseLength;
    private float wanderCounter;
    private float pauseCounter;
    private Vector3 wanderDirection;

    [Header("patrol")]
    public bool shouldPatrol;
    public Transform[] patrolPoints;
    private int currentPatrolPoint;

    [Header("health")]
    public int health;
    // public int enemyDamage = 100;
    public GameObject deathSplatter;

    [Header("gun")]
    [SerializeField] private bool shouldShoot = true;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    public float shootRange;
    private float fireCounter;

    [Header("Drops")]
    public bool shouldDropItem;
    public GameObject[] dropItems;
    public float itemDropPercent;

    private Vector3 moveDirection;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        if(instance == null){
            instance = this;
        }else{
            Destroy(this.gameObject);
        }

        if(shouldWander){
            pauseCounter = Random.Range(pauseLength * 0.8f, pauseLength * 1.4f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(sr.isVisible && PlayerMovement.instance.gameObject.activeInHierarchy){
            moveDirection = Vector3.zero;

            if(shouldChase && Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) < chaseRange){
                moveDirection = PlayerMovement.instance.transform.position - transform.position;
                if(moveDirection.x < 0.5){
                    transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                }else{
                    transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                }
            }else if(shouldWander){
                Vector3 newDirection = PlayerMovement.instance.transform.position - transform.position;

                if(wanderCounter > 0){
                    wanderCounter -= Time.deltaTime;

                    moveDirection = wanderDirection;
                    if(newDirection.x < 0.5){
                        transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                    }else{
                        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    }

                    if(wanderCounter <= 0){
                        pauseCounter = Random.Range(pauseLength * 0.8f, pauseLength * 1.4f);
                    }
                }

                if(pauseCounter > 0){
                    pauseCounter -= Time.deltaTime;

                    if(pauseCounter <= 0){
                        wanderCounter = Random.Range(wanderLength * 0.8f, wanderLength * 1.4f);

                        wanderDirection = new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f), 0f);
                    }
                }
            }else if(shouldPatrol){
                moveDirection = patrolPoints[currentPatrolPoint].position - transform.position;
                
                if(moveDirection.x < 0.5){
                    transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                }else{
                    transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                }

                if(Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) < 0.1f){
                    currentPatrolPoint++;

                    if(currentPatrolPoint >= patrolPoints.Length){
                        currentPatrolPoint = 0;
                    }
                }
            }

            if(shouldRunaway && Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) < runawayRange){
                moveDirection = transform.position - PlayerMovement.instance.transform.position;
                
                if(moveDirection.x < 0.5){
                    transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                }else{
                    transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                }

                if(moveDirection.x < 0.5){
                    transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                }else{
                    transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                }
            }


            moveDirection.Normalize();

            rb.velocity = moveDirection * movementSpeed;

            if(shouldShoot && Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) < shootRange){
                fireCounter -= Time.deltaTime;

                if(fireCounter <= 0){
                    fireCounter = fireRate;
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    AudioController.instance.PlaySFX(13);
                }
            }
        }else{
            rb.velocity = Vector2.zero;
        }
    }

    public void DamageByPlayer(int damage)
    {
        health -= damage;
        AudioController.instance.PlaySFX(2);

        if(health <= 0){
            AudioController.instance.PlaySFX(1);
            if(shouldDropItem){
                float dropChance = Random.Range(0f, 100f);

                if(dropChance < itemDropPercent){
                    int randItem = Random.Range(0, dropItems.Length);

                    Instantiate(dropItems[randItem], transform.position, transform.rotation);
                }
            }
        }
    }
}
