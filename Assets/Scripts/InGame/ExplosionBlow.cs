using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBlow : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(WaitUntilFade());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Finish"))
        {
            Destroy(other.gameObject);
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
