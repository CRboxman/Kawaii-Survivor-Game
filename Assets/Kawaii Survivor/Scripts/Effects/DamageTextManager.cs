using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DamageTextManager : MonoBehaviour
{

    [Header("Objects")]
    [SerializeField] private DamageText damageTextPrefab;
    private ObjectPool<DamageText> damageTextPool;

    private void Awake()
    {
        Enemy.onDamageTaken += EnemyHitCallBack;
        PlayerHealth.OnAttackDodge += PlayerDodgeCallBack;
    }
    void Start()
    {
        //初始化伤害文本对象池
        damageTextPool = new ObjectPool<DamageText>(
            CreateFunction,
            ActionOnGet,
            ActionOnRelease,
            ActionOnDestroy
        );
    }
             #region 池化伤害文本
    private DamageText CreateFunction()
    {
        return Instantiate(damageTextPrefab, transform);
    }
    private void ActionOnGet(DamageText text)
    {
        text.gameObject.SetActive(true);
    }
    private void ActionOnRelease(DamageText text)
    {
        text.gameObject.SetActive(false);
    }
    private void ActionOnDestroy(DamageText text)
    {
        Destroy(text.gameObject);
    }
    #endregion

    private void OnDestroy()
    {
        Enemy.onDamageTaken -= EnemyHitCallBack;
        PlayerHealth.OnAttackDodge -= PlayerDodgeCallBack;

    }
    /// <summary>
    /// 敌人受到伤害回调函数，进行伤害文本的实例化和播放动画
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="enemyPos"></param>
    private void EnemyHitCallBack(float damage, Vector2 enemyPos, bool isCriticalHit)
    {
        DamageText damageTextInstance = damageTextPool.Get();
        Vector3 spawnPosition = enemyPos;

        spawnPosition.x += UnityEngine.Random.Range(0, 0.5f);
        spawnPosition.y += UnityEngine.Random.Range(0, 0.5f);

        //spawnPosition = Camera.main.WorldToScreenPoint(spawnPosition);
        damageTextInstance.transform.position = spawnPosition;
        damageTextInstance.PlayAnimate(damage, isCriticalHit);
        //1秒之后释放（释放即返回到池中，失活那个对象）
        LeanTween.delayedCall(1, () => damageTextPool.Release(damageTextInstance));
    }
    private void PlayerDodgeCallBack(Vector2 playerPos)
    {
        DamageText damageTextInstance = damageTextPool.Get();
        Vector3 spawnPosition = playerPos;
        //spawnPosition = Camera.main.WorldToScreenPoint(spawnPosition);
        damageTextInstance.transform.position = spawnPosition;
        damageTextInstance.PlayAnimate("Dodge!");
        //1秒之后释放（释放即返回到池中，失活那个对象）
        LeanTween.delayedCall(1, () => damageTextPool.Release(damageTextInstance));
    }

}
