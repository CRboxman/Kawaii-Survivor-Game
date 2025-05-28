using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{

    [Header("Objects")]
    [SerializeField] private DamageText damageTextPrefab ;
    private void Awake()
    {
        Enemy.onDamageTaken += InstantiateDamageText;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        Enemy.onDamageTaken -= InstantiateDamageText;
    }
    private void InstantiateDamageText(float damage,Vector2 enemyPos)
    {
        Vector3 spawnPosition = enemyPos;
        DamageText damageTextInstance = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, transform);
        damageTextInstance.PlayAnimate(damage);
    }
}
