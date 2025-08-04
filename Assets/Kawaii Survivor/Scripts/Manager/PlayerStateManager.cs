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
    [Header("Settings")]
    private Dictionary<PlayerState,float> playerStates = new Dictionary<PlayerState, float>();
    // Start is called before the first frame update
    void Start()
    {
        playerStates.Add(PlayerState.MaxHealth, 50f);
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
        if (playerStates.ContainsKey(state))
        {
            playerStates[state] += value;
        }
        else
        {
            // 不存在就添加新的键值对
            playerStates[state] = value;
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
        return playerStates[nowState];
    }
}


