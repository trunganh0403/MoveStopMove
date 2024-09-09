using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public Animator animator;
    public VariableJoystick joystick;
    public NavMeshAgent navMeshAgent;
    public ObjAttack objAttack;
    public IState<Character> currentState;

    public float attackAnimationDuration = 0.8f;

    private void OnEnable()
    {
        ChangeState(new IdleState());
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public void ChangeState(IState<Character> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public virtual void PlayerIdle(Character t)
    {
        if (t.joystick != null)
        {
            if (t.joystick.Horizontal == 0 && t.joystick.Vertical == 0)
            {
                t.ChangeState(new IdleState());
            }
        }
    }

    public virtual void PlayerRun(Character t)
    {
        if (t.joystick != null)
        {
            if (t.joystick.Horizontal != 0 || t.joystick.Vertical != 0)
            {
                t.ChangeState(new RunningState());
            }
        }
    }

    public virtual void BotIdle(Character t)
    {
        if (t.navMeshAgent != null)
        {
            if (navMeshAgent.velocity.magnitude == 0)
            {
                t.ChangeState(new IdleState());
            }
        }
    }

    public virtual void BotRun(Character t)
    {
        if (t.navMeshAgent != null)
        {
            if (navMeshAgent.velocity.magnitude != 0f)
            {
                t.ChangeState(new RunningState());
            }
        }
    }

    public virtual void ObjsAttack(Character t)
    {
        //AnimatorStateInfo stateInfo = t.animator.GetCurrentAnimatorStateInfo(0);

        //if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= (attackAnimationDuration / t.animator.GetCurrentAnimatorStateInfo(0).length))
        //{
        //    t.ChangeState(new IdleState());
        //    t.objAttack.isAttack = false;
        //}

        if (t.objAttack != null && t.objAttack.isAttack)
        {
            AnimatorStateInfo stateInfo = t.animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= (attackAnimationDuration / t.animator.GetCurrentAnimatorStateInfo(0).length))
            {
                t.ChangeState(new IdleState());
                t.objAttack.isAttack = false;
            }
        }
    }

    public bool IsInAttackState()
    {
        return currentState is AttackState;
    }

    public bool IsInIdleState()
    {
        return currentState is IdleState;
    }

    public bool IsInDeadState()
    {
        return currentState is DeadsState;
    }
}
