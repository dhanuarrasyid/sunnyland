using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

    }
    public override void Hit(int amount)
    {
        base.Hit(amount);
        if (!healthManager.IsAlive)
        {

            Die();

        }

    }
}
