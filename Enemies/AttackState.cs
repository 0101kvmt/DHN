using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyAI {

    EnemyStates enemy;
    float timer;

    public AttackState (EnemyStates enemy)
    {
        this.enemy = enemy;
    }

    public void UpdateActions()
    {
        timer += Time.deltaTime;
        float distance = Vector3.Distance(enemy.chaseTarget.transform.position, enemy.transform.position);
        if(distance > enemy.attackRange && enemy.onlyMelee == true)
        {
            ToChaseState();
        }
        if(distance > enemy.shootRange && enemy.onlyMelee == false)
        {
            ToChaseState();
        }
        Watch();
        if(distance <= enemy.shootRange && distance > enemy.attackRange && enemy.onlyMelee == false && timer >= enemy.attackDelay)
        {
            Attack(true);
            timer = 0;
        }
        if(distance <= enemy.attackRange && timer >= enemy.attackDelay)
        {
            Attack(false);
            timer = 0;
        }
    }

    void Attack(bool shoot)
    {
        if (shoot == false)
        {
            enemy.chaseTarget.SendMessage("EnemyHit", enemy.meleeDamage, SendMessageOptions.DontRequireReceiver);
        }
        else if (shoot == true)
        {
            GameObject missle = GameObject.Instantiate(enemy.missle, enemy.transform.position, Quaternion.identity);
            missle.GetComponent<Missle>().speed = enemy.missleSpeed;
            missle.GetComponent<Missle>().damage = enemy.missleDamage;
        }
    }

    void Watch()
    {
        RaycastHit hit;
        if (Physics.Raycast(enemy.transform.position, enemy.vision.forward, out hit, enemy.patrolRange, enemy.raycastMask) &&
            hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;
            enemy.lastKnownPosition = hit.transform.position;
        }
        else
        {
            ToAlertState();
        }
    }

    public void onTriggerEnter(Collider enemy)
    {

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
        enemy.currentState = enemy.attackState;
    }

    public void ToChaseState()
    {
        Debug.Log("Chase State");
        enemy.currentState = enemy.chaseState;
    }
}
