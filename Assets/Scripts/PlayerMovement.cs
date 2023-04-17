using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform MyX;
    public bool BombLaunchReady;
    public float speed = 0.1f;
    public KeyCode CubeForward;
    public KeyCode CubeBackward;
    public KeyCode CubeRight;
    public KeyCode CubeLeft;
    public KeyCode BombaKey;
    public GameObject BombPrefab;
    public int PlayerPower=1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(CubeForward))
        {
            MyX.position = MyX.position + MyX.up * speed;
        }
        else if (Input.GetKey(CubeBackward))
        {
            MyX.position = MyX.position + MyX.up * speed * -1;
        }
        else if (Input.GetKey(CubeLeft))
        {
            MyX.position = MyX.position + MyX.right * -1 * speed;
        }
        else if (Input.GetKey(CubeRight))
        {
            MyX.position = MyX.position + MyX.right * speed;
        }
        if (Input.GetKey(BombaKey) && BombLaunchReady)
        {
            BombLaunchReady = !BombLaunchReady;
            int currentX = (int)Mathf.Round(MyX.position.x);
            int currentY = (int)Mathf.Round(MyX.position.y);
            var currentPos = new Vector3(currentX, currentY, MyX.position.z);
            StartCoroutine(NukeLaunch(currentPos));
        }
    }
    IEnumerator NukeLaunch(Vector3 currentPos)
    {
        GameObject NewBomb = Instantiate(BombPrefab, currentPos, Quaternion.identity);
        NewBomb.GetComponent<BombScript>().Player = this;
        yield return new WaitForSeconds(5f);
        BombLaunchReady = !BombLaunchReady;
    }

    public void IsDying()
    {
        Debug.Log("Jui mort");
        Destroy(gameObject);
    }
}
