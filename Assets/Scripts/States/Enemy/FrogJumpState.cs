using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogJumpState : JumpState {

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
        speedModifier = 200f;
        if((int)Time.timeSinceLevelLoad % 6 == 0)
        {
            this.character.HorizontalMove = 1;
        } else
        {
            this.character.HorizontalMove = -1; 
        }
    }

}
