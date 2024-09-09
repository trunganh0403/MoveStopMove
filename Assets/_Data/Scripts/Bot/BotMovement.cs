using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotMovement : MonoBehaviour
{
    [SerializeField] NavMeshAgent navMeshAgent; 
    [SerializeField] ObjAttack objAttack;
    [SerializeField] Character character;

    [SerializeField] float patrolRadius = 10f;
    [SerializeField] float delayBeforeMove = 1f;


    void Start()
    {
        if (navMeshAgent == null)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        MoveToRandomPosition();
    }

    private void FixedUpdate()
    {
        this.Move();
    }

    protected virtual void Move()
    {
        if (character.IsInDeadState())
        {
            navMeshAgent.isStopped = true;
            return;
        }

        if (objAttack.IsWeaponThrown)
        {
            navMeshAgent.isStopped = true;
            return;
        }

        navMeshAgent.isStopped = false;

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            StartCoroutine(WaitAndMoveToRandomPosition());
        }
    }

    protected virtual IEnumerator WaitAndMoveToRandomPosition()
    {
 
        yield return new WaitForSeconds(delayBeforeMove);

        MoveToRandomPosition();
    }

    protected virtual void MoveToRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;
        NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, patrolRadius, 1);
        Vector3 finalPosition = hit.position;

        navMeshAgent.SetDestination(finalPosition);
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.parent.position, patrolRadius);
    }
}
