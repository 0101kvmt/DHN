using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IEnemyAI {

    EnemyStates enemy;

    public ChaseState (EnemyStates enemy)
    {
        this.enemy = enemy;
    }

    
    public void UpdateActions()
    {
        Watch();
        Chase();
    }

    void Watch()
    {
        RaycastHit hit;
        if(Physics.Raycast(enemy.transform.position, enemy.vision.forward, out hit, enemy.patrolRange, enemy.raycastMask) && hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;
            enemy.lastKnownPosition = hit.transform.position;
        } else
        {
            ToAlertState();
        }
    }

    void Chase()
    {
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        enemy.navMeshAgent.Resume();
        if(enemy.navMeshAgent.remainingDistance <= enemy.attackRange && enemy.onlyMelee == true)
        {
           // enemy.navMeshAgent.Stop();
            ToAttackState();
        } else if (enemy.navMeshAgent.remainingDistance <= enemy.shootRange && enemy.onlyMelee == false)
        {
           // enemy.navMeshAgent.Stop();
            ToAttackState();
        }
    }
    public void onTriggerEnter(Collider enemy)
    {

    }

    public void ToPatrolState()
    {

    }

    public void ToAttackState()
    {
        Debug.Log("attack state!");
        enemy.currentState = enemy.attackState;
    }

    public void ToAlertState()
    {
        Debug.Log("allert state!");
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        Debug.Log("chase state");
    }
}
