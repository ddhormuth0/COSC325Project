using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{

    private int playerOneCharacter;
    private int playerTwoCharacter;
    public Image charOneFighter;
    public Image charTwoFighter;
    public Image charOneMage;
    public Image charTwoMage;

    // Start is called before the first frame update
    void Start()
    {
        playerOneCharacter = 1;
        playerTwoCharacter = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            //swap character
            if(playerOneCharacter == 2)
            {
                playerOneCharacter = 1;
                charOneFighter.gameObject.SetActive(true);
                charOneMage.gameObject.SetActive(false);
            } 
            else
            {
                playerOneCharacter = 2;
                charOneFighter.gameObject.SetActive(false);
                charOneMage.gameObject.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            //swap character
            if (playerTwoCharacter == 2)
            {
                playerTwoCharacter = 1;
                charTwoFighter.gameObject.SetActive(true);
                charTwoMage.gameObject.SetActive(false);
            }
            else
            {
                playerTwoCharacter = 2;
                charTwoFighter.gameObject.SetActive(false);
                charTwoMage.gameObject.SetActive(true);
            }
        }
        Debug.Log(playerOneCharacter);
    }

    public int getPlayerOne()
    {
        return this.playerOneCharacter;
    }

    public int getPlayerTwo()
    {
        return this.playerTwoCharacter;
    }

    //keeps object from being destroyed on next scene
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
