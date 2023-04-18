using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public List<GameObject> BlockList;
    public List<GameObject> PlayerList;

    public bool GameOn;
    
    private int _count;
    private int _currentPlayer;
    public int _nbPlayer;

    private bool _allGood;

    private KeyCode TempoKey;

    public ButtonScript MyButton;
    public ButtonScript Text;
    // Start is called before the first frame update
    public void ResetMap()
    {
        for (int z = 0; z < BlockList.Count; z++)
        {
            var x = Random.Range(1, 6);
            if (x <= 2)
            {
                Destroy(BlockList[z].gameObject);
            }
            else
            {
                BlockList[z].GetComponent<LootingBox>().started = true;
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
                    if (!GameOn)
                    {
                        Text.gameObject.SetActive(false);
                        MyButton.MyButton.SetActive(true);
                    }
                }
            }
            else
            {
                if (_nbPlayer <= 0 || _nbPlayer > 4)
                {
                    Text.GetComponent<TextMeshProUGUI>().text = "Nombre Joueur ?";
                }
                else
                {
                    for (int i = 1; i <= 4 - _nbPlayer; i++)
                    {
                        var tempPlayer = PlayerList[4 - i];
                        PlayerList.Remove(PlayerList[4 - i]);
                        Destroy(tempPlayer);
                    }
                    _allGood = !_allGood;
                }
            }
        }
        if (GameOn)
        {
            if(PlayerList.Count <= 0)
            {
                Text.gameObject.SetActive(true);
                Text.GetComponent<TextMeshProUGUI>().text = "Nombre Joueur ?";
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
}
