using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : CharacterState
{
    public override void Enter(Character character)
    {
        base.Enter(character);
        this.character.CharAnimator.SetBool("IsDead", true);
        GameManager.am.Play("Dead");
    }
}
