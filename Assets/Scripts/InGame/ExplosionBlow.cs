using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBlow : MonoBehaviour
{
    public Manager MyManager;
    public PlayerMovement NearestPlayer;

    private bool _pityLoot;

    //Récupération du GameObject Manager et mise en place du temps d'apparition
    void Start()
    {
        StartCoroutine(WaitUntilFade());
        MyManager = FindObjectOfType<Manager>(); 
    }

    //Vérification de ce que touche l'explosion et effet selon ce qui touche (Destruction caisse, tuer joueur, exploser une autre bombe)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            for (int i = 0; i < MyManager.BlockList.Count; i++)
            {
                if (MyManager.BlockList[i].gameObject.GetInstanceID() == other.gameObject.GetInstanceID())
                {
                    MyManager.BlockList[i].SetActive(false);
                    NearestPlayer.NbCrateDestroy++;
                    if(NearestPlayer.NbCrateDestroy >= 5)
                    {
                        _pityLoot = true;
                        NearestPlayer.NbCrateDestroy = 0;
                    }
                    other.GetComponent<LootingBox>().CrateDestroy(_pityLoot);
                }
            }
            _pityLoot = false;
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
