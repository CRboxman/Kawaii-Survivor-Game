using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float waveDuration = 5f;
    [Header("Objects")]
    [SerializeField] private Transform enemyParent;
    [SerializeField] public Wave[] waves;
    private float timer;
    private List<float> localTimeCounter = new List<float>();
    void Start()
    {
        Wave currentWave = waves[0]; // 目前只用第一个 wave
        for (int i = 0; i < currentWave.enemies.Count; i++)
        {
            localTimeCounter.Add(1f);
        }
    }

    void Update()
    {
        if (timer < waveDuration)
        {
            ManageCurrentWave();
        }
    }

    private void ManageCurrentWave()
    {
        Wave currentWave = waves[0];
        for (int i = 0; i < currentWave.enemies.Count; i++)
        {
            WaveEnemy enemy = currentWave.enemies[i];
            float startTime = enemy.spawnTimeStartToEnd.x / 100 * waveDuration;
            float endTime = enemy.spawnTimeStartToEnd.y / 100 * waveDuration;

            if (timer < startTime || timer > endTime)
                continue;

            float spawnTime = timer - startTime;
            float spawnDelay = 1f / enemy.spawnFrequency;

            if (spawnTime / spawnDelay > localTimeCounter[i])
            {
                Instantiate(enemy.enemyPrefab, Vector3.zero, Quaternion.identity, enemyParent);
                localTimeCounter[i]++;
            }
        }
        timer += Time.deltaTime; 
    }
}
    [System.Serializable]
public struct Wave
{
    public string name;
    public List<WaveEnemy> enemies;
}
[System.Serializable]
public struct WaveEnemy
{
    [MinMaxSlider(0, 100)] public Vector2 spawnTimeStartToEnd;
    public float spawnFrequency;
    public GameObject enemyPrefab;
}
