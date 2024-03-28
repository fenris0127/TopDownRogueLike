using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    public AudioSource levelMusic;
    public AudioSource gameoverMusic;
    public AudioSource winMusic;
    public AudioSource[] sfx;

    void Start()
    {
        if(instance == null){
            instance = this;
        }else{
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        
    }

    public void PlayGameOver()
    {
        levelMusic.Stop();

        gameoverMusic.Play();
    }

    public void PlayerWin()
    {
        levelMusic.Stop();

        winMusic.Play();
    }

    public void PlaySFX(int sfxNum)
    {
        sfx[sfxNum].Stop();

        sfx[sfxNum].Play();
    }
}
