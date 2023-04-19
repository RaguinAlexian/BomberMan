using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public Manager Manager;
    public GameObject MyButton;
    public GameObject LaunchButton;
    
    public List<GameObject> ButtonList;
    
    public bool Menu = true;

    [SerializeField]
    private int _tempoNbPlayer;

    //Gère sa propre taille selon son tag et la taille de l'écran
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
            if(gameObject.name == "LaunchGame")
            {
                gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(width/4 - 100,height/2);
            }
            if(gameObject.name == "MenuButton") 
            {
                gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(width / 4 + 100, height / 2);
            }
        }
    }

    //Fonction utilisée pour faire varier le nombre de joueur dans la partie. Fonction appelée sur le click d'un bouton
    public void ChangeNbPlayer()
    {
        Manager.NbPlayer = _tempoNbPlayer;
        Manager.PlayerAlive = _tempoNbPlayer;
        for (int i = 0; i < ButtonList.Count;i++)
        {
            ButtonList[i].gameObject.SetActive(false);
        }
        Manager.Text.gameObject.SetActive(true);
        Menu = !Menu;
    }

    //Fonction utilisée pour lancer la partie en refaisant la map. Fonction appelée sur le click d'un bouton
    public void LaunchGame()
    {
        LaunchButton.SetActive(false);
        MyButton.SetActive(false);
        Manager.GameOn = true;
        Manager.ResetMap();

    }

    //Fonction utilisée pour retourner sur le menu et reset la plupart des variables
    public void GoOnMenu()
    {
        LaunchButton.gameObject.SetActive(false);
        MyButton.SetActive(false);
        Menu = !Menu;
        
        Manager.CurrentPlayer = 0;
        Manager.PlayerAlive = 0;
        Manager.NbPlayer = 0;
        Manager.Count = 0;
        Manager.GamingTime = !Manager.GamingTime;
        Manager.AllGood = !Manager.AllGood;
        Manager.GameOn = false;
        Manager.PlayerList.Clear();
        
        for (int i = 0; i < ButtonList.Count; i++)
        {
            ButtonList[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < Manager.InitialPlayerList.Count; i++)
        {
            Manager.InitialPlayerList[i].gameObject.SetActive(true);
            Manager.PlayerList.Add(Manager.InitialPlayerList[i]);
        }
    }
}
