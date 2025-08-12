using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IPlayerStatesDependency
{
    [Header("Objects")]
    [SerializeField] protected Animator animatior;
    [field: SerializeField] public WeaponDataSO weaponData { get; private set; }
    [field: SerializeField] public int weaponLevel { get; private set; }
    [Header("Settings")]
    [SerializeField] protected float range;
    [SerializeField] protected LayerMask enemyLayerMask;
    [SerializeField] protected float aimLerp;
    [Header("Attack")]
    [SerializeField] protected float damage;
    [SerializeField] protected float attackDelay;
    [SerializeField] protected float attackTimer;
    [SerializeField][Range(0, 100)] protected float criticalChance;
    [SerializeField][Range(0,10)] protected float criticalPercent;
    [Header("Debug")]
    [SerializeField] protected bool detectGizmos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    protected Enemy GetClosestEnemy()
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
    //得到是否有暴击的伤害值
    protected float GetDamage(out bool isCriticalHit)
    {
        isCriticalHit = false;
        if (UnityEngine.Random.Range(0, 101) <= criticalChance)
        {
            isCriticalHit = true;
            return damage * criticalPercent;
        }
        return damage;
    }
    public abstract void UpdateStats(PlayerStateManager playerStateManager);
    /// <summary>
    /// 初始化武器的初始属性
    /// </summary>
    protected void ConfigureStats()
    {
        float multiplier = 1 + weaponLevel / 3;
        damage = weaponData.GetStateValue(PlayerState.Attack) * multiplier;
        attackDelay = (1 / (weaponData.GetStateValue(PlayerState.AttackSpeed)) )/multiplier;
        criticalChance = weaponData.GetStateValue(PlayerState.CriticalChance) /100* multiplier;
        criticalPercent = weaponData.GetStateValue(PlayerState.CriticalPercent)/100 * multiplier;
        if (weaponData.weaponPref.GetType()==typeof(RangeWeapon))
        {
            range = weaponData.GetStateValue(PlayerState.Range) * multiplier;
        }
    }
    public void UpgrateTo(int targetLevel)
    {
        weaponLevel = targetLevel;
        ConfigureStats();
    }
    private void OnDrawGizmos()
    {
        if (detectGizmos)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }


}
