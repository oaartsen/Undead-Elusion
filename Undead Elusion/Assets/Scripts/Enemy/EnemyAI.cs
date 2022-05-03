using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

    // Configuration parameters
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 12f;
    [SerializeField] float provokeRange = 8f;
    [SerializeField] float turnSpeed = 5f;

    // State variables
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    bool isProvokedByDamage = false;

    // Cached references
    NavMeshAgent myNavMeshAgent;
    Animator myAnimator;
    EnemyHealth myEnemyHealth;


    // Start is called before the first frame update
    void Start() {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        myAnimator = GetComponent<Animator>();
        myEnemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update() {

        if (myEnemyHealth.IsDead()) {
            enabled = false;
            myNavMeshAgent.enabled = false;
        }

        distanceToTarget = Vector3.Distance(target.position, transform.position);
        HandleProvoked();

        if (isProvoked || isProvokedByDamage) {
            isProvoked = true;
            isProvokedByDamage = false;
            EngageTarget();
        }
        else if (myNavMeshAgent.remainingDistance < myNavMeshAgent.stoppingDistance) {
            myAnimator.SetTrigger("startIdle");
        }

    }

    public void OnDamageTaken() {
        isProvokedByDamage = true;
    }

    void HandleProvoked() {
        if (distanceToTarget > chaseRange) {
            isProvoked = false;
        }

        else if (distanceToTarget <= provokeRange) {
            isProvoked = true;
        }
    }

    void EngageTarget() {
        FaceTarget();

        if (distanceToTarget > myNavMeshAgent.stoppingDistance) {
            ChaseTarget();
        }

        else {
            AttackTarget();
        }
    }

    void ChaseTarget() {
        myAnimator.SetBool("isAttacking", false);
        myAnimator.SetTrigger("startRunning");
        myNavMeshAgent.SetDestination(target.position);
    }

    void AttackTarget() {
        myAnimator.SetBool("isAttacking", true);
    }

    void FaceTarget() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }


    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, provokeRange);
    }
}
