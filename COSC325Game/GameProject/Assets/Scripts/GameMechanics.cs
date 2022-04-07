using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameMechanics : MonoBehaviour
{

    private bool timesUp;
    private bool outOfLivesPlayerOne;
    private bool outOfLivesPlayerTwo;
    private int playerOneCharacter;
    private int playerTwoCharacter;
    private CharacterSelection characterSelection;

    public Timer timer;
    public GameObject fighterOne;
    public GameObject fighterTwo;
    public GameObject mageOne;
    public GameObject mageTwo;
    private PlayerStats playerOne;
    private PlayerStats playerTwo;
    public PlayerStats playerOneFighter;
    public PlayerStats playerTwoFighter;
    public PlayerStats playerOneMage;
    public PlayerStats playerTwoMage;

    // Start is called before the first frame update
    void Start()
    {
        characterSelection = GameObject.Find("CharacterSelection").transform.GetComponent<CharacterSelection>();
        //get which character is selected
        playerOneCharacter = characterSelection.getPlayerOne();
        playerTwoCharacter = characterSelection.getPlayerTwo();
        //if playerOneCharacter equals one then the fighter is selected else the mage is selected
        //set the correct character to active and select the correct stat script
        if(playerOneCharacter == 1)
        {
            playerOne = playerOneFighter;
            fighterOne.SetActive(true);
            mageOne.SetActive(false);

        }
        else
        {
            playerOne = playerOneMage;
            fighterOne.SetActive(false);
            mageOne.SetActive(true);
        }

        //if playerOneCharacter equals one then the fighter is selected
        if (playerTwoCharacter == 1)
        {
            playerTwo = playerTwoFighter;
            fighterTwo.SetActive(true);
            mageTwo.SetActive(false);

        }
        else
        {
            playerTwo = playerTwoMage;
            fighterTwo.SetActive(false);
            mageTwo.SetActive(true);
        }

        //get lives and time booleans
        timesUp = timer.getTimeUP();
        outOfLivesPlayerOne = playerOne.getOutOfLives();
        outOfLivesPlayerTwo = playerTwo.getOutOfLives();
    }

    // Update is called once per frame
    void Update()
    {
        //update time and lives boolean
        timesUp = timer.getTimeUP();
        outOfLivesPlayerOne = playerOne.getOutOfLives();
        outOfLivesPlayerTwo = playerTwo.getOutOfLives();

        //if out of lives or time is over display end screen
        if ((timesUp || outOfLivesPlayerOne || outOfLivesPlayerTwo) && SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    //return who won
    public int getWhoWon()
    {
        //if player one died
        if (outOfLivesPlayerOne)
        {
            return 2;
        } 
        //if player two died
        else if (outOfLivesPlayerTwo)
        {
            return 1;
        } 
        
        //if time ran out
        else
        {
            //if player one has more lives
            if(playerOne.getNumberOfLives() > playerTwo.getNumberOfLives())
            {
                return 1;
            } 
            //if player two has more lives
            else if(playerOne.getNumberOfLives() < playerTwo.getNumberOfLives())
            {
                return 2;
            }
            //same amount of lives
            else
            {
                //if player one has higher proportional health they win
                if((playerOne.getHealth()/playerOne.getMaxHealth()) > (playerTwo.getHealth() / playerTwo.getMaxHealth()))
                {
                    return 1;
                } 
                else if ((playerOne.getHealth() / playerOne.getMaxHealth()) < (playerTwo.getHealth() / playerTwo.getMaxHealth()))
                {
                    return 2;
                } 
                //draw
                else
                {
                    return 0;
                }
            }
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
