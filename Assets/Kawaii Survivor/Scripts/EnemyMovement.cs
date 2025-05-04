using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    [Header("Objects")]
    [SerializeField] private ParticleSystem passAwayParticles;
    [SerializeField] private SpriteRenderer enemyRender;
    [SerializeField] private SpriteRenderer enemySpawnRender;
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float EnemyDetection = 1f;
    [SerializeField] private float scaleRateChangeSpeed = 0.3f;
    [SerializeField] private float localScaleRate = 0.3f;


    [Header("Dubug")]
    [SerializeField] private bool isPlayerDetected = false;
    private bool hasSpawn;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player GameObject with tag 'Player' not found in the scene.");
        }
        enemyRender.enabled = false;
        enemySpawnRender.enabled = true;
        Vector3 scaleRate = enemySpawnRender.transform.localScale * localScaleRate;
        LeanTween.scale(enemySpawnRender.gameObject, scaleRate, scaleRateChangeSpeed)
                                                                     .setLoopPingPong(5)
                                                                     .setOnComplete(SpawnSequenceCompleted);



    }

    void Update()
    {
        if (!hasSpawn)
            return;
        FollowPlayer();
        TryAttack();
    }
    private void OnDrawGizmos()
    {
        if (!isPlayerDetected)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, EnemyDetection);
    }
    private void SpawnSequenceCompleted()
    {
        enemyRender.enabled = true;
        enemySpawnRender.enabled = false;
        hasSpawn = true;
    }
    private void FollowPlayer()
    {
        Vector3 dir = (player.transform.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }
    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= EnemyDetection)
            //销毁外加粒子效果触发
            PassAway();
    }
    private void PassAway()
    {
        // Unparent the particles & play them pass
        passAwayParticles.transform.SetParent(null);
        passAwayParticles.Play();
        Destroy(gameObject);
    }
}
