using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FallingState : CharacterState {

    public abstract ICharacterState IdleState
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
        this.character.CharAnimator.SetBool("IsFalling", true);
    }

    public override void Exit()
    {
        base.Exit();
        this.character.CharAnimator.SetBool("IsFalling", false);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (this.character.IsGrounded())
        {
            this.character.ChangeState(IdleState);
        }
    }

    public override void Update()
    {
        base.Update();
        if (this.character.CanClimb && this.character.VerticalMove > 0)
        {
            this.character.ChangeState(ClimbState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        this.character.Move(this.character.HorizontalMove * speedModifier * Time.fixedDeltaTime);
    }
}
