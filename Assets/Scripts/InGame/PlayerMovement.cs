using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform MyPlayerTransform;
    
    public bool BombLaunchReady;
    public bool colorSwitch;
    
    public float speed = 0.1f;
    public float PlayerCooldown = 3f;

    public int PlayerPower = 1;

    public KeyCode CubeUp;
    public KeyCode CubeDown;
    public KeyCode CubeRight;
    public KeyCode CubeLeft;
    public KeyCode BombaKey;
    
    public GameObject BombPrefab;

    public Manager Manager;
    [SerializeField]
    private List<Material> ColorList;
    private Material DefaultColor;

    // Start is called before the first frame update
    void Start()
    {
        DefaultColor = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.GameOn)
        {
            if (Input.GetKey(CubeUp))
            {
                MyPlayerTransform.position = MyPlayerTransform.position + MyPlayerTransform.up * speed;
            }
            else if (Input.GetKey(CubeDown))
            {
                MyPlayerTransform.position = MyPlayerTransform.position + MyPlayerTransform.up * speed * -1;
            }
            else if (Input.GetKey(CubeLeft))
            {
                MyPlayerTransform.position = MyPlayerTransform.position + MyPlayerTransform.right * -1 * speed;
            }
            else if (Input.GetKey(CubeRight))
            {
                MyPlayerTransform.position = MyPlayerTransform.position + MyPlayerTransform.right * speed;
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

    public void Invincibility()
    {
        StartCoroutine(WaitForEnd());
        StartCoroutine(RandomColor());
    }

    IEnumerator WaitForEnd()
    {
        yield return new WaitForSeconds(6f);
        gameObject.tag = "Player";
        colorSwitch = false;
        gameObject.GetComponent<Renderer>().material = DefaultColor;
    }

    IEnumerator RandomColor()
    {
        colorSwitch = true;
        while (colorSwitch)
        {
            var x = Random.Range(0, ColorList.Count);
            gameObject.GetComponent<Renderer>().material = ColorList[x];
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(1f);
    }
}
