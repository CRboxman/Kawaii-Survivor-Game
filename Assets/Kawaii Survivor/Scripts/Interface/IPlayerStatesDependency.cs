using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// (带有此接口的脚本能被PlayerStateManager统一调用,用于实现玩家的属性升级)要求实现玩家状态的实际更新（数值或者ui）
/// </summary>
public interface IPlayerStatesDependency 
{
    /// <summary>
    ///  (被PlayerStateManager统一调用,用于实现玩家的属性升级)要求实现玩家状态的实际更新（数值或者ui）
    /// </summary>
    /// <param name="playerStateManager"></param>
    void UpdateStats(PlayerStateManager playerStateManager);
}
