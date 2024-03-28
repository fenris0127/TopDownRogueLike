using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public float moveSpeed;
    public Transform target;
    public Camera mainCam;
    public bool isBossRoom;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        mainCam = GetComponent<Camera>();

        if(isBossRoom){
            target = PlayerMovement.instance.transform;
        }
    }

    void Update()
    {
        if(target != null){
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        }
    }

    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }

}
