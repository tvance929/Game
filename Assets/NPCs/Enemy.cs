﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    [SerializeField] [Tooltip("To visualise switch to Scene and select enemy(s)")]
    float attackRadius = 1f;
    [SerializeField]
    int damagePointsPerAttack = 1;
    [SerializeField]
    float secondsBetweenAttacks = 0.1f;


    Player player;
    NavMeshAgent navMeshAgent;
    bool isAttacking = false;

    // Use this for initialization
    void Start() {
        player = FindObjectOfType<Player>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {
        float distanceToPlayer = (player.transform.position - transform.position).magnitude;
        if (distanceToPlayer <= attackRadius)
        {
            navMeshAgent.SetDestination(player.transform.position);
            if (!isAttacking) {
                isAttacking = true;
                InvokeRepeating("DealPeriodicDamage", 0f, secondsBetweenAttacks);
            }
        }
        else
        {
            CancelInvoke();
            isAttacking = false;
        }
    }

    void DealPeriodicDamage()
    {
        player.DealDamage(damagePointsPerAttack);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}