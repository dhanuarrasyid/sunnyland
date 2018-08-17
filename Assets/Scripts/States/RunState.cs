using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RunState : CharacterState {
    public abstract ICharacterState IdleState
    {
        get;
    }
    public abstract ICharacterState JumpState
    {
        get;
    }
    public abstract ICharacterState SlideState
    {
        get;
    }

    private float speedModifier = 400f;
    public override void Enter(Character character)
    {
        base.Enter(character);
        this.character.CharAnimator.SetFloat("Speed", 40);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        this.character.Move(this.character.HorizontalMove * speedModifier * Time.deltaTime);
    }

    public override void Update()
    {
        base.Update();
        if (Mathf.Abs(this.character.HorizontalMove) < 0.01)
        {
            this.character.ChangeState(new PlayerIdleState());
        }
        else if (this.character.JumpTriggered)
        {
            this.character.ChangeState(new PlayerJumpState());
        }
        else if (this.character.IsGrounded() && this.character.VerticalMove < 0)
        {
            this.character.ChangeState(new PlayerSlideState());
        }
    }
}
