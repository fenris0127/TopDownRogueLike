using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowSystem : MonoBehaviour
{
    public float slowSpeed;
    [SerializeField] private float originalSpeed;

    void Start()
    {
        originalSpeed = PlayerMovement.instance.movementSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            PlayerMovement.instance.activeMovementSpeed = slowSpeed;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            PlayerMovement.instance.activeMovementSpeed = slowSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            PlayerMovement.instance.activeMovementSpeed = originalSpeed;
        }
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if(collision.gameObject.tag == "Player"){
    //         PlayerMovement.instance.movementSpeed = slowSpeed;
    //     }
    // }

    // private void OnCollisionStay2D(Collision2D collision)
    // {
    //     if(collision.gameObject.tag == "Player"){
    //         PlayerMovement.instance.movementSpeed = slowSpeed;
    //     }
    // }

    // private void OnCollisionExit2D(Collision2D collision)
    // {
    //     if(collision.gameObject.tag == "Player"){
    //         PlayerMovement.instance.movementSpeed = originalSpeed;
    //     }
    // }
}
