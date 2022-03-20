using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameMechanics : MonoBehaviour
{

    private bool timesUp;
    private bool outOfLivesPlayerOne;
    private bool outOfLivesPlayerTwo;

    public Timer timer;
    public PlayerStats playerOne;
    public PlayerStats playerTwo;

    // Start is called before the first frame update
    void Start()
    {
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
                } else
                {
                    return 2;
                }
            }
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
