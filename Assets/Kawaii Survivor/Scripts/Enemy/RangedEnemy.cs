using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement),typeof(RangedEnemyAttack))]
public class RangedEnemy : MonoBehaviour
{
    private EnemyMovement enemyMovement;
    private RangedEnemyAttack rangedEnemyAttack;
    private Player player;

    [Header("Objects")]
    [SerializeField] private ParticleSystem passAwayParticles;
    [SerializeField] private SpriteRenderer enemyRender;
    [SerializeField] private SpriteRenderer enemySpawnRender;
    [SerializeField] public static Action<float, Vector2> onDamageTaken;
    [SerializeField] private Collider2D enemyCollider;
    [Header("Attack")]
    [SerializeField] private float EnemyDetection = 1f;
    [Header("Health")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private TMP_Text healthText;
    [Header("Spawn Related")]
    [SerializeField] private float scaleRateChangeSpeed = 0.3f;
    [SerializeField] private float localScaleRate = 0.3f;
    [SerializeField] private int loops = 4;
    [Header("Dubug")]
    [SerializeField] private bool isPlayerDetected = false;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthText.text = health.ToString();
        enemyMovement = GetComponent<EnemyMovement>();
        rangedEnemyAttack = GetComponent<RangedEnemyAttack>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        rangedEnemyAttack.storePlayer(player);

        if (player == null)
        {
            Debug.LogError("Player GameObject with tag 'Player' not found in the scene.");
        }

        StartSpawnSequence();

    }

    // Update is called once per frame
    void Update()
    {
        if (!enemyRender.enabled)
            return;
        ManageAttack();
    }

    private void ManageAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer > EnemyDetection)
            enemyMovement.FollowPlayer();
        else
            TryAttack(); 
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

    private void TryAttack()
    {
        rangedEnemyAttack.AutoAim();
    }

    public void ToTakeDamage(float damage)
    {
        float realDamage = Mathf.Clamp(damage, 0f, health);
        health -= realDamage;
        healthText.text = health.ToString();

        onDamageTaken?.Invoke(damage, transform.position);

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
