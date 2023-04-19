using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unkillable : MonoBehaviour
{
    //PickUp pour rendre le joueur invincible. Puisque l'explosion regarde les tags, on change celui de l'objet l'ayant ramass�. Comme seuls les joueurs peuvent se d�placer aucun probl�me.
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.tag = "Finish";
        other.GetComponent<PlayerMovement>().Invincibility();
        Destroy(gameObject);
    }

    //Start + Coroutine pour donner un temps avant de d�pop
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
