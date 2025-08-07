using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Character Data",menuName = "ScriptableObject/new Character Data")]
public class CharacterSO : ScriptableObject
{
    [field: SerializeField]public string CharacterName { get; private set; }
    [field: SerializeField]public Sprite CharacterSprite { get; private set; }
    [field: SerializeField]public int PurchasePrice { get; private set; }
    [HorizontalLine]
    [SerializeField] private float attack;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float criticalChance;
    [SerializeField] private float criticalPercent;
    [SerializeField]private float moveSpeed;
    [SerializeField] private float maxHealth;
    [SerializeField] private float range;
    [SerializeField] private float healthRecoverySpeed;
    [SerializeField] private float armor;
    [SerializeField] private float luck;
    [SerializeField] private float dodge;
    [SerializeField] private float lifeSteal;

    public Dictionary<PlayerState,float> BaseStats
    {
        get
        {
            return new Dictionary<PlayerState, float>
            {
                { PlayerState.Attack, attack },
                { PlayerState.AttackSpeed, attackSpeed },
                { PlayerState.CriticalChance, criticalChance },
                { PlayerState.CriticalPercent, criticalPercent },
                { PlayerState.MoveSpeed, moveSpeed },
                { PlayerState.MaxHealth, maxHealth },
                { PlayerState.Range, range },
                { PlayerState.HealthRecoverySpeed, healthRecoverySpeed },
                { PlayerState.Armor, armor },
                { PlayerState.Luck, luck },
                { PlayerState.Dodge, dodge },
                { PlayerState.LifeSteal, lifeSteal }
            };
        }
    }
}
