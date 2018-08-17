using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbState : PlayerState
{
    float speedModifier = 200f;

    public override void Enter(Character character)
    {
        base.Enter(character);
        this.character.CharAnimator.SetBool("IsClimbing", true);
        this.character.Climb(true);
    }

    public override void Exit()
    {
        base.Exit();
        this.character.CharAnimator.SetBool("IsClimbing", false);
        this.character.CharAnimator.SetFloat("VerticalSpeed", 0);
        this.character.Climb(false);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        this.character.Move(this.character.HorizontalMove * speedModifier * Time.deltaTime,
                            this.character.VerticalMove * speedModifier * Time.deltaTime);
    }

    public override void Update()
    {
        base.Update();
        this.character.CharAnimator.SetFloat("VerticalSpeed", Mathf.Abs(this.character.VerticalMove));
        this.character.CharAnimator.SetFloat("Speed", Mathf.Abs(this.character.HorizontalMove));
        if(this.character.VerticalMove < 0 && this.character.IsGrounded())
        {
            this.character.ChangeState(new PlayerCrouchState());
        } else if (!this.character.CanClimb)
        {
            this.character.ChangeState(new PlayerIdleState());
        } else if (this.character.JumpTriggered)
        {
            this.character.ChangeState(new PlayerJumpState());
        }
    }
}
