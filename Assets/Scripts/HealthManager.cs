using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {
    public int health;
    private float immuneLength = .5f;
    private float lastHitAt = 0f;

    public int Health
    {
        get;
        private set;
    }

    private void Awake()
    {
        Health = health;
    }

    public bool IsImmune
    {
        get 
        { 
            return lastHitAt + immuneLength > Time.timeSinceLevelLoad; 
        }
    }

    public bool IsAlive
    {
        get { return Health > 0; }

    }

    public void Hit()
    {
        Hit(1);
    }

    public void Hit(int amount)
    {
        if(!IsImmune)
        {
            Health -= amount;
            lastHitAt = Time.timeSinceLevelLoad;
            Debug.Log(GetComponent<Character>().ToString() + " has been hit. Life: " + Health);

        } else
        {
            Debug.Log(GetComponent<Character>().ToString() + " is immune. Life: " + Health);
        }
    }

    public void Recharge(int amount)
    {
        Health += amount;
    }

}
