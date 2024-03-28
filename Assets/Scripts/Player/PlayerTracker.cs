using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public static PlayerTracker instance;
    public int currentHealth;
    public int maxHealth;
    public int currentCoin;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        
    }
}
