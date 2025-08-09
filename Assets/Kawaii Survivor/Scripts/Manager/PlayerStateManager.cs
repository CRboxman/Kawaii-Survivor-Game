using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 管理玩家状态的脚本，负责统一触发所有实现了IPlayerStatesDependency接口的组件的UpdateStats方法来实现状态更新，外部可用AddPlayerState方法来添加或更新玩家状态值
/// </summary>
public class PlayerStateManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private CharacterSO playerData;
    [Header("Settings")]
    private Dictionary<PlayerState,float> playerAddedStates = new Dictionary<PlayerState, float>();
    private Dictionary<PlayerState,float> playerStates = new Dictionary<PlayerState, float>();
    private void Awake()
    {
        playerStates = playerData.BaseStats;

        foreach(KeyValuePair<PlayerState,float> kvp in playerStates)
        {
            playerAddedStates.Add(kvp.Key,0);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdatePlayerStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 主动触发一下UpdatePlayerStats方法，更新所有实现了IPlayerStatesDependency接口的组件
    /// </summary>
    /// <param name="state"></param>
    /// <param name="value"></param>
    public void AddPlayerState(PlayerState state, float value)
    {
        if (playerAddedStates.ContainsKey(state))
        {
            playerAddedStates[state] += value;
        }
        else
        {
            // 不存在就添加新的键值对
            playerAddedStates[state] = value;
        }
        UpdatePlayerStats();
    }
    /// <summary>
    /// 统一触发所有实现更新状态的IPlayerStatesDependency接口的UpdateStats方法
    /// </summary>
    public void UpdatePlayerStats()
    {
        IEnumerable<IPlayerStatesDependency> playerStatesDependency =
                    FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                    .OfType<IPlayerStatesDependency>();
        foreach (IPlayerStatesDependency playerStateslistener in playerStatesDependency)
        {
            playerStateslistener.UpdateStats(this);
        }
    }
    public float GetPlayerStateValue(PlayerState nowState)
    {
        return playerAddedStates[nowState];
    }
}


