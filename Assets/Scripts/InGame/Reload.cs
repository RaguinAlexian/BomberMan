using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerMovement>().PlayerCooldown = other.GetComponent<PlayerMovement>().PlayerCooldown - (other.GetComponent<PlayerMovement>().PlayerCooldown / 10);
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
