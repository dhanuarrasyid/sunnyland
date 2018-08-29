using UnityEngine;

public class PlayerJumpState : JumpState
{
    public override ICharacterState FallingState
    {
        get { return new PlayerFallingState(); }
    }

    public override ICharacterState ClimbState
    {
        get { return new PlayerClimbState(); }
    }

    public override void Enter(Character character)
    {
        base.Enter(character);
        GameManager.am.Play("Jump");
    }
}