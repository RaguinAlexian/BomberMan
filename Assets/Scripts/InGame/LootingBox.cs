using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootingBox : MonoBehaviour
{
    public GameObject PowerUp;
    public GameObject Unkillable;
    public GameObject Cooldown;

    public bool Started;

    //Destruction de la caisse + chance de drop + drop aléatoire + drop obligatoire si trop de fois rien eu
    public void CrateDestroy(bool NbCrateDestroyed)
    {
        if (Started)
        {
            var y = 0;
            var x =  Mathf.Clamp01(Random.Range(0, 8) - 6);
            if (NbCrateDestroyed)
            {
                y = 1;
            }
            var z = x + y;
            switch (z >= 1)
            {
                case true :
                    var xbis = Random.Range(0, 8);
                    if(y == 1 || xbis == 1)
                    {
                        Instantiate(PowerUp, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                    }
                    if(xbis == 4 && y != 1)
                    {
                        Instantiate(Unkillable, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                    }
                    if (xbis == 7 && y != 1)
                    {
                        Instantiate(Cooldown, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                    }
                    break;

                case false:
                    gameObject.SetActive(false);
                    break;
            }
        }
    }    
}
