using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : PlayerState
{
    private float speedModifier = 200f;
    public override void Enter(Character character)
    {
        base.Enter(character);
        this.character.Crouch(true);
        this.character.CharAnimator.SetBool("IsCrouching", true);
    }

    public override void Exit()
    {
        base.Exit();
        this.character.Crouch(false);
        this.character.CharAnimator.SetBool("IsCrouching", false);
    }

    public override void Update()
    {
        base.Update();
        if(this.character.VerticalMove >= 0 && this.character.CanStand)
        {
            if(this.character.IsRunning)
            {
                this.character.ChangeState(new PlayerRunState());
            } else
            {
                this.character.ChangeState(new PlayerIdleState());
            }
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        this.character.Move(this.character.HorizontalMove * speedModifier * Time.deltaTime);
    }
}
