using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理游戏UI的显示和隐藏，以及根据游戏状态通过GameStateChangedCallBack切换不同的UI面板
/// </summary>
public class UIManager : MonoBehaviour, IGameStateListener
{
    [Header("Objects")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject waveTransitionPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject weaponSelectPanel;
    [SerializeField] private GameObject stageCompletePanel;
    [SerializeField] private GameObject gameOverPanel;
    private List<GameObject> panelList = new List<GameObject>();

    private void Awake()
    {
        panelList.AddRange(new GameObject[]{ menuPanel,
                                                                            shopPanel,
                                                                            waveTransitionPanel,
                                                                            gamePanel,
                                                                            weaponSelectPanel,
                                                                            stageCompletePanel,
                                                                            gameOverPanel});
    }
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
        switch (gameState)
        {
            case GameState.MENU:
                ShowPanel(menuPanel);
                break;
            case GameState.SHOP:
                ShowPanel(shopPanel);
                break;
            case GameState.WAVETRANSITION:
                ShowPanel(waveTransitionPanel);
                break;
            case GameState.GAME:
                ShowPanel(gamePanel);
                break;
            case GameState.WEAPON_SELECT:
                ShowPanel(weaponSelectPanel);
                break;
            case GameState.STAGE_COMPLETE:
                ShowPanel(stageCompletePanel);
                break;
            case GameState.GAMEOVER:
                ShowPanel(gameOverPanel);
                break;
        }
    }
    private void ShowPanel(GameObject panel)
    {
        foreach (GameObject p in panelList)
            p.SetActive(p == panel);
    }
}
