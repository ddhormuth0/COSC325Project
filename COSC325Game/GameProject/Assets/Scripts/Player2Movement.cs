using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : MonoBehaviour
{

    public CharacterController2D controller;
    Rigidbody2D thisPlayer;

    public float runSpeed = 40f;
    public int basicAttack = 50;
    public float maxBlockTime = 3f;

    float horizontalMove = 0f;

    private bool canBlock;
    private bool isBlocking;
    private float blockTime;
    private bool isJumping = false;
    private Animator animate;
    private float idolTimer;
    private bool isGrounded;
    private Rigidbody2D m_body2d;
    private float attackTime;
    private Vector2 direction;
    private BoxCollider2D character;
    private int layerMask;
    private float startTime;



    // Start is called before the first frame update
    void Start()
    {

        thisPlayer = this.gameObject.GetComponent<Rigidbody2D>();
        startTime = 3;
        canBlock = true;
        isBlocking = false;
        blockTime = maxBlockTime;
        layerMask = LayerMask.GetMask("Player");
        attackTime = 0f;
        animate = GetComponent<Animator>();
        isGrounded = controller.GetGrounded();
        m_body2d = GetComponent<Rigidbody2D>();
        character = GetComponent<BoxCollider2D>();
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
            thisPlayer.WakeUp();

            //attacking
            if (Input.GetKeyDown(KeyCode.O) && attackTime <= 0)
            {
                //convert our 2d movment direction vectro into a vector3
                Vector3 directionThree = direction + Vector2.up;
                //shoots a ray out from the character and detects the item that it hits, only hits players
                RaycastHit2D hit = Physics2D.Raycast(character.gameObject.transform.position + directionThree, direction, 1f, layerMask);
                //a debug that shows us the swing radius of the sword attack
                Debug.DrawRay(character.gameObject.transform.position + directionThree, direction * 5, Color.red, 3f);
                //sets the animation state to attack
                animate.SetTrigger("Attack" + 1);
                //sets a timer of .5 until the next attack can be made
                attackTime = .5f;
                if (hit.collider != null)
                {
                    //debug tool that tells us what we hit with the basic attack
                    PlayerStats player = hit.transform.GetComponent<PlayerStats>();
                    Player1Movement playerBlock = hit.transform.GetComponent<Player1Movement>();

                    //if player is not blocking
                    if (!playerBlock.getBlocking())
                    {
                        player.takeDamage(basicAttack);
                        Debug.Log(player.getHealth());
                    }

                }

            }
            //move left
            if (Input.GetKey(KeyCode.J) && !isBlocking)
            {
                //vector direction that we are moving in
                direction = Vector2.left / 2f;
                //sets horizontal movement to go left
                horizontalMove = -1f * runSpeed;
                //sets our animation state to the run animation
                animate.SetInteger("AnimState", 1);
                idolTimer = 0.02f;
            }
            //move right
            else if (Input.GetKey(KeyCode.L) && !isBlocking)
            {
                //vector direction we are moving in
                direction = Vector2.right / 2;
                //sets horizontal movement to go right * our movement speed
                horizontalMove = 1f * runSpeed;
                //sets our animation state to the run animation
                animate.SetInteger("AnimState", 1);
                idolTimer = 0.02f;
            }
            //blocking
            if (Input.GetKey(KeyCode.K) && canBlock && blockTime > 0f)
            {
                isBlocking = true;
                blockTime -= Time.deltaTime;
                animate.SetTrigger("Block");
                animate.SetBool("IdleBlock", true);
                Debug.Log(blockTime);
            }
            else
            {
                isBlocking = false;
                Debug.Log(blockTime);
                if (blockTime < 3f)
                {
                    canBlock = false;
                    blockTime += Time.deltaTime;
                }
                else
                {
                    canBlock = true;
                }

                animate.SetBool("IdleBlock", false);
            }

            //for jumping
            if (Input.GetKeyDown(KeyCode.I) && !isBlocking)
            {
                isJumping = true;
                if (isGrounded)
                {
                    animate.SetTrigger("Jump");
                }
            }
            animate.SetBool("Grounded", isGrounded);
            isGrounded = controller.GetGrounded();

            animate.SetFloat("AirSpeedY", m_body2d.velocity.y);

            //for going idol and resetting basick attack.0
            attackTime -= Time.deltaTime;
            idolTimer -= Time.deltaTime;
            if (idolTimer < 0)
            {
                animate.SetInteger("AnimState", 0);
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
    }

    public bool getBlocking()
    {
        return isBlocking;
    }
}
