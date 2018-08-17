using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : RunState
{
    public override ICharacterState IdleState
    {
        get { return new PlayerIdleState(); }
    }

    public override ICharacterState JumpState
    {
        get { return new PlayerJumpState(); }
    }

    public override ICharacterState SlideState
    {
        get { return new PlayerSlideState(); }
    }
}
