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
    [Header("属性增益(%)，不是具体数值")]
    [SerializeField][Range(0, 500)][Tooltip("属性增益，不是具体数值")] private float attack;
    [SerializeField][Range(0, 5)][Tooltip("属性增益，不是具体数值")] private float attackSpeed;
    [SerializeField][Range(0, 500)][Tooltip("属性增益，不是具体数值")] private float moveSpeed;
    [Header("具体数值")]
    [SerializeField] [Tooltip("具体数值，需要调整basehealth，手动初始化的")]private float maxHealth;
    [SerializeField][Range(0, 20)][Tooltip("具体数值，自动初始化")] private float healthRecoverySpeed;
    [SerializeField][Range(0, 80)][Tooltip("具体数值，自动初始化")] private float armor;
    [SerializeField][Range(0, 100)][Tooltip("具体数值")] private float criticalChance;
    [SerializeField] [Range(150,500)][Tooltip("具体数值")] private float criticalPercent;
    [SerializeField][Range(0, 80)][Tooltip("具体数值")] private float dodge;
    [SerializeField] [Range(0, 100)][Tooltip("具体数值")] private float lifeSteal;
    [SerializeField][Tooltip("具体数值")] private float range;
    [SerializeField] private float luck;

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
