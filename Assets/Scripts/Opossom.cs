using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossom : Enemy {
    float patrolTime = 3f;
    float timer;
	// Use this for initialization
	void Start () {
        facingRight = false;
        HorizontalMove = -1;
        timer = patrolTime;
        ChangeState(new OpossomRunState());
	}

    protected override void GetInput()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            HorizontalMove *= -1;
            timer = patrolTime;
        }
    }
}
