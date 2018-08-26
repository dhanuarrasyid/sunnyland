using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : CharacterState
{

    private float deathTimer = .5f;
    public override void Enter(Character character)
    {
        base.Enter(character);
        this.character.CharAnimator.SetBool("IsDead", true);
    }

    public override void Exit()
    {
        base.Exit();
        character.Die();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        deathTimer -= Time.deltaTime;
        if (deathTimer <= 0)
        {
            character.ChangeState(new PlayerIdleState());
        }
    }
}
