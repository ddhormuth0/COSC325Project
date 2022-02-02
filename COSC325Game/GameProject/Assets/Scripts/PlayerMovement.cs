using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;

    bool isJumping = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move left
        if (Input.GetKey(KeyCode.A))
        {
            horizontalMove = -1f * runSpeed;
        }
        //move right
        if (Input.GetKey(KeyCode.D))
        {
            horizontalMove = 1f * runSpeed;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            isJumping = true;
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
