using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour {
    public CharacterController2D controller;
    public float horizontalSpeed = 20f;
    float horizontalMove = 0f;
    float verticalMove = 0;
    bool jump = false;
    float timer = 0;

    public Animator animator;

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > 3)
        {
            timer = 0;
            horizontalSpeed *= -1;
            horizontalMove = horizontalSpeed;
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

    public void OnJump()
    {
        animator.SetBool("IsJumping", true);
    }

    public void OnFall()
    {
        animator.SetBool("IsFalling", true);
        animator.SetBool("IsJumping", false);
    }

    public void OnLanding()
    {
        horizontalMove = 0;
        animator.SetBool("IsFalling", false);
        animator.SetBool("IsJumping", false);
    }
}
