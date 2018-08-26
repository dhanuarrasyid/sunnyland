using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy {

    float jumpTimer;
    float moveInput = 1f;

    private float JumpTime
    {
        get { return Random.Range(1, 5); }
    }

    private void Start()
    {
        facingRight = false;
        jumpTimer = JumpTime;
        ChangeState(new FrogIdleState());
    }

    protected override void GetInput()
    {
        if(!IsGrounded())
        {
            HorizontalMove = moveInput;
        } else
        {
            HorizontalMove = 0;
            jumpTimer -= Time.deltaTime;
            if (jumpTimer < 0)
            {
                jumpTimer = JumpTime;
                moveInput *= -1;
                JumpTriggered = true;
            }
        }
    }
}
