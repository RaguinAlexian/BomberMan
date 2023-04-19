using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public List<GameObject> BlockList;
    public List<GameObject> PlayerList;
    public List<GameObject> StatUIList;
    public List<Vector3> PlayerPosition;

    public bool GameOn;
    
    private int _count;
    private int _currentPlayer;
    public int NbPlayer;
    public int PlayerAlive;

    private bool _allGood;
    private bool _gamingTime;
    private bool _wantUI;

    private KeyCode TempoKey;

    public ButtonScript MyButton;
    public ButtonScript Text;

    void Start()
    {
        for (int y = 0; y < PlayerList.Count; y++)
        {
            var tempPlayerPosition = new Vector3(PlayerList[y].transform.position.x, PlayerList[y].transform.position.y, PlayerList[y].transform.position.z);
            PlayerPosition.Add(tempPlayerPosition);
        }
        ShowOrNotUI();
    }

    public void ResetMap()
    {
        NbPlayer = PlayerList.Count;
        PlayerAlive = NbPlayer;
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
                BlockList[z].GetComponent<LootingBox>().started = true;
            }
        }
        for (int i = 0; i < PlayerList.Count; i++)
        {
            PlayerList[i].SetActive(true);
            PlayerList[i].transform.position = PlayerPosition[i];
            PlayerList[i].GetComponent<PlayerMovement>().PlayerCooldown = 3;
            PlayerList[i].GetComponent<PlayerMovement>().PlayerPower = 1;
        }
        _wantUI = true;
        ShowOrNotUI();
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
        if (!MyButton.Menu)
        {
            if (_allGood)
            {
                if (_currentPlayer < PlayerList.Count)
                {
                    if (_count < 5)
                    {
                        switch (_count)
                        {
                            case 0:
                                Text.GetComponent<TextMeshProUGUI>().text = PlayerList[_currentPlayer].name + ", choisissez votre touche pour aller vers le haut !";
                                PlayerList[_currentPlayer].GetComponent<PlayerMovement>().CubeUp = TempoKey;
                                if (PlayerList[_currentPlayer].GetComponent<PlayerMovement>().CubeUp != KeyCode.None)
                                {
                                    TempoKey = KeyCode.None;
                                    _count++;
                                }
                                break;
                            case 1:
                                Text.GetComponent<TextMeshProUGUI>().text = PlayerList[_currentPlayer].name + ", choisissez votre touche pour aller vers le bas !";
                                PlayerList[_currentPlayer].GetComponent<PlayerMovement>().CubeDown = TempoKey;
                                if (PlayerList[_currentPlayer].GetComponent<PlayerMovement>().CubeDown != KeyCode.None)
                                {
                                    TempoKey = KeyCode.None;
                                    _count++;
                                }
                                break;
                            case 2:
                                Text.GetComponent<TextMeshProUGUI>().text = PlayerList[_currentPlayer].name + ", choisissez votre touche pour aller vers la gauche !";
                                PlayerList[_currentPlayer].GetComponent<PlayerMovement>().CubeLeft = TempoKey;
                                if (PlayerList[_currentPlayer].GetComponent<PlayerMovement>().CubeLeft != KeyCode.None)
                                {
                                    TempoKey = KeyCode.None;
                                    _count++;
                                }
                                break;
                            case 3:
                                Text.GetComponent<TextMeshProUGUI>().text = PlayerList[_currentPlayer].name + ", choisissez votre touche pour aller vers la droite !";
                                PlayerList[_currentPlayer].GetComponent<PlayerMovement>().CubeRight = TempoKey;
                                if (PlayerList[_currentPlayer].GetComponent<PlayerMovement>().CubeRight != KeyCode.None)
                                {
                                    TempoKey = KeyCode.None;
                                    _count++;
                                }
                                break;
                            case 4:
                                Text.GetComponent<TextMeshProUGUI>().text = PlayerList[_currentPlayer].name + ", choisissez votre touche pour poser une bombe !";
                                PlayerList[_currentPlayer].GetComponent<PlayerMovement>().BombaKey = TempoKey;
                                if (PlayerList[_currentPlayer].GetComponent<PlayerMovement>().BombaKey != KeyCode.None)
                                {
                                    TempoKey = KeyCode.None;
                                    _count++;
                                }
                                break;
                        }
                    }
                    else
                    {
                        _count = 0;
                        _currentPlayer++;
                    }
                }
                else
                {
                    if (_currentPlayer == PlayerList.Count && !GameOn && !_gamingTime)
                    {
                        Text.gameObject.SetActive(false);
                        MyButton.MyButton.SetActive(true);
                        _gamingTime = true;
                    }
                }
            }
            else
            {
                if (NbPlayer <= 0 || NbPlayer > 4)
                {
                    Text.GetComponent<TextMeshProUGUI>().text = "Nombre Joueur ?";
                }
                else
                {
                    for (int i = 1; i <= 4 - NbPlayer; i++)
                    {
                        var tempPlayer = PlayerList[4 - i];
                        PlayerList.Remove(PlayerList[4 - i]);
                        Destroy(tempPlayer);
                    }
                    _allGood = !_allGood;
                }
            }
        }
        if (GameOn && _gamingTime)
        {
            if(PlayerAlive <= 0 && _currentPlayer != 1)
            {
                Text.gameObject.SetActive(true);
                Text.GetComponent<TextMeshProUGUI>().text = "Personne n'a gagné";
                GameOn = false;
                StartCoroutine(WaitForEnding());
            }
            if(PlayerAlive == 1 && _currentPlayer != 1)
            {
                Text.gameObject.SetActive(true);
                var TempWinner = GameObject.FindGameObjectsWithTag("Player");
                Text.GetComponent<TextMeshProUGUI>().text = TempWinner[0].name + ", a vaincu !";
                GameOn = false;
                StartCoroutine(WaitForEnding());
            }
            if (PlayerAlive <= 0 && _currentPlayer == 1)
            {
                Text.gameObject.SetActive(true);
                Text.GetComponent<TextMeshProUGUI>().text = "Vous avez perdu";
                GameOn = false;
                StartCoroutine(WaitForEnding());
            }
        }
    }

    private void OnGUI()
    {
        if (_count <= 5)
        {
            Event e = Event.current;
            if (e.isKey && (e.type == EventType.KeyUp))
            { 
                TempoKey = e.keyCode;
            }
        }
    }

    private void Ending()
    {
        Text.gameObject.SetActive(false);
        MyButton.MyButton.SetActive(true);
    }

    IEnumerator WaitForEnding()
    {
        yield return new WaitForSeconds(2f);
        Ending();
    }

    public void ShowOrNotUI()
    {
        if (_wantUI)
        {
            Debug.Log("Dedans");
            for (int j = 0; j < StatUIList.Count; j++)
            {
                StatUIList[j].SetActive(true);
            }
        }
        else
        {
            Debug.Log("Dehors");
            for (int i = 0; i < StatUIList.Count; i++)
            {
                StatUIList[i].SetActive(false);
            }
        }
    }
}
