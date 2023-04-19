using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBlow : MonoBehaviour
{
    public GameObject Manager;
    void Start()
    {
        StartCoroutine(WaitUntilFade());
        Manager = GameObject.Find("GameManager");
    }

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
