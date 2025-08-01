using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        SetGameState(GameState.Menu);
    }
    public void StartGame()=> SetGameState(GameState.Game);
    public void StartShop()=> SetGameState(GameState.Shop);
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
            SetGameState(GameState.WaveTransition);
        }
        else
        {
            SetGameState(GameState.Shop);
        }
    }
}
public interface IGameStateListener
{
    void GameStateChangedCallBack(GameState gameState);
}
