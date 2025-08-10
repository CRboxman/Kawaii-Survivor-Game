using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Data", menuName = "ScriptableObject/new Weapon Data")]
public class WeaponDataSO : ScriptableObject
{
    [field: SerializeField]public string CharacterName { get; private set; }
    [field: SerializeField]public Sprite CharacterSprite { get; private set; }
    [field: SerializeField]public int PurchasePrice { get; private set; }
    [field: SerializeField]public Weapon weaponPref { get; private set; }
    [HorizontalLine]
    [Header("具体数值")]
    [SerializeField][Range(0,500)] private float attack;
    [SerializeField] private float attackSpeed;
    [SerializeField][Range(0, 100)] private float criticalChance;
    [SerializeField][Range(0,100)] private float criticalPercent;
    [SerializeField] private float range;
    public Dictionary<PlayerState, float> BaseStats
    {
        get
        {
            return new Dictionary<PlayerState, float>
            {
                { PlayerState.Attack, attack },
                { PlayerState.AttackSpeed, attackSpeed },
                { PlayerState.CriticalChance, criticalChance },
                { PlayerState.CriticalPercent, criticalPercent },
                { PlayerState.Range, range }
            };
        }
    }
    public float  GetStateValue(PlayerState playerState)
    {
        foreach(KeyValuePair<PlayerState, float> kvp in BaseStats)
        {
            if (kvp.Key == playerState)
            {
                return kvp.Value;
            }
        }
        return 0;
    }
}
