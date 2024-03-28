using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{   
    public static LevelController instance;
    public Transform startPos;

    [Header("pause")]
    public bool isPaused;
    public float waitToLoad = 4f;
    public string nextLevelName;

    [Header("coin")]
    public int currentCoins;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        PlayerMovement.instance.transform.position = startPos.position;
        PlayerMovement.instance.canMove = true;

        currentCoins = PlayerTracker.instance.currentCoin;

        Time.timeScale = 1f;

        UISystem.instance.coinTxt.text = currentCoins.ToString();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            PauseAndUnpause();
        }
    }

    public IEnumerator LevelEnd()
    {
        PlayerMovement.instance.canMove = false;

        UISystem.instance.StartFade();

        yield return new WaitForSeconds(waitToLoad);

        PlayerTracker.instance.currentCoin = currentCoins;
        PlayerTracker.instance.currentHealth = PlayerHealthSystem.instance.currentHealth;
        PlayerTracker.instance.maxHealth = PlayerHealthSystem.instance.maxHealth;

        SceneManager.LoadScene(nextLevelName);
    }

    public void PauseAndUnpause()
    {
        if(!isPaused){
            UISystem.instance.pauseMenu.SetActive(true);
            isPaused = true;

            Time.timeScale = 0f;
        }else{
            UISystem.instance.pauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
        }
    }

    public void GetCoins(int amount)
    {
        currentCoins += amount;

        UISystem.instance.coinTxt.text = currentCoins.ToString();
    }

    public void SpendCoins(int amount)
    {
        currentCoins -= amount;

        if(currentCoins < 0){
            currentCoins = 0;
        }

        UISystem.instance.coinTxt.text = currentCoins.ToString();
    }
}
