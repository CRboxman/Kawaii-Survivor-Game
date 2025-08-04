using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// (回合结束或者被其他GameManager提供的方法主动调用时触发)这个接口用于监听游戏状态的变化：GameStateChangedCallBack
/// </summary>
public interface IGameStateListener
{
    void GameStateChangedCallBack(GameState gameState);
}