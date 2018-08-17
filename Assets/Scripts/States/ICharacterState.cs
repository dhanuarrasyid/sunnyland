using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterState {
    void Enter(Character character);
    void Exit();
    void Update();
    void FixedUpdate();
    void OnTriggerEnter2D(Collider2D collision);
    void OnTriggerExit2D(Collider2D collision);
}
