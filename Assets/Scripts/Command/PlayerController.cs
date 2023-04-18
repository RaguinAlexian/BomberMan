using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody myRB;

    public static PlayerController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        myRB = GetComponent<Rigidbody>(); 
    }

    public void Bomb()
    {
        Debug.Log("Boom");
    }
}
