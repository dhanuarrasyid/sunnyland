using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideState : PlayerState
{
    float time;
    float slideDuration = .5f;
    float speedModifier = 400f;
    public override void Enter(Character character)
    {
        base.Enter(character);
        time = 0;
        this.character.Crouch(true);
        // TODO: add slide animation and trigger it
        this.character.CharAnimator.SetBool("IsCrouching", true);
    }

    public override void Exit()
    {
        base.Exit();
        this.character.Crouch(false);
        this.character.CharAnimator.SetBool("IsCrouching", false);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        this.character.Move(this.character.HorizontalMove * speedModifier * Time.deltaTime);
    }

    public override void Update()
    {
        base.Update();
        time += Time.deltaTime;
        if(time > slideDuration)
        {
            this.character.ChangeState(new PlayerCrouchState());
        }
    }
}
