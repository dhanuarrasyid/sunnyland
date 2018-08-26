using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : RunState
{
    ICharacterState IdleState
    {
        get { return new PlayerIdleState(); }
    }

    ICharacterState JumpState
    {
        get { return new PlayerJumpState(); }
    }

    ICharacterState SlideState
    {
        get { return new PlayerSlideState(); }
    }

    public override void Update()
    {
        base.Update();
        if (Mathf.Abs(this.character.HorizontalMove) < 0.01)
        {
            this.character.ChangeState(IdleState);
        }
        else if (this.character.JumpTriggered)
        {
            this.character.ChangeState(JumpState);
        }
        else if (this.character.IsGrounded() && this.character.VerticalMove < 0)
        {
            this.character.ChangeState(SlideState);
        }
    }
}
