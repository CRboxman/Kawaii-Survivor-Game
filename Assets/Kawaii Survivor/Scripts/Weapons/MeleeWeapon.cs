using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    private enum State
    {
        Idle,
        Attack
    }
    private State state;

    [Header("Objects")]
    private List<Enemy> damagedEnemies = new List<Enemy>();
    [SerializeField] private Transform hitDetectionArea;
    [SerializeField] private BoxCollider2D hitDetectionBoxCollider;

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
    private void AutoAim()
    {
        Enemy closestEnemy = GetClosestEnemy();
        Vector2 targetVector = Vector2.up;
        if (closestEnemy != null)
        {
            targetVector = (closestEnemy.transform.position - transform.position).normalized;
            transform.up = targetVector;
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
    //[NaughtyAttributes.Button],现在没必要用了，作为测试
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
                float damage = GetDamage(out bool isCriticalHit);

                enemy.ToTakeDamage(damage, isCriticalHit);
                damagedEnemies.Add(enemy);
            }

        }
    }
}
