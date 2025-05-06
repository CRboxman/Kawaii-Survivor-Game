using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    private EnemyMovement enemyMovement;
    private Player player;

    [Header("Objects")]
    [SerializeField] private ParticleSystem passAwayParticles;
    [SerializeField] private SpriteRenderer enemyRender;
    [SerializeField] private SpriteRenderer enemySpawnRender;
    [Header("Attack")]
    [SerializeField] private float damage = 10.0f;
    [SerializeField] private float attackFrequency = 1f;
    [SerializeField] private float EnemyDetection = 1f;
    [Header("Health")]
    [SerializeField] private float maxHealth;
    [SerializeField]private float health;
    [SerializeField]private TMP_Text healthText;
    [Header("Spawn Related")]
    [SerializeField] private float scaleRateChangeSpeed = 0.3f;
    [SerializeField] private float localScaleRate = 0.3f;
    [SerializeField] private int loops=4;

    [Header("Dubug")]
    [SerializeField] private bool isPlayerDetected = false;


    private float attackDelay = 0f;
    private float attackTimer = 0f;
    

    void Start()
    {
        health = maxHealth;
        healthText.text=health.ToString();
        enemyMovement = GetComponent<EnemyMovement>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("Player GameObject with tag 'Player' not found in the scene.");
        }
        StartSpawnSequence();
        attackDelay = 1f / attackFrequency;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer >= attackDelay)
            TryAttack();
        else
            WaitForAttack();
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

        enemyMovement.StorePlayer(player);
    }
    private void WaitForAttack()
    {
        attackTimer += Time.deltaTime;
    }
    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= EnemyDetection)
            // 触发攻击逻辑
            Attack();
    }
    private void Attack()
    {
        attackTimer = 0;
        player.ToTakeDamage(damage);
    }
    public void ToTakeDamage(float Damage)
    {
        float realDamage = Mathf.Clamp(Damage, 0f, health);
        health -= realDamage;
        healthText.text = health.ToString();
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
    private void OnDrawGizmos()
    {
        if (!isPlayerDetected)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, EnemyDetection);
    }
}
