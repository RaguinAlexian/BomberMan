using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    public PlayerMovement Player;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Boom());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Boom()
    {
        yield return new WaitForSeconds(5f);
        GameObject NewExplosion = Instantiate(ExplosionPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
        NewExplosion.transform.localScale = new Vector3(1, Player.PlayerPower,1);
        GameObject NewExplosionBis = Instantiate(ExplosionPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.Euler(0, 0, 90));
        NewExplosionBis.transform.localScale = new Vector3(1, Player.PlayerPower, 1);
        Destroy(gameObject);
    }
}
