using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Movement : MonoBehaviour
{

    public CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;


    private bool isJumping = false;
    private Animator animate;
    private float idolTimer;
    private bool isGrounded;
    private Rigidbody2D m_body2d;
    private float attackTime;
    private Vector2 direction;
    private BoxCollider2D character;
    private int layerMask;


    // Start is called before the first frame update
    void Start()
    {
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
        //attacking
        if (Input.GetKeyDown(KeyCode.Q) && attackTime <= 0)
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
            if(hit.collider != null)
            {
                //debug tool that tells us what we hit with the basic attack
                Debug.Log("hitting: " + hit.collider.tag);
            }
            
        }
        //move left
        if (Input.GetKey(KeyCode.A))
        {
            //vector direction that we are moving in
            direction = Vector2.left/2f;
            //sets horizontal movement to go left
            horizontalMove = -1f * runSpeed;
            //sets our animation state to the run animation
            animate.SetInteger("AnimState", 1);
            idolTimer = 0.02f;
        }
        //move right
        else if (Input.GetKey(KeyCode.D))
        {
            //vector direction we are moving in
            direction = Vector2.right/2;
            //sets horizontal movement to go right * our movement speed
            horizontalMove = 1f * runSpeed;
            //sets our animation state to the run animation
            animate.SetInteger("AnimState", 1);
            idolTimer = 0.02f;
        }
        
        //for jumping
        if (Input.GetKeyDown(KeyCode.W))
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

        //for going idol and resetting basick attack
        attackTime -= Time.deltaTime;
        idolTimer -= Time.deltaTime;
        if(idolTimer < 0)
        {
            animate.SetInteger("AnimState", 0);
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
}
