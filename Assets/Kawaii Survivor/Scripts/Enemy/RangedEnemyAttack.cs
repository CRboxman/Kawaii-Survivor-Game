using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class RangedEnemyAttack : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private EnemyBullet bulletPrefab;
     private Transform enemyBulletPoolFather;
    private Player player;
    private ObjectPool<EnemyBullet> bulletPool; 
    [Header("Settings")]
    [SerializeField] private float damage;
    [SerializeField] private float attackFrequency = 1f;
    private float attackDelay = 0f;
    private float attackTimer = 0f;

    [Header("Dubug")]
    [SerializeField] private bool isPlayerDetected = false;
    private Vector2 gizmosDirection;
 

    // Start is called before the first frame update
    void Start()
    {
        attackDelay = 1f / attackFrequency;
        attackTimer = attackDelay;
        bulletPool = new ObjectPool<EnemyBullet>(
            CreateBullet,
            ActionOnGet,
            ActionOnRelease,
            ActionOnDestroy
        );
        enemyBulletPoolFather = GameObject.Find("EnemyBulletPool").transform;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    #region 远程敌人射击子弹池化
    private EnemyBullet CreateBullet()
    {
        EnemyBullet bulletInstance = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        bulletInstance.transform.SetParent(enemyBulletPoolFather);
        bulletInstance.StoreRangerEnemyAttack(this);
        return bulletInstance;
    }
    private void ActionOnGet(EnemyBullet bullet)
    {
        bullet.Reload();
        bullet.transform.position = shootingPoint.position;
        bullet.gameObject.SetActive(true);
        bullet.StartLifetimeTimer();
    }
    private void ActionOnRelease(EnemyBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
    private void ActionOnDestroy(EnemyBullet bullet)
    {
        Destroy(bullet.gameObject);
    }
    #endregion

    //
    public void storePlayer(Player player)
    {
        this.player = player;
    }

    #region 瞄准，管理射击时间，播放动画并实例化子弹
    public void AutoAim()
    {
        ManageShooting();
    }
    private void ManageShooting()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackDelay)
        {
            attackTimer = 0f;
            Shoot();
        }
    }
    //得到了玩家对象才能开始射击
    private void Shoot()
    {
        Vector2 direction = (player.GetCenter() - (Vector2)shootingPoint.position).normalized;
        gizmosDirection = direction;

        EnemyBullet bulletInstantce = bulletPool.Get();
        bulletInstantce.Shoot(damage, direction);
    }

    #endregion
    public void ReleaseBullet(EnemyBullet enemyBullet)
    {
        bulletPool.Release(enemyBullet);
    }
    private void OnDrawGizmos()
    {
        if (!isPlayerDetected)
            return;
        Gizmos.color = Color.white;
        Gizmos.DrawLine(shootingPoint.position,(Vector2) shootingPoint.position+ gizmosDirection*5);
    }
}
