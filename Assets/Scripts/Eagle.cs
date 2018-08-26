using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : Enemy {
    float patrolTime = 1.5f;
    float timer;
    // Use this for initialization
    void Start()
    {
        facingRight = false;
        VerticalMove = -1;
        timer = patrolTime;
        ChangeState(new EaglePatrolState());
    }

    protected override void GetInput()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            VerticalMove *= -1;
            timer = patrolTime;
        }
    }
	
}
