using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerUp : MonoBehaviour
{
    public GameObject Manager;
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
