using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerBullet : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D BulletCollider;
    private RangeWeapon rangeWeapon;
    private Enemy targetEnemy;
    private bool isCriticalHit;
    [Header("Settings")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private LayerMask enemyMask;
    private float damage;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StorePlayerRangeWeapon(RangeWeapon rangeWeapon)
    {
        this.rangeWeapon = rangeWeapon;
    }
    public void Shoot(float damage, Vector2 direction,bool isCriticalHit)
    {
        Invoke("Release", 1);
        this.damage = damage;
        this.isCriticalHit = isCriticalHit;
        transform.right = direction;
        rb.velocity = direction * bulletSpeed;
    }
    public void Reload()
    {
        targetEnemy=null;

        rb.velocity = Vector2.zero;
        BulletCollider.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (targetEnemy != null)
            return;
        if (IsInLayerMask(collider.gameObject.layer, enemyMask))
        {
            targetEnemy = collider.GetComponent<Enemy>();
            CancelInvoke();
            Attack(targetEnemy);
            Release();
        }
    }
    private void Release()
    {
        if (!gameObject.activeSelf)
            return;
        rangeWeapon.ReleaseBullet(this);
    }
    private void Attack(Enemy enemy)
    {
        enemy.ToTakeDamage(damage, isCriticalHit);
    }

    private bool IsInLayerMask(int layer, LayerMask enemyMask)
    {
        return (enemyMask.value & (1 << layer)) != 0;
    }
}
