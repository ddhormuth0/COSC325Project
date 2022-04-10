using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Start is called before the first frame update
    public int PlayerMaxHealth = 2000;
    int maxLives = 3;
    int currentHealth;
    int lives;
    bool outOfLives;
    bool invincible;
    private Vector2 start;
    private bool tookDamage;
    public Player1Mage mage1;
    public Player2Mage mage2;

    public GameObject lifeOne;
    public GameObject lifeTwo;
    public GameObject lifeThree;
    public Transform position;
    public SpriteRenderer sprite;
    
    


    public HealthBar healthBar;

    void Start()
    {
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
        start = position.position;
        outOfLives = false;
        invincible = false;
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
            resetCharacter();
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
        tookDamage = false;
    }

    // if damage is taken subtract from health bar and update it
    // if its a mage then the flash red will continue until the player stops attacking
    public void takeDamage(int damage, bool isMage, int playerAttacking)
    {
        if (!invincible)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            tookDamage = true;
            //for mage one
            if(isMage && playerAttacking == 1)
            {
                StartCoroutine(MageOneAttackFlashRed());
            }
            //for mage 2 
            else if (isMage && playerAttacking == 2)
            {
                
                StartCoroutine(MageTwoAttackFlashRed());
            }
            //for fighter
            else
            {
                StartCoroutine(FlashRed());
            }
            
        }
    }

    public IEnumerator FlashRed()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(.1f);
        sprite.color = Color.white;
    }

    public IEnumerator MageOneAttackFlashRed()
    {
        sprite.color = Color.red;
        yield return new WaitUntil(() => !(Input.GetKey(KeyCode.Q) && mage1.getHitting()));
        sprite.color = Color.white;
    }

    public IEnumerator MageTwoAttackFlashRed()
    {
        sprite.color = Color.red;
        yield return new WaitUntil(() => !(Input.GetKey(KeyCode.O) && mage2.getHitting()));
        sprite.color = Color.white;
        
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

    private void resetCharacter()
    {
        //move back to start
        position.position = start;
        //make invisible
        invincible = true;
        float timer = 1f;
        while(timer > 0)
        {
            timer -= Time.deltaTime;

        }
        invincible = false;
    }

    public bool getTookDamage()
    {
        return tookDamage;
    }

}
