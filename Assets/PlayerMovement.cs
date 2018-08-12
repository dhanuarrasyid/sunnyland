using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;
    public float runSpeed = 40f;
    public float climbSpeed = 20f;
    public Animator animator;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool climb = false;

	
	// Update is called once per frame
	void Update () {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * climbSpeed;
        crouch = verticalMove < 0;
        climb = Mathf.Abs(verticalMove) > 0;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
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
        animator.SetBool("IsJumping", false);
    }

    public void OffGround()
    {
        Debug.Log("Off Ground");
        animator.SetBool("IsJumping", jump);
    }

    public void OnCrouch()
    {
        animator.SetBool("IsCrouching", crouch);
    }

    public void OnClimb()
    {
        Debug.Log("Climbing"); 
        animator.SetBool("IsClimbing", climb);
    }
}
