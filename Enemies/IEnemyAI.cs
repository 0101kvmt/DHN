using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyAI  {

    void UpdateActions();

    void onTriggerEnter(Collider enemy);

    void ToPatrolState();

    void ToAttackState();

    void ToAlertState();

    void ToChaseState();

}
