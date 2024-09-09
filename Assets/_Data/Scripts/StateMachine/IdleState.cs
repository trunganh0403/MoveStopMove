using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Character>
{
    public void OnEnter(Character t)
    {
        t.animator.SetBool("IsIdle", true);
    }

    public void OnExecute(Character t)
    {
        t.PlayerRun(t);
        t.BotRun(t);

        if (t.objAttack.isAttack == true)
        {
            t.ChangeState(new AttackState());
        }
    }

    public void OnExit(Character t)
    {
        t.animator.SetBool("IsIdle", false);
    }
}

