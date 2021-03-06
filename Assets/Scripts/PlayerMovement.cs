﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;
    public float runSpeed = 40f;
    public float climbSpeed = 40f;
    public Animator animator;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool climb = false;
    bool climbing = false;
    bool offGround = false;
    bool canClimb = false;

	
	// Update is called once per frame
	void Update () {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * climbSpeed;
        crouch = verticalMove < 0;
        climb = Mathf.Abs(verticalMove) > 0;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        animator.SetFloat("VerticalSpeed", Mathf.Abs(verticalMove));

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

	}

    private void FixedUpdate()
    {
        
        controller.Move(horizontalMove * Time.fixedDeltaTime, 
                        verticalMove * Time.fixedDeltaTime, 
                        jump);
        jump = false;

    }

    public void OnLanding(){
        Debug.Log("Landed");
        offGround = false;
        animator.SetBool("IsFalling", false);
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsClimbing", false);
    }

    public void OffGround()
    {
        Debug.Log("Off Ground");
        offGround = true;
        //animator.SetBool("IsJumping", jump);
    }

    public void OnCrouch(bool isCrouched)
    {
        
        Debug.Log("Crouch Event: " + isCrouched);
        animator.SetBool("IsClimbing", false);
        animator.SetBool("IsCrouching", crouch);
    }

    public void OnClimbEnter()
    {
        Debug.Log("Climb Started");
        animator.SetBool("IsClimbing", true);
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsFalling", false);
    }

    public void OnClimbExit()
    {
        Debug.Log("Climb Ended");
        animator.SetBool("IsClimbing", false);
    }

    public void OnJump()
    {
        animator.SetBool("IsJumping", true);
    }

    public void OnFall()
    {
        animator.SetBool("IsFalling", true);
        animator.SetBool("IsJumping", false);
    }
}
