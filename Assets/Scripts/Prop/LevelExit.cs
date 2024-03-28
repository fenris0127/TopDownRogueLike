using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public string sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            LevelController.instance.nextLevelName = sceneName;
            StartCoroutine(LevelController.instance.LevelEnd());
        }
    }
}
