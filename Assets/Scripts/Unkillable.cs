using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unkillable : MonoBehaviour
{
    public PowerUp PowerUp;
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.tag = "Finish";
        other.GetComponent<PlayerMovement>().Invincibility();
        Destroy(gameObject);
    }
}
