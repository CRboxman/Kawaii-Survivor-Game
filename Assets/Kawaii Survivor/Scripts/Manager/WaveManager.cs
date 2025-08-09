using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理波次的生成和敌人的生成，留有GameStateChangedCallBack，当触发游戏状态改变时，调用此方法来处理波次的开始和玩家的移动
/// </summary>
[RequireComponent(typeof(WaveManagerUI))]
public class WaveManager : MonoBehaviour, IGameStateListener
{
    [Header("Settings")]
    [SerializeField] private float waveDuration = 5f;
    [Header("Objects")]
    [SerializeField] private Transform enemyParent;
    [SerializeField] private Player player;
    [Space()]
    [SerializeField] public Wave[] waves;
    private WaveManagerUI waveManagerUI;
    private float timer = 0;// 计时器，用于记录当前波次的时间
    private int currentWaveIndex = 0;// 当前波次的编号
    private bool isWaveStarted = false;// 是否开始了当前波次
    private List<int> currentWaveEnemysSpawnEnemyCounter = new List<int>();
    private void Awake()
    {
        waveManagerUI = GetComponent<WaveManagerUI>();
    }
    void Start()
    {

    }

    void Update()
    {
        if(!isWaveStarted)
            return;

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
        isWaveStarted= true;
        //Debug.Log($"[WaveManager]Start!!!\n WaveStart(waveIndex):  {waveIndex} Started(Wave): {wave.name}");
    }

    private void EndWave(int waveIndex)
    {
        KillAllEnemys(enemyParent);
        waveManagerUI.UpdateWaveText($"Wave {waveIndex + 1}/{waves.Length} - {waves[waveIndex].name} Ended");
        //Debug.Log($"[WaveManager]END!!\n WaveStart(waveIndex): {waveIndex} Ended.");
        GameManager.instance.WaveCompletedCallBack();
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
        Vector2 targetPosition = (Vector2)player.transform.position + offset;

        targetPosition.x = Mathf.Clamp(targetPosition.x, -28, 28);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -16, 7);

        return targetPosition;
    }
    private void KillAllEnemys(Transform parentTransform)
    {
        while (parentTransform.childCount > 0)
        {
            Transform child = parentTransform.GetChild(0);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
    }

    public void GameStateChangedCallBack(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MENU:
                // 在菜单状态下，停止所有波次的计时器
                //timer = 0f;
                //currentWaveIndex = 0;
                //waveManagerUI.UpdateWaveText("Menu");
                //KillAllEnemys(enemyParent);
                player.CanMove(false);
                Debug.Log("在Menu状态下，等待点击按钮，来让状态为Game，StartWave才开始波次");
                break;
            case GameState.GAME:
                // 在游戏状态下，开始第一波
                player.CanMove(true);
                if (!isWaveStarted && currentWaveIndex < waves.Length)
                {
                    StartWave(currentWaveIndex);
                    Debug.Log("在Game状态下，开始波次（索引） " + currentWaveIndex);
                }
                break;
            case GameState.WAVETRANSITION:
                // 在波次状态下，继续当前波次
                isWaveStarted = false;
                player.CanMove(false);
                Debug.Log("在WaveTransition状态下，无法移动，波次停止，等待操作");
                break;
            case GameState.SHOP:
                // 在商店状态下，暂停当前波次
                isWaveStarted = false;
                 player.CanMove(false);
                Debug.Log(" 在Shop状态下，无法移动，波次停止，等待操作");
                break;
            case GameState.GAMEOVER:
                // 在游戏结束状态下，停止
                isWaveStarted = false;
                player.CanMove(false);
                Debug.Log("在GameOver状态下，无法移动，波次停止，等待操作，或者几秒后自动重新加载");
                break;
            case GameState.WEAPON_SELECT:
                // 在武器选择状态下，暂停当前波次
                isWaveStarted = false;
                player.CanMove(false);
                Debug.Log("在WeaponSelect状态下，无法移动，波次停止，等待操作");
                break;
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
    [MinMaxSlider(0, 100)] public Vector2 spawnTimeStartToEnd;// 以百分比表示的生成时间范围
    public float spawnFrequency;
    public GameObject enemyPrefab;
}
