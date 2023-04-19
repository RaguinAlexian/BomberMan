using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Reload : MonoBehaviour
{
    public GameObject Manager;

    //On récupère le script PlayerMovement de l'objet ayant récupéré le PickUp et on réduit son cooldown. Et l'on met à jour l'UI qui correspond à l'index du joueur dans la liste des joueurs. Comme seuls les joueurs peuvent se déplacer aucun problème.
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerMovement>().PlayerCooldown = other.GetComponent<PlayerMovement>().PlayerCooldown - (other.GetComponent<PlayerMovement>().PlayerCooldown / 10);
        Manager = GameObject.Find("GameManager");
        
        var tempoManager = Manager.GetComponent<Manager>();
        var tempStatList = GameObject.FindGameObjectsWithTag("StatUIText");
        
        for (int i = 0; i < tempoManager.PlayerList.Count; i++)
        {
            if (tempoManager.PlayerList[i].gameObject.GetInstanceID() == other.gameObject.GetInstanceID())
            {
                for (int j = 0; j < tempStatList.Length; j++)
                {
                    if (tempStatList[j].name == ("CooldownBombP" + (i+1).ToString()))
                    {
                        tempStatList[j].GetComponent<TextMeshProUGUI>().text = "Cooldown : " + (Mathf.Round(other.GetComponent<PlayerMovement>().PlayerCooldown * 100.0f) * 0.01f).ToString();
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
