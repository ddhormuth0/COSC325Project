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


    // Start is called before the first frame update
    void Start()
    {
        animate = GetComponent<Animator>();
        isGrounded = controller.GetGrounded();
        m_body2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //move left
        if (Input.GetKey(KeyCode.A))
        {
            horizontalMove = -1f * runSpeed;
            animate.SetInteger("AnimState", 1);
            idolTimer = 0.02f;
        }
        //move right
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalMove = 1f * runSpeed;
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

        //for going idol
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
