using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyAI {

    public int nextWayPoint = 0;

    EnemyStates enemy;

    public PatrolState (EnemyStates enemy)
    {
        this.enemy = enemy;
    }
    public void UpdateActions()
    {
        Watch();
        Patrol();
    }

    void Watch()
    {
        RaycastHit hit;
        if(Physics.Raycast(enemy.transform.position, -enemy.transform.forward, out hit, enemy.patrolRange))
        {
            if(hit.collider.CompareTag("Player"))
            
                enemy.chaseTarget = hit.transform;
                Debug.Log("Player Colide");
                // enemy.chaseTarget = hit.transform;
                ToChaseState();
            
        }
    }

    void Patrol()
    {
        Debug.Log("patrol");
        enemy.navMeshAgent.destination = enemy.waypoints[nextWayPoint].position;
        enemy.navMeshAgent.Resume();
        if(enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
        {
            nextWayPoint = (nextWayPoint + 1) % enemy.waypoints.Length ;
        }
    }

    public void onTriggerEnter(Collider enemy)
    {
        if (enemy.gameObject.CompareTag("Player"))
        {
            ToAlertState();
        }
            
       
    }

    public void ToPatrolState()
    {
        enemy.currentState = enemy.patrolState;
    }

    public void ToAttackState()
    {
        Debug.Log("Attack State");
        enemy.currentState = enemy.attackState;
    }

    public void ToAlertState()
    {
        Debug.Log("Alert State");
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        Debug.Log("Chase State");
        enemy.currentState = enemy.chaseState;
    }
}
