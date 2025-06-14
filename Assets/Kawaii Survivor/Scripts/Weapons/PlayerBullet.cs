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
    [Header("Settings")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private LayerMask enemyMask;
    private float damage;
    // Start is called before the first frame update
    private void Awake()
    {
        //LeanTween.delayedCall(gameObject, 5, () => rangedEnemyAttack.ReleaseBullet(this));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Shoot(float damage, Vector2 direction)
    {
        this.damage = damage;
        transform.right = direction;
        rb.velocity = direction * bulletSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (IsInLayerMask(collider.gameObject.layer, enemyMask))
        {
            Attack(collider.GetComponent<Enemy>());
            Destroy(gameObject);
        }
    }

    private void Attack(Enemy enemy)
    {
        enemy.ToTakeDamage(damage);
    }

    private bool IsInLayerMask(int layer, LayerMask enemyMask)
    {
        return (enemyMask.value & (1 << layer)) != 0;
    }
}
