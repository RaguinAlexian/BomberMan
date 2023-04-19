using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerUp : MonoBehaviour
{
    public GameObject Manager;

    //On récupère le script PlayerMovement de l'objet ayant récupéré le PickUp et on augmente sa puissance. Et l'on met à jour l'UI qui correspond à l'index du joueur dans la liste des joueurs. Comme seuls les joueurs peuvent se déplacer aucun problème.
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerMovement>().PlayerPower++;
        Manager = GameObject.Find("GameManager");
        
        var tempoManager = Manager.GetComponent<Manager>();
        var tempStatList = GameObject.FindGameObjectsWithTag("StatUIText");
        
        for (int i = 0; i < tempoManager.PlayerList.Count; i++)
        {
            if (tempoManager.PlayerList[i].gameObject.name == other.gameObject.name)
            {
                for (int j = 0; j < tempStatList.Length; j++)
                {
                    if (tempStatList[j].name == ("PowerBombP" + (i+1).ToString()))
                    {
                        Debug.Log(tempStatList[j].name);
                        tempStatList[j].GetComponent<TextMeshProUGUI>().text = "Power : " + (other.GetComponent<PlayerMovement>().PlayerPower).ToString();
                    }
                }
            }
        }
        Destroy(gameObject);
    }

    //Start + Coroutine pour donner un temps avant de dépop
    void Start()
    {
        StartCoroutine(WaitDespawn());
    }

    IEnumerator WaitDespawn()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
