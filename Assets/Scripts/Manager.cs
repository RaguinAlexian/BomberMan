using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public List<GameObject> BlockList;
    public List<GameObject> PlayerList;
    public List<GameObject> InitialPlayerList;
    public List<GameObject> StatUIList;
    public List<Vector3> PlayerPosition;
    
    public ButtonScript LaunchGame;
    public ButtonScript Menu;
    public ButtonScript Text;

    public int Count;
    public int CurrentPlayer;
    public int NbPlayer;
    public int PlayerAlive;

    public bool GameOn;
    public bool AllGood;
    public bool GamingTime;

    private bool _wantUI;

    private KeyCode _tempoKey;
    
    void Start()
    {
        for (int y = 0; y < PlayerList.Count; y++)
        {
            var tempPlayerPosition = new Vector3(PlayerList[y].transform.position.x, PlayerList[y].transform.position.y, PlayerList[y].transform.position.z);
            PlayerPosition.Add(tempPlayerPosition);
            InitialPlayerList.Add(PlayerList[y]);
        }
        ShowOrNotUI();
    }

    public void ResetMap()
    {
        NbPlayer = PlayerList.Count;
        PlayerAlive = NbPlayer;
        _wantUI = true;
        ShowOrNotUI();
        
        for (int x = 0; x < BlockList.Count; x++)
        {
            BlockList[x].SetActive(true);
        }
        
        for (int z = 0; z < BlockList.Count; z++)
        {
            var x = Random.Range(1, 6);
            if (x <= 2)
            {
                BlockList[z].SetActive(false);
            }
            else
            {
                BlockList[z].GetComponent<LootingBox>().Started = true;
            }
        }
        
        for (int i = 0; i < PlayerList.Count; i++)
        {
            PlayerList[i].SetActive(true);
            PlayerList[i].transform.position = PlayerPosition[i];
            PlayerList[i].GetComponent<PlayerMovement>().PlayerCooldown = 3;
            PlayerList[i].GetComponent<PlayerMovement>().PlayerPower = 1;
        }
        
        for (int z = StatUIList.Count - 1; z > 0; z--)
        {
            if (z >= 2 * PlayerList.Count)
            {
                StatUIList[z].SetActive(false);
            }
        }
    }

    void Update()
    {
        if (!LaunchGame.Menu)
        {
            if (AllGood)
            {
                //Le joueur va choisir ses touches, choix du joueur
                if (CurrentPlayer < PlayerList.Count)
                {
                    //Choix des touches et implémentations de celles-ci
                    if (Count < 5)
                    {
                        switch (Count)
                        {
                            case 0:
                                
                                Text.GetComponent<TextMeshProUGUI>().text = PlayerList[CurrentPlayer].name + ", choisissez votre touche pour aller vers le haut !";
                                PlayerList[CurrentPlayer].GetComponent<PlayerMovement>().CubeUp = _tempoKey;
                                
                                if (PlayerList[CurrentPlayer].GetComponent<PlayerMovement>().CubeUp != KeyCode.None)
                                {
                                    _tempoKey = KeyCode.None;
                                    Count++;
                                }
                                break;
                            
                            case 1:
                                
                                Text.GetComponent<TextMeshProUGUI>().text = PlayerList[CurrentPlayer].name + ", choisissez votre touche pour aller vers le bas !";
                                PlayerList[CurrentPlayer].GetComponent<PlayerMovement>().CubeDown = _tempoKey;
                                
                                if (PlayerList[CurrentPlayer].GetComponent<PlayerMovement>().CubeDown != KeyCode.None)
                                {
                                    _tempoKey = KeyCode.None;
                                    Count++;
                                }
                                break;
                            
                            case 2:
                                
                                Text.GetComponent<TextMeshProUGUI>().text = PlayerList[CurrentPlayer].name + ", choisissez votre touche pour aller vers la gauche !";
                                PlayerList[CurrentPlayer].GetComponent<PlayerMovement>().CubeLeft = _tempoKey;
                                
                                if (PlayerList[CurrentPlayer].GetComponent<PlayerMovement>().CubeLeft != KeyCode.None)
                                {
                                    _tempoKey = KeyCode.None;
                                    Count++;
                                }
                                break;
                            
                            case 3:
                                
                                Text.GetComponent<TextMeshProUGUI>().text = PlayerList[CurrentPlayer].name + ", choisissez votre touche pour aller vers la droite !";
                                PlayerList[CurrentPlayer].GetComponent<PlayerMovement>().CubeRight = _tempoKey;
                                
                                if (PlayerList[CurrentPlayer].GetComponent<PlayerMovement>().CubeRight != KeyCode.None)
                                {
                                    _tempoKey = KeyCode.None;
                                    Count++;
                                }
                                break;
                            
                            case 4:
                                
                                Text.GetComponent<TextMeshProUGUI>().text = PlayerList[CurrentPlayer].name + ", choisissez votre touche pour poser une bombe !";
                                PlayerList[CurrentPlayer].GetComponent<PlayerMovement>().BombaKey = _tempoKey;
                                
                                if (PlayerList[CurrentPlayer].GetComponent<PlayerMovement>().BombaKey != KeyCode.None)
                                {
                                    _tempoKey = KeyCode.None;
                                    Count++;
                                }
                                break;
                        }
                    }
                    else
                    {
                        Count = 0;
                        CurrentPlayer++;
                    }
                }
                //Après le choix du nombre de joueurs on fait apparaitre les boutons pour jouer ou revenir en arrière
                else
                {
                    if (CurrentPlayer == PlayerList.Count && !GameOn && !GamingTime)
                    {
                        LaunchGame.LaunchButton.SetActive(true);
                        Text.gameObject.SetActive(false);
                        Menu.MyButton.SetActive(true);
                        GamingTime = true;
                    }
                }
            }
            else
            {
                if (NbPlayer <= 0 || NbPlayer > 4)
                {
                    Text.GetComponent<TextMeshProUGUI>().text = "Nombre Joueur ?";
                }
                //Tous les joueurs existent jusqu'à ce qu'on choisisse le nombre de joueur jouant. On les retire alors de la liste
                else
                {
                    for (int i = 1; i <= 4 - NbPlayer; i++)
                    {
                        var tempPlayer = PlayerList[4 - i];
                        PlayerList.Remove(PlayerList[4 - i]);
                        tempPlayer.SetActive(false);
                    }
                    AllGood = !AllGood;
                }
            }
        }
        if (GameOn && GamingTime)
        {
            //Vérification de victoire
            if(PlayerAlive <= 0 && CurrentPlayer != 1)
            {
                Text.gameObject.SetActive(true);
                Text.GetComponent<TextMeshProUGUI>().text = "Personne n'a gagné";
                GameOn = false;
                StartCoroutine(WaitForEnding());
            }
            
            if(PlayerAlive == 1 && CurrentPlayer != 1)
            {
                Text.gameObject.SetActive(true);
                var TempWinner = GameObject.FindGameObjectsWithTag("Player");
                Text.GetComponent<TextMeshProUGUI>().text = TempWinner[0].name + ", a vaincu !";
                GameOn = false;
                StartCoroutine(WaitForEnding());
            }
            
            if (PlayerAlive <= 0 && CurrentPlayer == 1)
            {
                Text.gameObject.SetActive(true);
                Text.GetComponent<TextMeshProUGUI>().text = "Vous avez perdu";
                GameOn = false;
                StartCoroutine(WaitForEnding());
            }
        }
    }

    //Détecte quand on appuie sur une touche et sur laquelle. Utilisée pour implémenter les touches voulus
    private void OnGUI()
    {
        if (Count <= 5 && !GameOn)
        {
            Event e = Event.current;
            if (e.isKey && (e.type == EventType.KeyUp))
            {
                _tempoKey = e.keyCode;
            }
        }
    }

    //Fonction lancée lors de la fin de partie pour rejouer ou revenir au menu
    private void Ending()
    {
        LaunchGame.LaunchButton.SetActive(true);
        Text.gameObject.SetActive(false);
        Menu.MyButton.SetActive(true);

    }

    IEnumerator WaitForEnding()
    {
        yield return new WaitForSeconds(2f);
        Ending();
    }

   //Affiche ou non les stats des différents joueurs selon le nombre de joueurs
    public void ShowOrNotUI()
    {
        if (_wantUI)
        {
            for (int j = 0; j < StatUIList.Count; j++)
            {
                StatUIList[j].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < StatUIList.Count; i++)
            {
                StatUIList[i].SetActive(false);
            }
        }
    }
}
