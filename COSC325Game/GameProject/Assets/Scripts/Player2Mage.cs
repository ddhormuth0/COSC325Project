using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Mage : MonoBehaviour
{

    public CharacterController2D controller;
    Rigidbody2D thisPlayer;

    public float runSpeed = 90f;
    public int basicAttack = 50;
    public float maxBlockTime = 3f;
    public GameObject circle;

    float horizontalMove = 0f;

    private bool isHitting;
    private bool canBlock;
    private bool isBlocking;
    private float blockTime;
    private bool isJumping = false;
    private Animator animate;
    private float idolTimer;
    private bool isGrounded;
    private Rigidbody2D m_body2d;
    private Vector2 direction;
    private BoxCollider2D character;
    private int layerFighter;
    private int layerMage;
    private float startTime;
    private float maxMana = 3f;
    private float mana;
    private bool isAttacking;
    private bool canAttack;
    public ManaBar manaBar;
    public Image shield;



    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        mana = maxMana;
        thisPlayer = this.gameObject.GetComponent<Rigidbody2D>();
        startTime = 3;
        canBlock = true;
        isBlocking = false;
        blockTime = maxBlockTime;
        layerFighter = LayerMask.GetMask("Fighter");
        layerMage = LayerMask.GetMask("Wizard");
        animate = GetComponent<Animator>();
        isGrounded = controller.GetGrounded();
        m_body2d = GetComponent<Rigidbody2D>();
        character = GetComponent<BoxCollider2D>();
        isHitting = false;
        circle.SetActive(false);
        manaBar.SetMaxMana(maxMana);
    }

    // Update is called once per frame
    void Update()
    {


        if (startTime > 0)
        {
            startTime -= Time.deltaTime;
        }
        else
        {
            //stops the player from floating
            thisPlayer.WakeUp();

            //move left
            if (Input.GetKey(KeyCode.J) && !isBlocking && !isAttacking)
            {
                //vector direction that we are moving in
                direction = Vector2.left / 2f;
                //sets horizontal movement to go left
                horizontalMove = -1f * runSpeed;
                //sets our animation state to the run animation
                animate.SetBool("isRunning", true);
                idolTimer = 0.02f;
            }
            //move right
            else if (Input.GetKey(KeyCode.L) && !isBlocking && !isAttacking)
            {
                //vector direction we are moving in
                direction = Vector2.right / 2;
                //sets horizontal movement to go right * our movement speed
                horizontalMove = 1f * runSpeed;
                //sets our animation state to the run animation
                animate.SetBool("isRunning", true);
                idolTimer = 0.02f;
            }
            //blocking
            if (Input.GetKey(KeyCode.K) && canBlock && blockTime > 0f)
            {
                isBlocking = true;
                blockTime -= Time.deltaTime;
                //animate.SetBool("IdleBlock", true);
                circle.SetActive(true);
            }
            else
            {
                circle.SetActive(false);
                isBlocking = false;

                if (blockTime < maxBlockTime)
                {
                    canBlock = false;
                    shield.color = new Color(1, 1, 1, .2f);
                    blockTime += Time.deltaTime;
                }
                else
                {
                    canBlock = true;
                    shield.color = new Color(1, 1, 1, 1f);
                }

            }

            //for jumping
            if (Input.GetKeyDown(KeyCode.I) && !isBlocking)
            {
                isJumping = true;
            }

            isGrounded = controller.GetGrounded();



            //for going idol
            idolTimer -= Time.deltaTime;
            if (idolTimer < 0)
            {
                animate.SetBool("isRunning", false);
            }

        }
    }

    private void FixedUpdate()
    {
        //move the character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, isJumping);
        //when key is not pressed set back to 0
        horizontalMove = 0f;

        isJumping = false;

        //attacking
        if (Input.GetKey(KeyCode.O) && !isBlocking && canAttack)
        {

            //convert our 2d movment direction vectro into a vector3
            Vector3 directionThree = direction + Vector2.up / 4;
            //shoots a ray out from the character and detects the item that it hits, only hits players
            RaycastHit2D hit = Physics2D.Raycast(character.gameObject.transform.position + directionThree, direction, 1f, layerMage | layerFighter);
            //a debug that shows us the swing radius of the sword attack
            Debug.DrawRay(character.gameObject.transform.position + directionThree, direction * 5, Color.red, 3f);
            //sets the animation state to attack
            animate.SetBool("isAttacking", true);
            isAttacking = true;
            mana -= Time.deltaTime;
            manaBar.SetMana(mana);
            //sets a timer of .5 until the next attack can be made
            if (hit.collider != null)
            {
                //debug tool that tells us what we hit with the basic attack
                PlayerStats player = hit.transform.GetComponent<PlayerStats>();
                //if it is a fighter get fighter script
                if (player.gameObject.layer.Equals(7))
                {
                    Player1Movement playerBlock = hit.transform.GetComponent<Player1Movement>();
                    //if player is not blocking
                    if (!playerBlock.getBlocking())
                    {
                        isHitting = true;
                        player.takeDamage(basicAttack, true, 2);
                        Debug.Log(player.getHealth());
                    }
                    //blocking
                    else
                    {
                        isHitting = false;
                    }
                }
                //it is mage
                else if (player.gameObject.layer.Equals(6))
                {
                    Player1Mage playerBlock = hit.transform.GetComponent<Player1Mage>();
                    //if player is not blocking
                    if (!playerBlock.getBlocking())
                    {
                        isHitting = true;
                        player.takeDamage(basicAttack, true, 2);

                    }
                    //blocking
                    else
                    {
                        isHitting = false;
                    }
                }


            }
            else
            {
                isHitting = false;
            }


            //if out of mana you cant attack
            if (mana <= 0)
            {
                canAttack = false;
            }

        }
        else
        {
            //mana must be full
            if (mana < maxMana)
            {
                canAttack = false;
                mana += Time.deltaTime;
                manaBar.SetMana(mana);
                Debug.Log(mana);
            }
            else
            {
                canAttack = true;
            }
            animate.SetBool("isAttacking", false);
            isAttacking = false;
            isHitting = false;
        }
    }

    public bool getBlocking()
    {
        return isBlocking;
    }

    public bool getHitting()
    {
        return isHitting;
    }
}
