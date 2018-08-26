using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogRunState : RunState {

    ICharacterState IdleState
    {
        get { return new FrogIdleState(); }
    }

    ICharacterState JumpState
    {
        get { return new FrogJumpState(); }
    }

    ICharacterState SlideState
    {
        get { return new FrogIdleState(); }
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
