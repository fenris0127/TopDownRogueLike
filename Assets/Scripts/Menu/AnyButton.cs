using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnyButton : MonoBehaviour
{
    public float waitForAnyKey = 2f;
    public GameObject anyKeyTxt;
    public string loadToScene;

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if(waitForAnyKey > 0){
            waitForAnyKey -= Time.deltaTime;
            if(waitForAnyKey <= 0){
                anyKeyTxt.SetActive(true);
            }
        }else{
            if(Input.anyKeyDown){
                SceneManager.LoadScene(loadToScene);
            }
        }
    }
}
