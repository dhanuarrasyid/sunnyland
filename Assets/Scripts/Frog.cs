using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy {



    private void Start()
    {
        facingRight = false;
        ChangeState(new FrogIdleState());
    }

    protected override void GetInput()
    {
        if(!IsGrounded())
        {
            if ((int)Time.timeSinceLevelLoad % 6 == 0)
            {
                HorizontalMove = 1;
            }
            else
            {
                HorizontalMove = -1;
            }
        } else
        {
            HorizontalMove = 0;
            if ((int)Time.timeSinceLevelLoad % 3 == 0)
            {
                JumpTriggered = true;
            }
        }
    }
}
