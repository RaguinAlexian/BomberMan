using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBlow : MonoBehaviour
{
    public GameObject Manager;

    //Récupération du GameObject Manager et mise en place du temps d'apparition
    void Start()
    {
        StartCoroutine(WaitUntilFade());
        Manager = GameObject.Find("GameManager");
    }

    //Vérification de ce que touche l'explosion et effet selon ce qui touche (Destruction caisse, tuer joueur, exploser une autre bombe)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            var tempoManager = Manager.GetComponent<Manager>();
            for (int i = 0; i < tempoManager.BlockList.Count; i++)
            {
                if (tempoManager.BlockList[i].gameObject.GetInstanceID() == other.gameObject.GetInstanceID())
                {
                    tempoManager.BlockList[i].SetActive(false);
                    other.GetComponent<LootingBox>().CrateDestroy();
                }
            }
        }
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().IsDying();
        }
        if (other.CompareTag("Bomb"))
        {
            other.GetComponent<BombScript>().Boom();
        }
    }

    IEnumerator WaitUntilFade()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
