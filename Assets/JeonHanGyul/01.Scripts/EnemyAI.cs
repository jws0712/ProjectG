using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Range(0, 50)][SerializeField] float attackRange = 5, sightRange = 20, attackDelay = 3;

    [Range(0, 20)][SerializeField] int power;
    


    private NavMeshAgent thisEnemy;
    private Transform playerPos;

    private bool isAttacking;

    private void Start()
    {
        thisEnemy = GetComponent<NavMeshAgent>();
        playerPos = FindObjectOfType<TestAttack>().transform;
    }

    private void Update()
    {
        float distanceFromPlayer = Vector3.Distance(playerPos.position, this.transform.position);

        if(distanceFromPlayer <= sightRange && distanceFromPlayer > attackRange && !TestAttack.isDead)
        {
            isAttacking = false;
            thisEnemy.isStopped = false;
            StopAllCoroutines();

            ChasePlayer();
        }

        if(distanceFromPlayer <= attackRange && !isAttacking && !TestAttack.isDead)
        {
            thisEnemy.isStopped = true;
            StartCoroutine(AttackPlayer());
        }

        if (TestAttack.isDead)
        {
            thisEnemy.isStopped = true;
        }
    }

    private void ChasePlayer()
    {
        thisEnemy.SetDestination(playerPos.position);
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;

        yield return new WaitForSeconds(attackDelay);

        FindObjectOfType<TestAttack>().TakeDamage(power);

        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(this.transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }
}
