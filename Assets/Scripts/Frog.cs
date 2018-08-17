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
        
    }
}
