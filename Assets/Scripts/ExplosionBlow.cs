using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBlow : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitUntilFade());
    }

    IEnumerator WaitUntilFade()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
