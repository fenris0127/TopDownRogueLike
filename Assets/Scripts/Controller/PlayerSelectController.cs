using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectController : MonoBehaviour
{
    public static PlayerSelectController instance;
    
    public PlayerMovement activePlayer;
    public PlayerSelector prevActivePlayer;

    void Start()
    {
        instance = this;
    }
}
