using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class JumpState : CharacterState {

    public abstract ICharacterState FallingState
    {
        get;
    }
    public abstract ICharacterState ClimbState
    {
        get;
    }

    protected float speedModifier = 400f;

    public override void Enter(Character character)
    {
        base.Enter(character);
        this.character.CharAnimator.SetBool("IsJumping", true);
        this.character.Jump();
    }

    public override void Exit()
    {
        base.Exit();
        this.character.CharAnimator.SetBool("IsJumping", false);
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        if (character.IsGrounded())
        {
            character.ChangeState(FallingState);
        }
    }

    public override void Update()
    {
        base.Update();
        //this could probably go in the update method
        if (this.character.IsFalling())
        {
            this.character.ChangeState(FallingState);
        }
        else if (this.character.CanClimb && this.character.VerticalMove > 0)
        {
            this.character.ChangeState(ClimbState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        this.character.Move(this.character.HorizontalMove * speedModifier * Time.fixedDeltaTime);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (character.IsGrounded())
        {
            character.ChangeState(FallingState);
        }
    }
}
