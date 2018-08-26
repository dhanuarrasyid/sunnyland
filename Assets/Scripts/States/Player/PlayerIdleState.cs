using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IdleState
{
    ICharacterState JumpState
    {
        get { return new PlayerJumpState(); }
    }

    ICharacterState RunState
    {
        get { return new PlayerRunState(); }   
    }

    ICharacterState CrouchState
    {
        get { return new PlayerCrouchState(); }
    }

    ICharacterState FallingState
    {
        get { return new PlayerFallingState(); }
    }

    ICharacterState ClimbState
    {
        get { return new PlayerClimbState(); }
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
