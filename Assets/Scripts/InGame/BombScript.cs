using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    public PlayerMovement Player;
    
    //Temps avant explosion de la bombe
    void Start()
    {
        StartCoroutine(WaitUntilBoom());
    }

    IEnumerator WaitUntilBoom()
    {
        yield return new WaitForSeconds(3f);
        Boom();
    }

    //Cr�ation de l'explosion en croix bas� sur un instantiate de prefab g�rant lui-m�me ce qu'il touche
    public void Boom()
    {
        GameObject NewExplosion = Instantiate(ExplosionPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
        GameObject NewExplosionBis = Instantiate(ExplosionPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.Euler(0, 0, 90));
        NewExplosion.transform.localScale = new Vector3(1, 2 * Player.PlayerPower, 1);
        NewExplosionBis.transform.localScale = new Vector3(1, 2 * Player.PlayerPower, 1);
        Destroy(gameObject);
    }

    //Permet d'�viter des bugs de collision tant qu'on est sur la bombe. La bombe redevient physique une fois qu'on s'�carte
    private void OnTriggerExit(Collider other)
    {
        gameObject.GetComponent<SphereCollider>().isTrigger = false;
    }
}
