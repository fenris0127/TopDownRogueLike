using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool closeDoor; // door
    // public bool openDoor;
    public GameObject[] doors;
    // public List<GameObject> enemies;
    [HideInInspector] public bool roomActive;

    void Start()
    {
        
    }

    void Update()
    {
        // if(enemies.Count > 0 && roomActive && openDoor){
        //     for (int i = 0; i < enemies.Count; i++){
        //         if(enemies[i] == null){
        //             enemies.RemoveAt(i);
        //             i--;
        //         }
        //     }

        //     if(enemies.Count == 0){
        //         foreach (GameObject door in doors){
        //             door.SetActive(false);
        //             closeDoor = false;
        //         }
        //     }
        // }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            CameraController.instance.ChangeTarget(transform);

            if(closeDoor){
                foreach (GameObject door in doors){
                    door.SetActive(true);
                }
            }

            roomActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player"){ roomActive = false; }
    }

    public void OpenDoors()
    {
        foreach (GameObject door in doors){
            door.SetActive(false);
            closeDoor = false;
        }
    }
}
