using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogIdleState : IdleState
{
    ICharacterState JumpState
    {
        get { return new FrogJumpState(); }
    }


    ICharacterState FallingState
    {
        get { return new FrogFallingState(); }
    }

    public override void Enter(Character character)
    {
        base.Enter(character);
        this.character.JumpTriggered = false;
        this.character.HorizontalMove = 0;
    }

    public override void Update()
    {
        base.Update();

        if (this.character.JumpTriggered)
        {
            this.character.ChangeState(JumpState);
        } else if (this.character.IsFalling())
        {
            this.character.ChangeState(FallingState);
        }
    }

}
