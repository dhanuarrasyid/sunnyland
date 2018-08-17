using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogRunState : RunState {

    public override ICharacterState IdleState
    {
        get { return new FrogIdleState(); }
    }

    public override ICharacterState JumpState
    {
        get { return new FrogJumpState(); }
    }

    public override ICharacterState SlideState
    {
        get { return new FrogIdleState(); }
    }
}
