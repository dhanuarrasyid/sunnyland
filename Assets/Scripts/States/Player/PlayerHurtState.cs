using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : PlayerState
{
    public override void Enter(Character character)
    {
        base.Enter(character);
        character.CharAnimator.SetBool("IsHurt", true);
    }

    public override void Exit()
    {
        base.Exit();
        character.CharAnimator.SetBool("IsHurt", false);
    }

    public override void Update()
    {
        base.Update();

        if(!character.IsImmune)
        {
            character.ChangeState(new PlayerIdleState());
        }
    }
}
