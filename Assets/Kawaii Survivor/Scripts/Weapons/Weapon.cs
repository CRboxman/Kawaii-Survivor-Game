using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] protected Animator animatior;
    [Header("Settings")]
    [SerializeField] protected float range;
    [SerializeField] protected LayerMask enemyLayerMask;
    [SerializeField] protected float aimLerp;
    [Header("Attack")]
    [SerializeField]protected float damage;
    [SerializeField] protected float attackDelay;
    [SerializeField]protected float attackTimer;
    [Header("Debug")]
    [SerializeField]protected bool detectGizmos;
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
