using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour,IGameStateListener
{
    [Header("Settings")]
    [SerializeField]private GameObject menuPanel;
    [SerializeField]private GameObject shopPanel;
    [SerializeField]private GameObject waveTransitionPanel;
    [SerializeField]private GameObject gamePanel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameStateChangedCallBack(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.Menu:
                menuPanel.SetActive(true);
                shopPanel.SetActive(false);
                waveTransitionPanel.SetActive(false);
                gamePanel.SetActive(false);
                Debug.Log("这里是菜单哦！！");
                break;
            case GameState.Shop:
                menuPanel.SetActive(false);
                shopPanel.SetActive(true);
                waveTransitionPanel.SetActive(false);
                gamePanel.SetActive(false);
                break;
            case GameState.WaveTransition:
                menuPanel.SetActive(false);
                shopPanel.SetActive(false);
                waveTransitionPanel.SetActive(true);
                gamePanel.SetActive(false);
                break;
            case GameState.Game:
                menuPanel.SetActive(false);
                shopPanel.SetActive(false);
                waveTransitionPanel.SetActive(false);
                gamePanel.SetActive(true);
                break;
        }
    }
}
