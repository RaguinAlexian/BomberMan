using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public Manager Manager;
    [SerializeField]
    private int _tempoNbPlayer;
    public bool Menu = true;
    public GameObject MyButton;
    public List<GameObject> ButtonList;
    // Start is called before the first frame update
    void Start()
    {
        var width = Screen.width;
        var height = Screen.height;

        if (!gameObject.CompareTag("TextUi")) 
        {
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width / 4, height / 4); 
        }
        else
        {
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height / 4);
        }
    }

    public void ChangeNbPlayer()
    {
        Manager.NbPlayer = _tempoNbPlayer;
        Manager.PlayerAlive = _tempoNbPlayer;
        for (int i = 0; i < ButtonList.Count;i++)
        {
            ButtonList[i].gameObject.SetActive(false);
        }
        Menu = !Menu;
    }

    public void LaunchGame()
    {
        MyButton.SetActive(false);
        Manager.ResetMap();
        Manager.GameOn = true;
    }
}
