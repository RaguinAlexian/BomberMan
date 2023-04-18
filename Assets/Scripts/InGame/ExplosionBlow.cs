using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBlow : MonoBehaviour
{
    private GameObject Manager;
    void Start()
    {
        StartCoroutine(WaitUntilFade());
        Manager = GameObject.Find("GameManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Finish") && !other.CompareTag("Player"))
        {
            if (other.GetComponent<LootingBox>())
            {
                //Manager.GetComponent<Manager>();
                other.GetComponent<LootingBox>().CrateDestroy();
            }
            else
            {
                Destroy(other.gameObject);
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
