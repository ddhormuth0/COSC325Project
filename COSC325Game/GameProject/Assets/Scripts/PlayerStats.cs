using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Start is called before the first frame update
    int PlayerMaxHealth = 2000;
    int maxLives = 3;
    int currentHealth;
    int lives;
    bool outOfLives;

    public GameObject lifeOne;
    public GameObject lifeTwo;
    public GameObject lifeThree;

    public HealthBar healthBar;

    void Start()
    {
        outOfLives = false;
        //set max lives
        lives = maxLives;
        //set current health and set up health bar
        currentHealth = PlayerMaxHealth;
        healthBar.SetMaxHealth(PlayerMaxHealth);
    }

    private void Update()
    {
        //when a player has 0 health delete a life
        if(currentHealth <= 0)
        {
            lives--;
            currentHealth = PlayerMaxHealth;
            healthBar.SetHealth(PlayerMaxHealth);

            if(lives == 2)
            {
                lifeThree.SetActive(false);
            }
            if (lives == 1)
            {
                lifeTwo.SetActive(false);
            }
            if (lives == 0)
            {
                outOfLives = true;
            }
        }

        
    }

    // if damage is taken subtract from health bar and update it
    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public int getHealth()
    {
        return currentHealth;
    }

    public bool getOutOfLives()
    {
        return this.outOfLives;
    }

    public int getNumberOfLives()
    {
        return this.lives;
    }

    public int getMaxHealth()
    {
        return this.PlayerMaxHealth;
    }
}
