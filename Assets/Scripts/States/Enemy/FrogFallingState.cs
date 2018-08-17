using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogFallingState : FallingState {
    public override ICharacterState IdleState
    {
        get { return new FrogIdleState(); }
    }

    public override ICharacterState ClimbState
    {
        get { return new FrogIdleState(); }
    }

    public override void Enter(Character character)
    {
        base.Enter(character);
        speedModifier = 200f;
    }
}
