using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public ExplosionBlow ExplosionPrefab;
    public PlayerMovement Player;

    private PlayerMovement _closestPlayer;
    
    //Temps avant explosion de la bombe
    void Start()
    {
        StartCoroutine(WaitUntilBoom());
        _closestPlayer = FindClosestPlayer();
    }

    IEnumerator WaitUntilBoom()
    {
        yield return new WaitForSeconds(3f);
        Boom();
    }

    //Création de l'explosion en croix basé sur un instantiate de prefab gérant lui-même ce qu'il touche
    public void Boom()
    {
        var NewExplosion = Instantiate(ExplosionPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
        var NewExplosionBis = Instantiate(ExplosionPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.Euler(0, 0, 90));
        NewExplosion.transform.localScale = new Vector3(1, 2 * Player.PlayerPower, 1);
        NewExplosionBis.transform.localScale = new Vector3(1, 2 * Player.PlayerPower, 1);

        NewExplosion.NearestPlayer = _closestPlayer;
        NewExplosionBis.NearestPlayer = _closestPlayer;
        Destroy(gameObject);
    }

    //Permet d'éviter des bugs de collision tant qu'on est sur la bombe. La bombe redevient physique une fois qu'on s'écarte
    private void OnTriggerExit(Collider other)
    {
        gameObject.GetComponent<SphereCollider>().isTrigger = false;
    }

    public PlayerMovement FindClosestPlayer()
    {
        GameObject[] AllPlayers;
        AllPlayers = GameObject.FindGameObjectsWithTag("Player");
        PlayerMovement Closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject NearPlayer in AllPlayers)
        {
            Vector3 diff = NearPlayer.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                Closest = NearPlayer.GetComponent<PlayerMovement>();
                distance = curDistance;
            }
        }
        return Closest;
    }
}
