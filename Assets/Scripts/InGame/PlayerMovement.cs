using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform MyPlayerTransform;
    
    public bool BombLaunchReady;
    private bool _colorSwitch;

    
    public float PlayerCooldown = 3f;
    private float _speed = 0.1f;


    public int PlayerPower = 1;

    public KeyCode CubeUp;
    public KeyCode CubeDown;
    public KeyCode CubeRight;
    public KeyCode CubeLeft;
    public KeyCode BombaKey;
    
    public Manager Manager;
    public GameObject BombPrefab;

    [SerializeField]
    private List<Material> ColorList;
    private Material DefaultColor;

    //On sauvegarde le material de base du joueur.
    void Start()
    {
        DefaultColor = gameObject.GetComponent<Renderer>().material;
    }

    //Déplacement libre sauf diagonales + placement de bombe sur la grille
    void Update()
    {
        if (Manager.GameOn)
        {
            if (Input.GetKey(CubeUp))
            {
                MyPlayerTransform.position = MyPlayerTransform.position + MyPlayerTransform.up * _speed;
            }
            else if (Input.GetKey(CubeDown))
            {
                MyPlayerTransform.position = MyPlayerTransform.position + MyPlayerTransform.up * _speed * -1;
            }
            else if (Input.GetKey(CubeLeft))
            {
                MyPlayerTransform.position = MyPlayerTransform.position + MyPlayerTransform.right * -1 * _speed;
            }
            else if (Input.GetKey(CubeRight))
            {
                MyPlayerTransform.position = MyPlayerTransform.position + MyPlayerTransform.right * _speed;
            }
            
            if (Input.GetKey(BombaKey) && BombLaunchReady)
            {
                BombLaunchReady = !BombLaunchReady;
                int currentX = (int)Mathf.Round(MyPlayerTransform.position.x);
                int currentY = (int)Mathf.Round(MyPlayerTransform.position.y);
                var currentPos = new Vector3(currentX, currentY, MyPlayerTransform.position.z);
                StartCoroutine(NukeLaunch(currentPos));
            }
        }
    }
    IEnumerator NukeLaunch(Vector3 currentPos)
    {
        GameObject NewBomb = Instantiate(BombPrefab, currentPos, Quaternion.identity);
        NewBomb.GetComponent<BombScript>().Player = this;
        yield return new WaitForSeconds(PlayerCooldown);
        BombLaunchReady = !BombLaunchReady;
    }

    //Mort du joueur 
    public void IsDying()
    {
        for(int i = 0; i < Manager.PlayerList.Count; i++)
        {
            if(gameObject.name == Manager.PlayerList[i].name)
            {
                Manager.PlayerList[i].SetActive(false);
            }
        }
        Debug.Log(gameObject.name + " a perdu");
        Manager.PlayerAlive--;
    }

    //Changement de la couleur et du tag sur un temps d'invincibilité temporaire
    public void Invincibility()
    {
        StartCoroutine(WaitForEnd());
        StartCoroutine(RandomColor());
    }

    IEnumerator WaitForEnd()
    {
        yield return new WaitForSeconds(6f);
        gameObject.tag = "Player";
        _colorSwitch = false;
        gameObject.GetComponent<Renderer>().material = DefaultColor;
    }

    IEnumerator RandomColor()
    {
        _colorSwitch = true;
        while (_colorSwitch)
        {
            var x = Random.Range(0, ColorList.Count);
            gameObject.GetComponent<Renderer>().material = ColorList[x];
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(1f);
    }
}
