using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IdleState : CharacterState {
    public override void Enter(Character character)
    {
        base.Enter(character);
        this.character.CharAnimator.SetFloat("Speed", 0);
        this.character.CharAnimator.SetFloat("VerticalSpeed", 0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        this.character.Move(0);
    }
}
