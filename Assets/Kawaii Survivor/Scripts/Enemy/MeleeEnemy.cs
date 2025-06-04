using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(EnemyMovement))]
public class MeleeEnemy : Enemy
{

    [Header("Attack")]
    [SerializeField] private float damage ;
    [SerializeField] private float attackFrequency = 1f;
    private float attackDelay = 0f;
    private float attackTimer = 0f;
    protected override void Start()
    {
        base.Start();
        attackDelay = 1f / attackFrequency;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!CanAttack())
            return;
        if (attackTimer >= attackDelay)
            TryAttack();
        else
            WaitForAttack();

        enemyMovement.FollowPlayer();
    }

    private void WaitForAttack()
    {
        attackTimer += Time.deltaTime;
    }
    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= EnemyDetection)
            // ´¥·¢¹¥»÷Âß¼­
            Attack();
    }
    private void Attack()
    {
        attackTimer = 0;
        player.ToTakeDamage(damage);
    }

    private void OnDrawGizmos()
    {
        if (!isPlayerDetected)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, EnemyDetection);
    }
}
