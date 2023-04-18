using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public List<GameObject> BlockList;
    public List<GameObject> PlayerList;
    
    private int _count;
    private int _currentPlayer;

    private KeyCode TempoKey;
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
            else
            {
                BlockList[z].GetComponent<LootingBox>().started = true;
            }
        }
    }

    void Update()
    {
        if (_currentPlayer < PlayerList.Count)
        {
            if (_count < 5)
            {
                switch (_count)
                {
                    case 0:
                        Debug.Log(PlayerList[_currentPlayer].name + ", choisissez votre touche pour aller vers le haut !");
                        PlayerList[_currentPlayer].GetComponent<PlayerMovement>().CubeUp = TempoKey;
                        if (PlayerList[_currentPlayer].GetComponent<PlayerMovement>().CubeUp != KeyCode.None)
                        {
                            TempoKey = KeyCode.None;
                            _count++;
                        }
                        break;
                    case 1:
                        Debug.Log(PlayerList[_currentPlayer].name + ", choisissez votre touche pour aller vers le bas !");
                        PlayerList[_currentPlayer].GetComponent<PlayerMovement>().CubeDown = TempoKey;
                        if (PlayerList[_currentPlayer].GetComponent<PlayerMovement>().CubeDown != KeyCode.None)
                        {
                            TempoKey = KeyCode.None;
                            _count++;
                        }
                        break;
                    case 2:
                        Debug.Log(PlayerList[_currentPlayer].name + ", choisissez votre touche pour aller vers la gauche !");
                        PlayerList[_currentPlayer].GetComponent<PlayerMovement>().CubeLeft = TempoKey;
                        if (PlayerList[_currentPlayer].GetComponent<PlayerMovement>().CubeLeft != KeyCode.None)
                        {
                            TempoKey = KeyCode.None;
                            _count++;
                        }
                        break;
                    case 3:
                        Debug.Log(PlayerList[_currentPlayer].name + ", choisissez votre touche pour aller vers la droite !");
                        PlayerList[_currentPlayer].GetComponent<PlayerMovement>().CubeRight = TempoKey;
                        if (PlayerList[_currentPlayer].GetComponent<PlayerMovement>().CubeRight != KeyCode.None)
                        {
                            TempoKey = KeyCode.None;
                            _count++;
                        }
                        break;
                    case 4:
                        Debug.Log(PlayerList[_currentPlayer].name + ", choisissez votre touche pour poser une bombe !");
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
