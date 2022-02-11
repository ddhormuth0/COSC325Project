using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.O) && attackTime <= 0)
        {
            Vector3 directionThree = direction + Vector2.up;
            RaycastHit2D hit = Physics2D.Raycast(character.gameObject.transform.position + directionThree, direction, 1f, layerMask);
            Debug.DrawRay(character.gameObject.transform.position + directionThree, direction * 5, Color.red, 3f);
            animate.SetTrigger("Attack" + 1);
            attackTime = .5f;
            if (hit.collider != null)
            {
                Debug.Log("hitting: " + hit.collider.tag);
            }

        }
        //move left
        if (Input.GetKey(KeyCode.J))
        {
            direction = Vector2.left / 2f;
            horizontalMove = -1f * runSpeed;
            animate.SetInteger("AnimState", 1);
            idolTimer = 0.02f;
        }
        //move right
        else if (Input.GetKey(KeyCode.L))
        {
            direction = Vector2.right / 2;
            horizontalMove = 1f * runSpeed;
            animate.SetInteger("AnimState", 1);
            idolTimer = 0.02f;
        }

        //for jumping
        if (Input.GetKeyDown(KeyCode.I))
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
        attackTime -= Time.deltaTime;
        idolTimer -= Time.deltaTime;
        if (idolTimer < 0)
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
