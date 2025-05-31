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
    [SerializeField]private  BoxCollider2D hitDetectionBoxCollider;
    [Header("Settings")]
    [SerializeField] private float range;
    [SerializeField] private float hitRange;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private float aimLerp;
    [Header("Attack")]
    [SerializeField]private float damage;
    [SerializeField] private float attackDelay;
    [SerializeField]private float attackTimer;
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
        // 根据 attackDelay（攻击间隔）调整动画速度，使动画播放时长等于一次攻击的间隔时间。
        // 攻击越频繁，动画播放越快；保证视觉效果和逻辑攻击同步。
        animatior.speed = 1f / attackDelay;
    }
    private void Attacking()
    {
        Attack();
    }
    private void StopAttack()
    {
        state = State.Idle;
        damagedEnemies.Clear();
        animatior.speed = 1f; // 恢复动画速度为默认值
    }
    private void Attack()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapBoxAll(hitDetectionArea.position, 
                                                                                                    hitDetectionBoxCollider.bounds.size,
                                                                                                    hitDetectionArea.localEulerAngles.z,
                                                                                                    enemyLayerMask);
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
            transform.up=targetVector;
            ManageAttack();
        }
        transform.up = Vector2.Lerp(transform.up, targetVector, Time.deltaTime * aimLerp);
        WaitForAttack();
    }

    private void ManageAttack()
    {
        if (attackTimer >= attackDelay)
        {
            attackTimer = 0;
            StartAttack();
        }
    }
    private void WaitForAttack()
    {
        attackTimer += Time.deltaTime;
    }

    private Enemy GetClosestEnemy()
    {
        Enemy closestEnemy = null;
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(transform.position, range, enemyLayerMask);
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
