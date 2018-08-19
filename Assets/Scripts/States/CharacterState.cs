using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterState : ICharacterState {
    protected Character character;

    public virtual void Enter(Character character)
    {
        Log("Enter: ");
        this.character = character;
    }
    public virtual void Exit()
    {
        Log("Exit: ");
    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Log("OnTriggerEnter: ");
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        Log("OnTriggerExit");
    }

    public virtual void Update()
    {

    }

    private void Log(string str)
    {
        if (this.character != null)
        {
            //Debug.Log(this.character.ToString() + " " + str + " " + this.ToString());
        }
    }
	
}
