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
        StartCoroutine(WaitUntilBoom());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitUntilBoom()
    {
        yield return new WaitForSeconds(3f);
        Boom();
    }
    public void Boom()
    {
        GameObject NewExplosion = Instantiate(ExplosionPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
        NewExplosion.transform.localScale = new Vector3(1, 2 * Player.PlayerPower, 1);
        GameObject NewExplosionBis = Instantiate(ExplosionPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.Euler(0, 0, 90));
        NewExplosionBis.transform.localScale = new Vector3(1, 2 * Player.PlayerPower, 1);
        Destroy(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        gameObject.GetComponent<SphereCollider>().isTrigger = false;
    }
}
