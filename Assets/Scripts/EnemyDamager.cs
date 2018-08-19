using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour {
    private Character damager;

	// Use this for initialization
	void Start () {
        damager = GetComponentInParent<Character>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")     
        {
            Character character = collision.gameObject.GetComponent<Character>();
            if(character != null)
            {
                damager.Damage(character);
            }
        }
    }
}
