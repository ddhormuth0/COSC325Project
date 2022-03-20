using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWin : MonoBehaviour
{
    public Text winText;

    int whoWon;

    public GameMechanics gameMechanics;

    private void Start()
    {
        //get script
        gameMechanics = GameObject.Find("GameManager").transform.GetComponent<GameMechanics>();

        whoWon = gameMechanics.getWhoWon();

        if(whoWon == 1)
        {
            winText.text = "PLAYER ONE WINS";
        }
        else if (whoWon == 2)
        {
            winText.text = "PLAYER TWO WINS";
        }
        else
        {
            winText.text = "DRAW";
        }
    }

}
