using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DamageTextManager : MonoBehaviour
{

    [Header("Objects")]
    [SerializeField] private DamageText damageTextPrefab ;
    private ObjectPool<DamageText> damageTextPool;
    private void Awake()
    {
        Enemy.onDamageTaken += EnemyHitCallBack;
    }

    // Start is called before the first frame update
    void Start()
    {
        damageTextPool = new ObjectPool<DamageText>(
            CreateFunction,
            ActionOnGet,
            ActionOnRelease,
            ActionOnDestroy
        );
    }
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
    private void OnDestroy()
    {
        Enemy.onDamageTaken -= EnemyHitCallBack;
    }
    private void EnemyHitCallBack(float damage,Vector2 enemyPos)
    {
        DamageText damageTextInstance =damageTextPool.Get();
        Vector3 spawnPosition = enemyPos;

        //spawnPosition = Camera.main.WorldToScreenPoint(spawnPosition);
        damageTextInstance.transform.position = spawnPosition;
        damageTextInstance.PlayAnimate(damage);
        //1秒之后释放（释放即返回到池中，失活那个对象）
        LeanTween.delayedCall(1, () => damageTextPool.Release(damageTextInstance));
    }
}
