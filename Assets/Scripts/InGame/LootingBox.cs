using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootingBox : MonoBehaviour
{
    public GameObject PowerUp;
    public GameObject Unkillable;
    public GameObject Cooldown;

    public bool started;

    void Start()
    {
        
    }
    public void CrateDestroy()
    {
        if (started)
        {
            var x = Random.Range(0, 8);
            switch (x)
            {
                case 1 :
                    Instantiate(PowerUp, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                    break;
                case 4:
                    Instantiate(Unkillable, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                    break;
                case 7:
                    Instantiate(Cooldown, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                    break;
            }
        }
    }

    
}
