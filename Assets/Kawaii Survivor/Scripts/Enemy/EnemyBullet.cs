using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(Collider2D))]
public class EnemyBullet : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private Collider2D BulletCollider;
        private RangedEnemyAttack rangedEnemyAttack;
    [Header("Settings")]
    [SerializeField]private float bulletSpeed;
    private float damage;

    // Start is called before the first frame update
    private void Awake()
    {
        LeanTween.delayedCall(gameObject, 5, () => rangedEnemyAttack.ReleaseBullet(this));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Shoot(float damage,Vector2 direction)
    {
        this.damage = damage;
        transform.right = direction;
        rb.velocity = direction * bulletSpeed; 
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Player player))
        {
            LeanTween.cancel(gameObject);

            player.ToTakeDamage(damage);
            this.BulletCollider.enabled = false;

            rangedEnemyAttack.ReleaseBullet(this);
        }
    }
    public void StartLifetimeTimer()
    {
        // 先取消之前的计时器（如果存在）
        LeanTween.cancel(gameObject);
        // 设置新的5秒计时器
        LeanTween.delayedCall(gameObject, 5, () => rangedEnemyAttack.ReleaseBullet(this));
    }
    public void StoreRangerEnemyAttack(RangedEnemyAttack rangedEnemyAttack)
    {
        this.rangedEnemyAttack = rangedEnemyAttack;
    }

    public void Reload()
    {
        rb.velocity = Vector2.zero;
        BulletCollider.enabled = true;
    }
}
