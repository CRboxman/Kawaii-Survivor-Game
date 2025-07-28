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
    [SerializeField] private Transform candyParent;
    [SerializeField] private Transform cashParent;
    private ObjectPool<Candy> candyPool;
    private ObjectPool<Cash> cashPool;
    private Vector2 offset = new Vector2(0, 0.5f);
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
        bool shouleSpawnCash = Random.Range(0, 101) <= 90;
        if (shouleSpawnCash)
        {
            Cash cashInstance = cashPool.Get();
            cashInstance.transform.position = enemyPosition + (Vector2)offset;
            cashInstance.animator.Play("fall_Anim");
            cashInstance.transform.SetParent(cashParent);
        }


        Candy candyInstanse = candyPool.Get();
        candyInstanse.transform.position = enemyPosition ;
        candyInstanse.animator.Play("fall_Anim");
        candyInstanse.transform.SetParent(candyParent);
    }
}
