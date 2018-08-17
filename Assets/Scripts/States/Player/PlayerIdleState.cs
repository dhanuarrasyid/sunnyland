using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IdleState
{
    public override ICharacterState JumpState
    {
        get { return new PlayerJumpState(); }
    }

    public override ICharacterState RunState
    {
        get { return new PlayerRunState(); }   
    }

    public override ICharacterState CrouchState
    {
        get { return new PlayerCrouchState(); }
    }

    public override ICharacterState FallingState
    {
        get { return new PlayerFallingState(); }
    }

    public override ICharacterState ClimbState
    {
        get { return new PlayerClimbState(); }
    }

  
}
