using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPiece : MonoBehaviour
{
    [Header("move")]
    public float moveSpeed = 5f;
    public float deceleration = 7f;
    public float lifeTime = 3f;
    [Range(0, 1)] public float fadeSpeed = 2.5f;

    private Vector3 moveDirection;
    private SpriteRenderer sr;

    void Start()
    {
        moveDirection.x = Random.Range(-moveSpeed, moveSpeed);
        moveDirection.y = Random.Range(-moveSpeed, moveSpeed);
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.position += moveDirection * Time.deltaTime;

        moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, deceleration * Time.deltaTime);

        lifeTime -= Time.deltaTime;

        if(lifeTime <= 0){
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, Mathf.MoveTowards(sr.color.a, 0f, fadeSpeed * Time.deltaTime));

            if(sr.color.a <= 0f){
                Destroy(gameObject);
            }
        }
    }
}
