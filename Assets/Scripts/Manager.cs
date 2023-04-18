using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public List<GameObject> BlockList;
    
    public PlayerMovement Player;
    
    public int count;

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
        if (count <= 5)
        {
            switch (count)
            {
                case 0:
                    Debug.Log("Joueur 1 , choisissez votre touche pour aller vers le haut !");
                    Player.CubeUp = TempoKey;
                    if (Player.CubeUp != KeyCode.None)
                    {
                        TempoKey = KeyCode.None;
                        count++;
                    }
                    break;
                case 1:
                    Debug.Log("Joueur 1 , choisissez votre touche pour aller vers le bas !");
                    Player.CubeDown = TempoKey;
                    if (Player.CubeDown != KeyCode.None)
                    {
                        TempoKey = KeyCode.None;
                        count++;
                    }
                    break;
                case 2:
                    Debug.Log("Joueur 1 , choisissez votre touche pour aller vers la gauche !");
                    Player.CubeLeft = TempoKey;
                    if (Player.CubeLeft != KeyCode.None)
                    {
                        TempoKey = KeyCode.None;
                        count++;
                    }
                    break;
                case 3:
                    Debug.Log("Joueur 1 , choisissez votre touche pour aller vers la droite !");
                    Player.CubeRight = TempoKey;
                    if (Player.CubeRight != KeyCode.None)
                    {
                        TempoKey = KeyCode.None;
                        count++;
                    }
                    break;
                case 4:
                    Debug.Log("Joueur 1 , choisissez votre touche pour poser une bombe !");
                    Player.BombaKey = TempoKey;
                    if (Player.BombaKey != KeyCode.None)
                    {
                        TempoKey = KeyCode.None;
                        count++;
                    }
                    break;
            }
        }
    }

    private void OnGUI()
    {
        if (count <= 5)
        {
            Event e = Event.current;
            if (e.isKey && (e.type == EventType.KeyUp))
            { 
                TempoKey = e.keyCode;
            }
        }
    }
}
