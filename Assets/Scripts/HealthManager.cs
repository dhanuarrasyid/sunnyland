using UnityEngine.UI;
using UnityEngine;

public class HealthManager : MonoBehaviour {
    public int health;
    public Slider healthBar;
    
    private float immuneLength = .5f;
    private float lastHitAt = 0f;

    public int Health
    {
        get
        {
            return health;
        }
        private set{
            health = value;
            if(healthBar != null)
            {
                healthBar.value = value;    
            }
        }
    }

    private void Awake()
    {
        if (healthBar != null)
        {
            healthBar.maxValue = health;
        }
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
