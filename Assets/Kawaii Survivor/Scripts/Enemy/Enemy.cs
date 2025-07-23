using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] protected ParticleSystem passAwayParticles;
    [SerializeField] protected SpriteRenderer enemyRender;
    [SerializeField] protected SpriteRenderer enemySpawnRender;
    [SerializeField] public static Action<float, Vector2,bool> onDamageTaken;
    [SerializeField] protected Collider2D enemyCollider;
    [SerializeField] protected TMP_Text healthText;
    protected EnemyMovement enemyMovement;
    protected Player player;
    [Header("Health")]
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float health;
    [Header("Spawn Related")]
    [SerializeField] protected float scaleRateChangeSpeed = 0.3f;
    [SerializeField] protected float localScaleRate = 0.3f;
    [SerializeField] protected int loops = 4;
    [Header("Attack")]
    [SerializeField] protected float EnemyDetection = 1f;
    [Header("Dubug")]
    [SerializeField] protected bool isPlayerDetected = false;

    //初始化血量，血量文本，获取敌人移动组件和玩家组件，开始生成逻辑
    protected virtual void Start()
    {
        health = maxHealth;
        healthText.text = health.ToString();
        enemyMovement = GetComponent<EnemyMovement>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("Player GameObject with tag 'Player' not found in the scene.");
        }
        StartSpawnSequence();
    }

    #region 通过判断渲染决定是否可以攻击，生成动画，提供玩家（用于在被继承的类中判断是否可以攻击以及追随），被造成伤害，死亡时的粒子效果
    protected bool CanAttack()
    {
        return enemyRender.enabled;
    }
    private void StartSpawnSequence()
    {
        enemyRender.enabled = false;
        enemySpawnRender.enabled = true;
        Vector3 scaleRate = enemySpawnRender.transform.localScale * localScaleRate;
        LeanTween.scale(enemySpawnRender.gameObject, scaleRate, scaleRateChangeSpeed)
                           .setLoopPingPong(loops)
                           .setOnComplete(SpawnSequenceCompleted);
    }
    private void SpawnSequenceCompleted()
    {
        enemyRender.enabled = true;
        enemySpawnRender.enabled = false;

        enemyCollider.enabled = true;

        enemyMovement.StorePlayer(player);
    }
    public void ToTakeDamage(float damage, bool isCriticalHit)
    {
        float realDamage = Mathf.Clamp(damage, 0f, health);
        health -= realDamage;
        healthText.text = health.ToString();

        onDamageTaken?.Invoke(damage, transform.position, isCriticalHit);

        if (health <= 0f)
        {
            health = 0f;
            PassAway();
        }
    }
    //销毁外加粒子效果触发
    private void PassAway()
    {
        // Unparent the particles & play them pass
        passAwayParticles.transform.SetParent(null);
        passAwayParticles.Play();
        Destroy(gameObject);
    }
    #endregion


    #region 测试用，显示攻击检测距离
    private void OnDrawGizmos()
    {
        if (!isPlayerDetected)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, EnemyDetection);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, EnemyDetection);
    }
    #endregion

}
