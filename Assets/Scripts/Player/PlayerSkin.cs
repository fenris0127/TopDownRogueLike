using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    public List<GameObject> slots;
    public GameObject[] skins;
    public int selectedPlayer;
    public List<GameObject> playerSkins;

    void Awake()
    {
        // int unlockedPlayer = PlayerPrefs.GetInt("Player", 1);
        // for (int i = 0; i < skins.Length; i++){ skins[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f); }
        // for (int i = 0; i < unlockedPlayer; i++){ skins[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f); }

        for(int i = 0; i < slots.Count; i++){
            GameObject player = Instantiate(skins[Random.Range(0, skins.Length)]);
            playerSkins.Add(player);
            Debug.Log(i);

            for (int j = 0; j < playerSkins.Count; j++) {
                Debug.Log(playerSkins.Contains(player));
                if(playerSkins[j].name == player.name){
                    playerSkins.RemoveAt(playerSkins.Count - 1);
                    Destroy(player);
                    player = Instantiate(skins[Random.Range(0, skins.Length)]);
                    playerSkins.Add(player);
                }else{
                    break;
                }
            }
        }

        for (int i = 0; i < playerSkins.Count; i++)
        {
            playerSkins[i].transform.position = new Vector3(slots[i].transform.position.x, slots[i].transform.position.y, slots[i].transform.position.z);
            playerSkins[i].transform.rotation = Quaternion.identity;
        }
    }
}
