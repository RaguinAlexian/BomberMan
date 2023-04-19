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
            var x = Random.Range(0, 8);
            if (NbCrateDestroyed)
            {
                x = 1;
            }
            switch (x)
            {
                case 1 :
                    if(x == 1 || NbCrateDestroyed)
                    {
                        Instantiate(PowerUp, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                    }
                    gameObject.SetActive(false);
                    break;
                case 2 :    
                    if(x == 2 && !NbCrateDestroyed)
                    {
                        Instantiate(Unkillable, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                    }
                    gameObject.SetActive(false);
                    break;
                case 3:
                    if (x == 3 && !NbCrateDestroyed)
                    {
                        Instantiate(Cooldown, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                    }
                    gameObject.SetActive(false);
                    break;

                default :
                    gameObject.SetActive(false);
                    break;
            }
        }
    }    
}
