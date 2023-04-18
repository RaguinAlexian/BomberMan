using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unkillable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.tag = "Finish";
        other.GetComponent<PlayerMovement>().Invincibility();
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
