using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaglePatrolState : CharacterState
{
    protected virtual float SpeedModifier
    {
        get { return 200f; }
    }

    public override void Enter(Character character)
    {
        base.Enter(character);
        this.character.CharAnimator.SetFloat("Speed", 40);
    }


    public override void FixedUpdate()
    {
        base.FixedUpdate();
        this.character.Move(this.character.HorizontalMove * SpeedModifier * Time.deltaTime,
                            this.character.VerticalMove * SpeedModifier * Time.deltaTime);
    }


}
