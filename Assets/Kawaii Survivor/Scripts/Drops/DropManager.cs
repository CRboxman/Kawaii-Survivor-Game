using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class DropManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Candy candyPrefrab;
    [SerializeField] private Cash cashPrefrab;
    [SerializeField] private Transform candyParent;
    [SerializeField] private Transform cashParent;
    private Vector2 offset = new Vector2(0, 0.5f);
    private void Awake()
    {
        Enemy.onPassAway += EnemyPassAwayCallBack;
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
        Enemy.onPassAway -= EnemyPassAwayCallBack;
    }

    private void EnemyPassAwayCallBack(Vector2 enemyPosition)
    {
        bool shouleSpawnCash = Random.Range(0, 101) <= 90;
        if (shouleSpawnCash) 
        {
            Cash cashInstance=Instantiate(cashPrefrab, enemyPosition+offset, Quaternion.identity);
            cashInstance.cashAnimator.Play("fall_Anim");
            cashInstance.transform.SetParent(cashParent);
        }


        Candy candyInstanse=Instantiate(candyPrefrab,enemyPosition,Quaternion.identity);
        candyInstanse.candyAnimator.Play("fall_Anim");
        candyInstanse.transform.SetParent(candyParent);
    }
}
