using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadsState : IState<Character>
{
    public void OnEnter(Character t)
    {
        t.animator.SetBool("IsDead", true);
    }

    public void OnExecute(Character t)
    {

    }

    public void OnExit(Character t)
    {
        t.animator.SetBool("IsDead", false);
        //t.currentState.OnExit(t);
    }
}
