using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Transform hitDetectionArea;
    [Header("Settings")]
    [SerializeField] private float range;
    [SerializeField] private float hitRange;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float aimLerp;
    [Header("Attack")]
    [SerializeField]private float damage;
    [Header("Debug")]
    [SerializeField]private bool detectGizmos;
    [SerializeField]private bool attackDetectGizmos;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AutoAim();
        Attack();
    }

    private void Attack()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(hitDetectionArea.position, hitRange, layerMask);
        for (int i = 0; i < enemyColliders.Length; i++)
            enemyColliders[i].GetComponent<Enemy>().ToTakeDamage(damage);
    }

    private void AutoAim()
    {
        Enemy closestEnemy = GetClosestEnemy();
        Vector2 targetVector =Vector2.up ;
        if (closestEnemy != null)
        {
            targetVector = (closestEnemy.transform.position - transform.position).normalized;
        }
        transform.up = Vector2.Lerp(transform.up, targetVector, Time.deltaTime * aimLerp);
    }
    private Enemy GetClosestEnemy()
    {
        Enemy closestEnemy = null;
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(transform.position, range, layerMask);
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
