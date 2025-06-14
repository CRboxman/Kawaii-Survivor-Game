using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon
{
    [Header("Objects")]
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private PlayerBullet playerBullet;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AutoAim();
    }
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
        PlayerBullet bulletInstance = Instantiate(playerBullet, shootingPoint.position, Quaternion.identity);
        bulletInstance.Shoot(damage, transform.up);
    }
}
