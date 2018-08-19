using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : FallingState
{
    public override ICharacterState IdleState
    {
        get { return new PlayerIdleState(); }
    }

    public override ICharacterState ClimbState
    {
        get { return new PlayerClimbState(); }
    }


}
