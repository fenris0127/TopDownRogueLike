using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UISystem : MonoBehaviour
{
    public static UISystem instance;

    [Header("Health")]
    public Slider healthSlider;
    public TextMeshProUGUI healthTxt;

    [Header("Game Over")]
    public GameObject deathScreen;
    public string newGame;
    public string mainScene;

    [Header("Fade")]
    public Image fadeScreen;
    public float fadeSpeed;
    private bool fadeIn;
    private bool fadeOut;

    [Header("Pasue")]
    public GameObject pauseMenu;

    [Header("Win")]
    public GameObject winScreen;

    [Header("Coin")]
    public TextMeshProUGUI coinTxt;

    [Header("Boss")]
    public Slider bossHealth;

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
        fadeOut = true;
        fadeIn = false;
    }

    void Update()
    {
        if(fadeOut){
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, 
                                        Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 0f){ fadeOut = false; }
        }

        if(fadeIn){
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, 
                                        Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 1f){ fadeIn = false; }
        }
    }

    public void StartFade()
    {
        fadeIn = true;
        fadeOut = false;
    }

    public void NewGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(newGame);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(mainScene);
    }

    public void Resume()
    {
        LevelController.instance.PauseAndUnpause();
    }
}
