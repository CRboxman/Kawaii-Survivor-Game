using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 单例，提供了方法来改变游戏状态，通过主动调用这些方法或回合结束自动触发GameStateChangedCallBack监听
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; // 设置目标帧率为60
        SetGameState(GameState.MENU);
    }
    public void StartGame()=> SetGameState(GameState.GAME);
    public void StartWeaponSelect()=> SetGameState(GameState.WEAPON_SELECT);
    public void StartShop()=> SetGameState(GameState.SHOP);
    // Update is called once per frame
    void Update()
    {

    }
    public void SetGameState(GameState gameState)
    {
        IEnumerable<IGameStateListener> gameStateListeners =
                            FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                            .OfType<IGameStateListener>();
        foreach (IGameStateListener listener in gameStateListeners)
        {
            listener.GameStateChangedCallBack(gameState);
        }
    }
    public void WaveCompletedCallBack()
    {
        if (Player.instance.HasLevelUp())
        {
            SetGameState(GameState.WAVETRANSITION);
        }
        else
        {
            SetGameState(GameState.SHOP);
        }
    }
    public void ManagerGameOver()
    {
        SceneManager.LoadScene(0);
    }
}
/// <summary>
/// 这个接口用于监听游戏状态的变化，GameStateChangedCallBack由GameManager提供的方法或者回合结束自动调用
/// </summary>
public interface IGameStateListener
{
    void GameStateChangedCallBack(GameState gameState);
}
