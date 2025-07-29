using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;
public class DropManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Candy candyPrefrab;
    [SerializeField] private Cash cashPrefrab;
    [SerializeField] private Treasure treasurePrefrab;
    [SerializeField] private Transform candyParent;
    [SerializeField] private Transform cashParent;
    [SerializeField]private Vector2 offset_1 = new Vector2(0, 0.5f);
    [SerializeField]private Vector2 offset_2 = new Vector2(0, 0.5f);
    [Header("Settings")]
    [SerializeField][Range(0,100)]private float spawnCashProbability = 0.9f; //现金掉落概率
    [SerializeField][Range(0, 100)] private float spawnTreasureProbability = 0.4f; //宝箱掉落概率
    private ObjectPool<Candy> candyPool;
    private ObjectPool<Cash> cashPool;
    private void Awake()
    {
        Enemy.onPassAway += EnemyPassAwayCallBack;
        Candy.onCollected += ReleaseCandy; // 订阅糖果收集事件
        Cash.onCollected += ReleaseCash; // 订阅现金收集事件
    }
    // Start is called before the first frame update
    void Start()
    {
        candyPool = new ObjectPool<Candy>(
             CandyCreateFunction,
             CandyActionOnGet,
             CandyActionOnRelease,
             CandyActionOnDestroy
        );
        cashPool = new ObjectPool<Cash>(
            CashCreateFunction,
            CashActionOnGet,
            CashActionOnRelease,
            CashActionOnDestroy
        );
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDestroy()
    {
        Enemy.onPassAway -= EnemyPassAwayCallBack;
        Candy.onCollected -= ReleaseCandy; // 订阅糖果收集事件
        Cash.onCollected -= ReleaseCash; // 订阅现金收集事件
    }

    #region     糖果池化
    private Candy CandyCreateFunction()                        => Instantiate(candyPrefrab,candyParent);
    private void CandyActionOnGet(Candy candy)          => candy.gameObject.SetActive(true);
    private void CandyActionOnRelease(Candy candy)    => candy.gameObject.SetActive(false);
    private void CandyActionOnDestroy(Candy candy)   => Destroy(candy.gameObject);

    #endregion
    #region     金钱池化
    private Cash CashCreateFunction()                            => Instantiate(cashPrefrab, cashParent);
    private void CashActionOnGet(Cash cash)                => cash.gameObject.SetActive(true);
    private void CashActionOnRelease(Cash cash)          => cash.gameObject.SetActive(false);
    private void CashActionOnDestroy(Cash cash)         => Destroy(cash.gameObject);

    #endregion
    public void ReleaseCandy(Candy candy)    =>candyPool.Release(candy);
    public void ReleaseCash(Cash cash)          => cashPool.Release(cash);
    /// <summary>
    /// 敌人死亡时的回调函数，负责生成糖果和现金掉落物，并播放动画。
    /// </summary>
    /// <param name="enemyPosition"></param>
    private void EnemyPassAwayCallBack(Vector2 enemyPosition)
    {
        //判断金币掉落的概率，并生成现金掉落物
        bool shouleSpawnCash = Random.Range(0, 101) <= spawnCashProbability;
        if (shouleSpawnCash)
        {
            Cash cashInstance = cashPool.Get();
            cashInstance.transform.position = enemyPosition + offset_1;
            cashInstance.animator.Play("fall_Anim");
            cashInstance.transform.SetParent(cashParent);
        }
        //生成糖果掉落物
        Candy candyInstanse = candyPool.Get();
        candyInstanse.transform.position = enemyPosition ;
        candyInstanse.animator.Play("fall_Anim");
        candyInstanse.transform.SetParent(candyParent);
        //生成宝箱
        DropTreasure(enemyPosition);
    }

    private void DropTreasure(Vector2 enemyPosition)
    {
        //生成宝箱
        bool shouleSpawnTreasure = Random.Range(0, 101) <= spawnTreasureProbability;
        if (shouleSpawnTreasure)
        {
            Treasure treasureInstanse=Instantiate(treasurePrefrab,transform);
            treasureInstanse.transform.position = enemyPosition + offset_1+offset_2;
            treasureInstanse.treasureAnimator.Play("fall_Anim");
        }
    }
}
