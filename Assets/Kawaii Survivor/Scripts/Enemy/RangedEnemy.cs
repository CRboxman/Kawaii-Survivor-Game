using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement),typeof(RangedEnemyAttack))]
public class RangedEnemy : Enemy 
{
    private RangedEnemyAttack rangedEnemyAttack;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rangedEnemyAttack = GetComponent<RangedEnemyAttack>();
        rangedEnemyAttack.storePlayer(player);
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanAttack())
            return;
        ManageAttack();
        transform.localScale=player.transform.position.x > transform.position.x ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        healthText.transform.localScale = player.transform.position.x > transform.position.x ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
    }

    private void ManageAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer > EnemyDetection)
            enemyMovement.FollowPlayer();
        else
            TryAttack(); 
    }


    private void TryAttack()
    {
        rangedEnemyAttack.AutoAim();
    }

    private void OnDrawGizmos()
    {
        if (!isPlayerDetected)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, EnemyDetection);
    }
}
