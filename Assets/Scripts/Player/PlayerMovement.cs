using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [Header("player")]
    [HideInInspector] public bool canMove = true;
    [HideInInspector] public SpriteRenderer sr;
    public float movementSpeed;
    public float dashSpeed = 8f;
    public float dashLength = 0.5f;
    public float dashCooldown = 1f;
    public float dashInvinc = 0.5f;
    
    [HideInInspector] public float activeMovementSpeed;
    [HideInInspector] public float dashCouter;
    private float dashCooldownCouter;

    [Header("gun movement")]
    public Transform gun;
    public List<Gun> availableGuns;
    [HideInInspector] public int currentGun;
    private int limitGun = 2;

    private Vector2 movementInput;
    private Rigidbody2D rb;
    // private Camera cam;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        activeMovementSpeed = movementSpeed;
    }

    void Update()
    {
        if(canMove && !LevelController.instance.isPaused){
            movementInput.x = Input.GetAxisRaw("Horizontal");
            movementInput.y = Input.GetAxisRaw("Vertical");
            movementInput.Normalize();

            rb.velocity = movementInput * activeMovementSpeed;

            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPoint = CameraController.instance.mainCam.WorldToScreenPoint(transform.localPosition);

            if(mousePos.x < screenPoint.x){
                transform.localScale = new Vector3(-1f, 1f, 1f);
                gun.localScale = new Vector3(-1f, -1f, 1f);
            }else{
                transform.localScale = Vector3.one;
                gun.localScale = Vector3.one;
            }

            // rotate gun with mouse
            Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

            gun.rotation = Quaternion.Euler(0f, 0f, angle);

            if(Input.GetKeyDown(KeyCode.Tab)){
                if(availableGuns.Count > 0){
                    currentGun++;
                    if(currentGun >= availableGuns.Count){
                        currentGun = 0;
                    }

                    SwitchGun();
                }else{
                    
                }
            }

            if(Input.GetKeyDown(KeyCode.Space)){
                if(dashCooldownCouter <= 0 && dashCouter <= 0){
                    activeMovementSpeed = dashSpeed;
                    dashCouter = dashLength;
                    PlayerHealthSystem.instance.MakeInvincible(dashInvinc);
                    AudioController.instance.PlaySFX(8);
                }
            }

            if(dashCouter > 0){
                dashCouter -= Time.deltaTime;

                if(dashCouter <= 0){
                    activeMovementSpeed = movementSpeed;
                    dashCooldownCouter = dashCooldown;
                }
            }

            if(dashCooldownCouter > 0){
                dashCooldownCouter -= Time.deltaTime;
            }
        }else{
            rb.velocity = Vector2.zero;
        }   
    }

    public void SwitchGun()
    {
        foreach (Gun gun in availableGuns){
            gun.gameObject.SetActive(false);
        }

        availableGuns[currentGun].gameObject.SetActive(true);
    }
}
