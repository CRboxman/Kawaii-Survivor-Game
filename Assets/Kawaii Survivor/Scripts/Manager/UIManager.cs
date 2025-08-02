using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IGameStateListener
{
    [Header("Settings")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject waveTransitionPanel;
    [SerializeField] private GameObject gamePanel;
    private List<GameObject> panelList = new List<GameObject>();

    private void Awake()
    {
        panelList.AddRange(new GameObject[]
         { menuPanel,
            shopPanel,
            waveTransitionPanel,
            gamePanel });
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
            case GameState.Menu:
                ShowPanel(menuPanel);
                break;
            case GameState.Shop:
                ShowPanel(shopPanel);
                break;
            case GameState.WaveTransition:
                ShowPanel(waveTransitionPanel);
                break;
            case GameState.Game:
                ShowPanel(gamePanel);
                break;
        }
    }
    private void ShowPanel(GameObject panel)
    {
        foreach (GameObject p in panelList)
            p.SetActive(p == panel);
    }
}
