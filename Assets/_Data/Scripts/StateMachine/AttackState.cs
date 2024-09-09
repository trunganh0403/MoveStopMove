using UnityEngine;
using UnityEngine.AI;

public class AttackState : IState<Character>
{
    public void OnEnter(Character t)
    {
        t.animator.SetBool("IsAttack", true);
    }

    public void OnExecute(Character t)
    {
        if (t.objAttack != null && t.objAttack.isAttack && t.joystick != null)
        {
            t.ObjsAttack(t);

            if (t.joystick.Horizontal != 0 || t.joystick.Vertical != 0)
            {
                t.ChangeState(new RunningState());
                t.objAttack.isAttack = false;
            }
        }

        if (t.objAttack != null && t.objAttack.isAttack && t.navMeshAgent != null)
        {
            t.ObjsAttack(t);

            if (t.navMeshAgent.velocity.magnitude > 0.001f)
            {
                t.ChangeState(new RunningState());
                t.objAttack.isAttack = false;
            }
           
        }

    }

    public void OnExit(Character t)
    {
        t.animator.SetBool("IsAttack", false);
    }
}
