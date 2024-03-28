using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCenter : MonoBehaviour
{
    public bool openDoor;
    public List<GameObject> enemies;
    public Room room;

    void Start()
    {
        if(openDoor){
            room.closeDoor = true;
        }
    }

    void Update()
    {
        if(enemies.Count > 0 && room.roomActive && openDoor){
            for (int i = 0; i < enemies.Count; i++){
                if(enemies[i] == null){
                    enemies.RemoveAt(i);
                    i--;
                }
            }

            if(enemies.Count == 0){
                Debug.Log("here");
                room.OpenDoors();
            }
        }
    }
}
