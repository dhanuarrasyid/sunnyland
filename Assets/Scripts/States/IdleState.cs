using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IdleState : CharacterState {

    public abstract ICharacterState JumpState
    {
        get;
    }

    public abstract ICharacterState RunState
    {
        get;
    }

    public abstract ICharacterState CrouchState
    {
        get;
    }

    public abstract ICharacterState FallingState
    {
        get;
    }

    public abstract ICharacterState ClimbState
    {
        get;
    }

    public override void Enter(Character character)
    {
        base.Enter(character);
        this.character.CharAnimator.SetFloat("Speed", 0);
        this.character.CharAnimator.SetFloat("VerticalSpeed", 0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        this.character.Move(0);
    }

    public override void Update()
    {
        base.Update();

        if (this.character.IsRunning)
        {
            this.character.ChangeState(RunState);
        }
        else if (this.character.JumpTriggered)
        {
            Debug.Log("JumpTriggered: " + this.character.JumpTriggered);
            this.character.ChangeState(JumpState);
        }
        else if (this.character.IsGrounded() && this.character.VerticalMove < 0)
        {
            this.character.ChangeState(CrouchState);
        }
        else if (this.character.IsFalling())
        {
            this.character.ChangeState(FallingState);
        }
        else if (this.character.CanClimb && this.character.VerticalMove > 0)
        {
            this.character.ChangeState(ClimbState);
        }
    }
}
