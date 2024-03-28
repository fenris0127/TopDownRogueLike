using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    [SerializeField] private bool canSelect;
    public bool shouldUnlock;
    public GameObject message;
    public PlayerMovement playerToSpwan;

    void Start()
    {
        if(shouldUnlock){
            if(PlayerPrefs.HasKey(playerToSpwan.name)){
                if(PlayerPrefs.GetInt(playerToSpwan.name) == 1){
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    // gameObject.SetActive(true);
                }else{
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
                    // gameObject.SetActive(false);
                }
            }else{
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
                // gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        if(canSelect){
            if(Input.GetKeyDown(KeyCode.E)){
                Vector3 playerPos = PlayerMovement.instance.transform.position;
                Destroy(PlayerMovement.instance.gameObject);
                
                PlayerMovement newPlayer = Instantiate(playerToSpwan, playerPos, playerToSpwan.transform.rotation);
                PlayerMovement.instance = newPlayer;
                Debug.Log(newPlayer);
                Debug.Log("-----------------------");

                gameObject.SetActive(false);

                CameraController.instance.target = newPlayer.transform;

                PlayerSelectController.instance.activePlayer = newPlayer;
                PlayerSelectController.instance.prevActivePlayer.gameObject.SetActive(true);
                PlayerSelectController.instance.prevActivePlayer = this;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            canSelect = true;
            message.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            canSelect = false;
            message.SetActive(false);
        }
    }
}
