using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStates : MonoBehaviour {

    public Transform[] waypoints;
    public int patrolRange;
    public int attackRange;
    public int shootRange;
    public Transform vision;
    public float stayAlertTime;

    public GameObject missle;
    public float missleDamage;
    public float missleSpeed;

    public bool onlyMelee = false;

    public float meleeDamage;
    public float attackDelay;

    public LayerMask raycastMask;

    [HideInInspector]
    public AlertState alertState;
    [HideInInspector]
    public AttackState attackState;
    [HideInInspector]
    public ChaseState chaseState;
    [HideInInspector]
    public PatrolState patrolState;
    [HideInInspector]
    public IEnemyAI currentState;
    [HideInInspector]
    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    [HideInInspector]
    public Transform chaseTarget;
    [HideInInspector]
    public Vector3 lastKnownPosition;
    void Awake()
    {
        alertState = new AlertState(this);
        attackState = new AttackState(this);
        chaseState = new ChaseState(this);
        patrolState = new PatrolState(this);
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
	// Use this for initialization
	void Start () {
        currentState = patrolState;
	}
	
	// Update is called once per frame
	void Update () {
        currentState.UpdateActions();
	}

    void OnTriggerEnter(Collider otherObj)
    {
        currentState.onTriggerEnter(otherObj);
    }
    
    void HiddenShot(Vector3 shotPosition)
    {
        Debug.Log("Hidden Shot");
        lastKnownPosition = shotPosition;
        currentState = alertState;
    }
}
