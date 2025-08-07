using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveTransitionManager : MonoBehaviour, IGameStateListener
{

    [Header("Objects")]
    [SerializeField] private PlayerStateManager playerStateManager;
    [SerializeField] private UpGrateContainer_UI[] upgradeContainerButton;
    //[Header("Settings")]

    private void Awake()
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
            case GameState.WAVETRANSITION:
                ConfigureUpgradeContainerButton();
                break;
        }
    }
    [Button]
    private void ConfigureUpgradeContainerButton()
    {
        for (int i = 0; i < upgradeContainerButton.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(PlayerState)).Length);
            PlayerState state = (PlayerState)Enum.GetValues(typeof(PlayerState)).GetValue(randomIndex);
            string randomStateString = Enums.GetPlayerStateName(state);
            string upgradeValueString ;
            Action action = GetAction(state,out upgradeValueString);
            upgradeContainerButton[i].Configure(null, randomStateString, upgradeValueString);// 设置按钮文本和描述
            upgradeContainerButton[i].Button.onClick.RemoveAllListeners();// 清除之前的监听器
            upgradeContainerButton[i].Button.onClick.AddListener(() =>
            {
                action?.Invoke();
            });//根据这次随机的状态，设置按钮的点击事件，触发对应的状态增加方法 
            // 添加一个回调，当玩家选择了这个奖励时，调用GameManager的WaveCompletedCallBack方法，判断是否还有升级点
            // 如果没有升级点，则进入商店界面，有升级点，则继续等待玩家选择（手动执行回合结束回调，统一调用一次状态接口方法）
            //目前有回合过渡阶段的状态升级以及判断是否还可以再次选择，ui，波次管理器的移动和生成
            // 选择之后再次判断
            upgradeContainerButton[i].Button.onClick.AddListener(()=>BounsSelectedCallBack());
        }
    }
    private void BounsSelectedCallBack()
    {
        GameManager.instance.WaveCompletedCallBack();
    }
    private Action GetAction(PlayerState state, out string upgradeValueString)
    {
        upgradeValueString = "";
        float value=0;

        switch (state)
        {
            case PlayerState.Attack:
                value = UnityEngine.Random.Range(1, 11);
                upgradeValueString = "+" + value + " 攻击力";
                return () =>
                {
                    playerStateManager.AddPlayerState(state, value);
                    Debug.Log("增加攻击力：" + value);
                };

            case PlayerState.AttackSpeed:
                value = UnityEngine.Random.Range(0.1f, 1.0f);
                upgradeValueString = "+" + value.ToString("F2") + " 攻速";
                return () =>
                {
                    playerStateManager.AddPlayerState(state, value);
                    Debug.Log("增加攻速：" + value);
                };

            case PlayerState.CriticalChance:
                value = UnityEngine.Random.Range(1, 10);
                upgradeValueString = "+" + value + "% 暴击率";
                return () =>
                {
                    playerStateManager.AddPlayerState(state, value);
                    Debug.Log("增加暴击率：" + value);
                };

            case PlayerState.CriticalPercent:
                value = UnityEngine.Random.Range(10, 51);
                upgradeValueString = "+" + value + "% 暴击伤害";
                return () =>
                {
                    playerStateManager.AddPlayerState(state, value);
                    Debug.Log("增加暴击伤害：" + value);
                };

            case PlayerState.MoveSpeed:
                value = UnityEngine.Random.Range(1, 5);
                upgradeValueString = "+" + value + " 移动速度";
                return () =>
                {
                    playerStateManager.AddPlayerState(state, value);
                    Debug.Log("增加移动速度：" + value);
                };

            case PlayerState.MaxHealth:
                value = UnityEngine.Random.Range(10, 101);
                upgradeValueString = "+" + value + " 最大生命";
                return () =>
                {
                    playerStateManager.AddPlayerState(state, value);
                    Debug.Log("增加最大生命：" + value);
                };

            case PlayerState.Range:
                value = UnityEngine.Random.Range(1, 5);
                upgradeValueString = "+" + value + " 攻击范围";
                return () =>
                {
                    playerStateManager.AddPlayerState(state, value);
                    Debug.Log("增加攻击范围：" + value);
                };

            case PlayerState.HealthRecoverySpeed:
                value = UnityEngine.Random.Range(0.1f, 1.0f);
                upgradeValueString = "+" + value.ToString("F2") + " 生命回复";
                return () =>
                {
                    playerStateManager.AddPlayerState(state, value);
                    Debug.Log("增加生命回复速度：" + value);
                };

            case PlayerState.Armor:
                value = UnityEngine.Random.Range(1, 10);
                upgradeValueString = "+" + value + " 护甲";
                return () =>
                {
                    playerStateManager.AddPlayerState(state, value);
                    Debug.Log("增加护甲：" + value);
                };

            case PlayerState.Luck:
                value = UnityEngine.Random.Range(1, 5);
                upgradeValueString = "+" + value + " 幸运值";
                return () =>
                {
                    playerStateManager.AddPlayerState(state, value);
                    Debug.Log("增加幸运：" + value);
                };

            case PlayerState.Dodge:
                value = UnityEngine.Random.Range(1, 10);
                upgradeValueString = "+" + value + "% 闪避";
                return () =>
                {
                    playerStateManager.AddPlayerState(state, value);
                    Debug.Log("增加闪避率：" + value);
                };

            case PlayerState.LifeSteal:
                value = UnityEngine.Random.Range(1, 10);
                upgradeValueString = "+" + value + "% 吸血";
                return () =>
                {
                    playerStateManager.AddPlayerState(state, value);
                    Debug.Log("增加吸血：" + value);
                };

            default:
                upgradeValueString = "未知属性";
                return () => Debug.LogWarning("未知的 PlayerState: " + state);
        }
    }

}

