using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {
    
    private void Start()
    {
        facingRight = true;
        ChangeState(new PlayerIdleState());
    }

    protected override void GetInput()
    {
        HorizontalMove = Input.GetAxisRaw("Horizontal");
        VerticalMove = Input.GetAxisRaw("Vertical");
        JumpTriggered = Input.GetButtonDown("Jump");
    }

    public override void Hit(int amount)
    {
        base.Hit(amount);
        if(healthManager.IsAlive)
        {
            ChangeState(new PlayerHurtState());

        } else 
        {
            
            ChangeState(new DeadState());

        }

    }
}
