using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private enum State
    {
        Idle,
        Attack
    }
    private State state;

    [Header("Objects")]
    [SerializeField] private Transform hitDetectionArea;
    [SerializeField] private Animator animatior;
    private List<Enemy> damagedEnemies = new List<Enemy>();
    [Header("Settings")]
    [SerializeField] private float range;
    [SerializeField] private float hitRange;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float aimLerp;
    [Header("Attack")]
    [SerializeField]private float damage;
    [Header("Debug")]
    [SerializeField]private bool detectGizmos;
    [SerializeField]private bool attackDetectGizmos;
    


    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                AutoAim();
                break;
            case State.Attack:
                Attacking();
                break;
        }
    }
    [NaughtyAttributes.Button]
    private void StartAttack()
    {
        animatior.Play("Attack");
        state = State.Attack;

        damagedEnemies.Clear();
    }
    private void Attacking()
    {
        Attack();
    }
    private void StopAttack()
    {
        state = State.Idle;
        damagedEnemies.Clear();
    }
    private void Attack()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(hitDetectionArea.position, hitRange, layerMask);
        for (int i = 0; i < enemyColliders.Length; i++)
        {
            Enemy enemy = enemyColliders[i].GetComponent<Enemy>();
            if (!damagedEnemies.Contains(enemy))
            {
                enemy.ToTakeDamage(damage);
                damagedEnemies.Add(enemy);
            }

        }
    }
    private void AutoAim()
    {
        Enemy closestEnemy = GetClosestEnemy();
        Vector2 targetVector =Vector2.up ;
        if (closestEnemy != null)
        {
            targetVector = (closestEnemy.transform.position - transform.position).normalized;
        }
        transform.up = Vector2.Lerp(transform.up, targetVector, Time.deltaTime * aimLerp);
    }
    private Enemy GetClosestEnemy()
    {
        Enemy closestEnemy = null;
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(transform.position, range, layerMask);
        if (enemyColliders.Length <= 0)
            return null;
        float minDistance = range;
        for (int i = 0; i < enemyColliders.Length; i++)
        {
            Enemy enemyChecked = enemyColliders[i].GetComponent<Enemy>();
            if (enemyChecked == null)
                continue;
            float distance = Vector2.Distance(transform.position, enemyChecked.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemyChecked;
            }
        }
            return closestEnemy;
    }
    private void OnDrawGizmos()
    {
        if (detectGizmos)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, range);
        }
        if(attackDetectGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(hitDetectionArea.position, hitRange);
        }
    }
}
