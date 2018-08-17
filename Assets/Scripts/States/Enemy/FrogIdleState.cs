using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogIdleState : IdleState
{
    public override ICharacterState JumpState
    {
        get { return new FrogJumpState(); }
    }

    public override ICharacterState RunState
    {
        get { return new FrogRunState(); }
    }

    public override ICharacterState CrouchState
    {
        get { return new FrogIdleState(); }
    }

    public override ICharacterState FallingState
    {
        get { return new FrogFallingState(); }
    }

    public override ICharacterState ClimbState
    {
        get { return new FrogIdleState(); }
    }

    public override void Enter(Character character)
    {
        base.Enter(character);
        this.character.JumpTriggered = false;
        this.character.HorizontalMove = 0;
    }

    public override void Update()
    {
        if ((int)Time.timeSinceLevelLoad % 3 == 0)
        {
            this.character.JumpTriggered = true;
        }
        base.Update(); 
    }
}
