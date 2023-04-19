using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Reload : MonoBehaviour
{
    public GameObject Manager;
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
                        Debug.Log(tempStatList[j].name);
                        tempStatList[j].GetComponent<TextMeshProUGUI>().text = "Cooldown : " + (Mathf.Round(other.GetComponent<PlayerMovement>().PlayerCooldown * 100.0f) * 0.01f).ToString();
                    }
                }
            }
        }
        Destroy(gameObject);
    }

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
