using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public List<GameObject> BlockList;
    // Start is called before the first frame update
    void Start()
    {
        for (int z = 0; z < BlockList.Count; z++)
        {
            var x = Random.Range(1, 6);
            if (x <= 2)
            {
                Destroy(BlockList[z].gameObject);
            }
        }
    }        
}
