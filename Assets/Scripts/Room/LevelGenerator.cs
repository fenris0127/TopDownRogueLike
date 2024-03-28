using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator instance;

    [Header("group")]
    public GameObject roomPos;
    public GameObject wallPos;
    public GameObject roomCenterPos;

    [Header("room")]
    public GameObject layoutRoom;
    private GameObject endRoom;
    
    public int distanceToEnd; // roomNum

    [Header("shop room")]
    public bool includeShop;
    public int minDistanceToShop;
    public int maxDistanceToShop;
    private GameObject shopRoom;

    [Header("room layout color")]
    public Color startColor;
    public Color tresureColor;
    public Color shopColor;
    public Color endColor;

    [Header("room position")]
    public Direction selectedDirection;
    public Transform generatorPoint; 
    public float xOffset = 18f;
    public float yOffset = 10f;
    public LayerMask whatIsRoom;
    public RoomPrefab rooms;

    [Header("room center")]
    public RoomCenter centerStart;
    public RoomCenter centerEnd;
    public RoomCenter centerShop;
    public RoomCenter[] potentialCenters;

    [SerializeField] private List<GameObject> layoutRoomObjects;
    [SerializeField] private List<GameObject> generatedOutline;

    void Awake()
    {
        if(instance == null){
            instance = this;
        }else{
            Destroy(this.gameObject);
        }
    }


    void Start()
    {
        GameObject layout = Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation);
        layout.GetComponent<SpriteRenderer>().color = startColor;
        layout.transform.parent = roomPos.transform;
        layout.name = "StartRoom";

        selectedDirection = (Direction)Random.Range(0, 4);
        MoveGenerationPoint();

        for (int i = 0; i < distanceToEnd; i++){
            GameObject newRoom = Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation);

            layoutRoomObjects.Add(newRoom);
            newRoom.transform.parent = roomPos.transform;

            if(i + 1 == distanceToEnd){
                newRoom.GetComponent<SpriteRenderer>().color = endColor;
                layoutRoomObjects.RemoveAt(layoutRoomObjects.Count - 1);
                newRoom.name = "EndRoom";
                endRoom = newRoom;
            }

            selectedDirection = (Direction)Random.Range(0, 4);
            MoveGenerationPoint();

            while(Physics2D.OverlapCircle(generatorPoint.position, 0.2f, whatIsRoom)){
                MoveGenerationPoint();
            }
        }

        if(includeShop){
            int shopSelector = Random.Range(minDistanceToShop, maxDistanceToShop + 1);
            shopRoom = layoutRoomObjects[shopSelector];
            layoutRoomObjects.RemoveAt(shopSelector);
            shopRoom.GetComponent<SpriteRenderer>().color = shopColor;
        }

        // startRoom
        CreateRoomOutline(Vector3.zero);

        foreach (GameObject room in layoutRoomObjects){
            CreateRoomOutline(room.transform.position);
        }

        CreateRoomOutline(endRoom.transform.position);
        if(includeShop){
            CreateRoomOutline(shopRoom.transform.position);
        }

        foreach (GameObject outline in generatedOutline){
            bool generateCenter = true;

            if(outline.transform.position == Vector3.zero){
                RoomCenter startRoom = Instantiate(centerStart, outline.transform.position, transform.rotation);
                startRoom.room = outline.GetComponent<Room>();
                startRoom.transform.parent = roomCenterPos.transform;

                generateCenter = false;
            }

            if(outline.transform.position == endRoom.transform.position){
                RoomCenter roomCenter = Instantiate(centerEnd, outline.transform.position, transform.rotation);
                roomCenter.room = outline.GetComponent<Room>();
                roomCenter.transform.parent = roomCenterPos.transform;

                generateCenter = false;
            }

            if(includeShop){
                if(outline.transform.position == shopRoom.transform.position ){
                    RoomCenter roomCenter = Instantiate(centerShop, outline.transform.position, transform.rotation);
                    roomCenter.room = outline.GetComponent<Room>();
                    roomCenter.transform.parent = roomCenterPos.transform;

                    generateCenter = false;
                }
            }

            if(generateCenter){
                int centerSelect = Random.Range(0, potentialCenters.Length);

                RoomCenter roomEnd = Instantiate(potentialCenters[centerSelect], outline.transform.position, transform.rotation);
                roomEnd.room = outline.GetComponent<Room>();
                roomEnd.transform.parent = roomCenterPos.transform;
            }
        }
    }

    void Update()
    {
        
    }

    public void MoveGenerationPoint()
    {
        switch (selectedDirection)
        {
            case Direction.up: 
                generatorPoint.position += new Vector3(0f, yOffset, 0f);
                break;
            case Direction.down: 
                generatorPoint.position += new Vector3(0f, -yOffset, 0f);
                break;
            case Direction.left: 
                generatorPoint.position += new Vector3(-xOffset, 0f, 0f);
                break;
            case Direction.right: 
                generatorPoint.position += new Vector3(xOffset, 0f, 0f);
                break;
            default: break;
        }
    }

    public void CreateRoomOutline(Vector3 roomPos)
    {
        bool roomAbove = Physics2D.OverlapCircle(roomPos + new Vector3(0f, yOffset, 0f), 0.2f, whatIsRoom);
        bool roomBelow = Physics2D.OverlapCircle(roomPos + new Vector3(0f, -yOffset, 0f), 0.2f, whatIsRoom);
        bool roomLeft = Physics2D.OverlapCircle(roomPos + new Vector3(-xOffset, 0f, 0f), 0.2f, whatIsRoom);
        bool roomRight = Physics2D.OverlapCircle(roomPos + new Vector3(xOffset, 0f, 0f), 0.2f, whatIsRoom);

        int directionCount = 0;
        if(roomAbove){ directionCount++; }
        if(roomBelow){ directionCount++; }
        if(roomLeft){ directionCount++; }
        if(roomRight){ directionCount++; }

        switch(directionCount){
            case 0: 
                Debug.Log("no room");
                break;
            case 1: 
                if(roomAbove){ 
                    GameObject up = Instantiate(rooms.roomUp, roomPos, transform.rotation); 
                    generatedOutline.Add(up); 
                    up.transform.parent = wallPos.transform; 
                }else if(roomBelow){ 
                    GameObject down = Instantiate(rooms.roomDown, roomPos, transform.rotation);
                    generatedOutline.Add(down); 
                    down.transform.parent = wallPos.transform; 
                }else if(roomLeft){ 
                    GameObject left = Instantiate(rooms.roomLeft, roomPos, transform.rotation); 
                    generatedOutline.Add(left); 
                    left.transform.parent = wallPos.transform; 
                }else if(roomRight){ 
                    GameObject right = Instantiate(rooms.roomRight, roomPos, transform.rotation); 
                    generatedOutline.Add(right); 
                    right.transform.parent = wallPos.transform; 
                }
                break;
            case 2: 
                if(roomAbove && roomBelow){ 
                    GameObject upDown = Instantiate(rooms.roomUpDown, roomPos, transform.rotation); 
                    generatedOutline.Add(upDown); 
                    upDown.transform.parent = wallPos.transform; 
                }else if(roomAbove && roomLeft){ 
                    GameObject upLeft = Instantiate(rooms.roomUpLeft, roomPos, transform.rotation); 
                    generatedOutline.Add(upLeft); 
                    upLeft.transform.parent = wallPos.transform; 
                }else if(roomAbove && roomRight){ 
                    GameObject upRight = Instantiate(rooms.roomUpRight, roomPos, transform.rotation); 
                    generatedOutline.Add(upRight); 
                    upRight.transform.parent = wallPos.transform; 
                }else if(roomBelow && roomLeft){ 
                    GameObject leftDown = Instantiate(rooms.roomLeftDown, roomPos, transform.rotation); 
                    generatedOutline.Add(leftDown); 
                    leftDown.transform.parent = wallPos.transform; 
                }else if(roomBelow && roomRight){ 
                    GameObject rightDown = Instantiate(rooms.roomRightDown, roomPos, transform.rotation); 
                    generatedOutline.Add(rightDown); 
                    rightDown.transform.parent = wallPos.transform; 
                }else if(roomLeft && roomRight){ 
                    GameObject leftRight = Instantiate(rooms.roomLeftRight, roomPos, transform.rotation); 
                    generatedOutline.Add(leftRight); 
                    leftRight.transform.parent = wallPos.transform; 
                }
                break;
            case 3: 
                if(roomAbove && roomBelow && roomLeft){ 
                    GameObject upDownLeft = Instantiate(rooms.roomUpDownLeft, roomPos, transform.rotation); 
                    generatedOutline.Add(upDownLeft); 
                    upDownLeft.transform.parent = wallPos.transform; 
                }else if(roomAbove && roomBelow && roomRight){ 
                    GameObject upDownRight = Instantiate(rooms.roomUpDownRight, roomPos, transform.rotation); 
                    generatedOutline.Add(upDownRight); 
                    upDownRight.transform.parent = wallPos.transform; 
                }else if(roomAbove && roomLeft && roomRight){ 
                    GameObject upLeftRight = Instantiate(rooms.roomUpLeftRight, roomPos, transform.rotation); 
                    generatedOutline.Add(upLeftRight); 
                    upLeftRight.transform.parent = wallPos.transform; 
                }else if(roomBelow && roomLeft && roomRight){ 
                    GameObject downLeftRight = Instantiate(rooms.roomLeftRightDown, roomPos, transform.rotation); 
                    generatedOutline.Add(downLeftRight); 
                    downLeftRight.transform.parent = wallPos.transform; 
                }
                break;
            case 4: 
                if(roomAbove && roomBelow && roomLeft && roomRight){ 
                    GameObject every = Instantiate(rooms.roomEveryDirection, roomPos, transform.rotation);
                    generatedOutline.Add(every);
                    every.transform.parent = wallPos.transform;  
                }
                break;
        }
    }
}

public enum Direction
{ 
    up, 
    down, 
    right, 
    left 
};

[Serializable]
public class RoomPrefab
{
    public GameObject roomDown;
    public GameObject roomLeft;
    public GameObject roomRight;
    public GameObject roomUp;
    public GameObject roomLeftDown;
    public GameObject roomLeftRight;
    public GameObject roomLeftRightDown;
    public GameObject roomRightDown;
    public GameObject roomUpDown;
    public GameObject roomUpDownLeft;
    public GameObject roomEveryDirection;
    public GameObject roomUpDownRight;
    public GameObject roomUpLeft;
    public GameObject roomUpLeftRight;
    public GameObject roomUpRight;
}
