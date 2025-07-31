using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(WaveManagerUI))]
public class WaveManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float waveDuration = 5f;
    [Header("Objects")]
    [SerializeField] private Transform enemyParent;
    [SerializeField]private Player player;
    [Space()]
    [SerializeField] public Wave[] waves;
    private WaveManagerUI waveManagerUI;
    private float timer;// 计时器，用于记录当前波次的时间
    private int currentWaveIndex = 0;// 当前波次的编号
    private List<int> currentWaveEnemysSpawnEnemyCounter = new List<int>();
    private void Awake()
    {
        waveManagerUI = GetComponent<WaveManagerUI>();
    }
    void Start()
    {
        StartWave(currentWaveIndex);
    }

    void Update()
    {
        if (currentWaveIndex >= waves.Length)
            return;

        timer += Time.deltaTime;

        if (timer < waveDuration)
        {
            ManageCurrentWave();
            string timerString = ((int)(waveDuration - timer)).ToString();
            waveManagerUI.UpdateWaveTimeText(timerString);
        }
        else
        {
            EndWave(currentWaveIndex);
            currentWaveIndex++;

            if (currentWaveIndex < waves.Length)
            {
                StartWave(currentWaveIndex);
            }
        }
    }
    private void StartWave(int waveIndex)
    {
        timer = 0f;
        currentWaveEnemysSpawnEnemyCounter.Clear();

        waveManagerUI.UpdateWaveText($"Wave {waveIndex + 1}/{waves.Length} - {waves[waveIndex].name}");

        Wave wave = waves[waveIndex];
        for (int i = 0; i < wave.waveEnemys.Count; i++)
        {
            currentWaveEnemysSpawnEnemyCounter.Add(0);
        }

        Debug.Log($"[WaveManager] Wave {waveIndex} Started: {wave.name}");
    }

    private void EndWave(int waveIndex)
    {
        KillAllEnemys(enemyParent);
        waveManagerUI.UpdateWaveText($"Wave {waveIndex + 1}/{waves.Length} - {waves[waveIndex].name} Ended");
        Debug.Log($"[WaveManager] Wave(waveIndex): {waveIndex} Ended.");
    }
    private void ManageCurrentWave()
    {
        Wave currentWave = waves[currentWaveIndex];
        for (int i = 0; i < currentWave.waveEnemys.Count; i++)
        {
            WaveEnemy enemy = currentWave.waveEnemys[i];// 获取当前波次的当前这个敌人的信息
            float startTime = enemy.spawnTimeStartToEnd.x / 100 * waveDuration;// 将百分比转换为实际开始生成时的时间
            float endTime = enemy.spawnTimeStartToEnd.y / 100 * waveDuration;// 将百分比转换为实际结束生成时的时间
            // 如果当前计时器不在这个敌人生成的时间范围内，则跳过
            if (timer < startTime || timer > endTime)
                continue;
            //(当前敌人生成时的局部计时器)当前波的计时器减去开始生成时间，得到当前敌人已经开始开始生成时的时间
            float hasSpawnTime = timer - startTime;
            float spawnDelay = 1f / enemy.spawnFrequency;// 计算当前敌人生成的间隔时间，频率越高，攻击间隔越短
            int shouldSpawnCount = Mathf.FloorToInt(hasSpawnTime / spawnDelay);
            if (shouldSpawnCount > currentWaveEnemysSpawnEnemyCounter[i])
            {
                // 每次只生成一次
                Instantiate(enemy.enemyPrefab, GetSpawnPosition(), Quaternion.identity, enemyParent);
                currentWaveEnemysSpawnEnemyCounter[i]++;
            }
        }
    }
    private Vector2 GetSpawnPosition()
    {
        Vector2 direction = UnityEngine.Random.onUnitSphere;
        Vector2 offset = direction.normalized * UnityEngine.Random.Range(10, 17);
        Vector2 targetPosition=(Vector2)player.transform.position+offset;

        targetPosition.x = Mathf.Clamp(targetPosition.x, -28, 28);
        targetPosition.y=Mathf.Clamp(targetPosition.y, -16, 7);

        return targetPosition;
    }
    private void KillAllEnemys(Transform parentTransform)
    {
        while(parentTransform.childCount>0)
        {
            Transform child = parentTransform.GetChild(0);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
    }
}
[System.Serializable]
public struct Wave
{
    public string name;
    public List<WaveEnemy> waveEnemys;
}
[System.Serializable]
public struct WaveEnemy
{
    [MinMaxSlider(0, 100)] public Vector2 spawnTimeStartToEnd;
    public float spawnFrequency;
    public GameObject enemyPrefab;
}
