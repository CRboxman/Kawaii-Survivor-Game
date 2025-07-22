using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class RangeWeapon : Weapon
{
    [Header("Objects")]
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private PlayerBullet playerBullet;
    [SerializeField] private Transform playerBulletPoolFather;
    private ObjectPool<PlayerBullet> playerBulletPool;
    // Start is called before the first frame update
    void Start()
    {
        playerBulletPool = new ObjectPool<PlayerBullet>(
        CreateBullet,
        ActionOnGet,
        ActionOnRelease,
        ActionOnDestroy
        );
    }
    // Update is called once per frame
    void Update()
    {
        AutoAim();
    }
    #region 玩家子弹池化
    private PlayerBullet CreateBullet()
    {
        PlayerBullet bulletInstance = Instantiate(playerBullet, shootingPoint.position, Quaternion.identity);
        bulletInstance.transform.SetParent(playerBulletPoolFather);
        bulletInstance.StorePlayerRangeWeapon(this);
        return bulletInstance;
    }
    private void ActionOnGet(PlayerBullet bullet)
    {
        bullet.Reload();
        bullet.transform.position = shootingPoint.position;
        bullet.gameObject.SetActive(true);
    }
    private void ActionOnRelease(PlayerBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
    private void ActionOnDestroy(PlayerBullet bullet)
    {
        Destroy(bullet.gameObject);
    }
    public void ReleaseBullet(PlayerBullet bullet)
    {
        playerBulletPool.Release(bullet);
    }
    #endregion
    #region 瞄准，管理射击时间，播放动画并实例化子弹
    private void AutoAim()
    {
        Enemy closestEnemy = GetClosestEnemy();
        Vector2 targetVector = Vector2.up;
        if (closestEnemy != null)
        {
            targetVector = (closestEnemy.transform.position - transform.position).normalized;
            ManageShooting();
            transform.up = targetVector;
            return;
        }
        transform.up = Vector2.Lerp(transform.up, targetVector, Time.deltaTime * aimLerp);
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
    private void Shoot()
    {
        animatior.Play("GunAttack");
        PlayerBullet bulletInstance = playerBulletPool.Get();
        bulletInstance.Shoot(damage, transform.up);
        animatior.speed = 1f / attackDelay;
    }
    #endregion

}
