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
            string randomStateString = Enums.GetOlayerStateName(state);
            string buttonString ;
            Action action = GetAction(state,out buttonString);
            upgradeContainerButton[i].Configure(null, randomStateString, buttonString);
            upgradeContainerButton[i].Button.onClick.RemoveAllListeners();
            upgradeContainerButton[i].Button.onClick.AddListener(() =>
            {
                action?.Invoke();
            });
        }
    }

    private Action GetAction(PlayerState state, out string buttonString)
    {
        buttonString = "";
        float value;

        switch (state)
        {
            case PlayerState.Attack:
                value = UnityEngine.Random.Range(1, 11);
                buttonString = "+" + value.ToString() + " 攻击力";
                return () => Debug.Log("增加攻击力：" + value);

            case PlayerState.AttackSpeed:
                value = UnityEngine.Random.Range(0.1f, 1.0f);
                buttonString = "+" + value.ToString("F2") + " 攻速";
                return () => Debug.Log("增加攻速：" + value);

            case PlayerState.CriticalChance:
                value = UnityEngine.Random.Range(1, 10);
                buttonString = "+" + value.ToString() + "% 暴击率";
                return () => Debug.Log("增加暴击率：" + value);

            case PlayerState.CriticalPercent:
                value = UnityEngine.Random.Range(10, 51);
                buttonString = "+" + value.ToString() + "% 暴击伤害";
                return () => Debug.Log("增加暴击伤害：" + value);

            case PlayerState.MoveSpeed:
                value = UnityEngine.Random.Range(1, 5);
                buttonString = "+" + value.ToString() + " 移动速度";
                return () => Debug.Log("增加移动速度：" + value);

            case PlayerState.MaxHealth:
                value = UnityEngine.Random.Range(10, 101);
                buttonString = "+" + value.ToString() + " 最大生命";
                return () => Debug.Log("增加最大生命：" + value);

            case PlayerState.Range:
                value = UnityEngine.Random.Range(1, 5);
                buttonString = "+" + value.ToString() + " 攻击范围";
                return () => Debug.Log("增加攻击范围：" + value);

            case PlayerState.HealthRecoverySpeed:
                value = UnityEngine.Random.Range(0.1f, 1.0f);
                buttonString = "+" + value.ToString("F2") + " 生命回复";
                return () => Debug.Log("增加生命回复速度：" + value);

            case PlayerState.Armor:
                value = UnityEngine.Random.Range(1, 10);
                buttonString = "+" + value.ToString() + " 护甲";
                return () => Debug.Log("增加护甲：" + value);

            case PlayerState.Luck:
                value = UnityEngine.Random.Range(1, 5);
                buttonString = "+" + value.ToString() + " 幸运值";
                return () => Debug.Log("增加幸运：" + value);

            case PlayerState.Dodge:
                value = UnityEngine.Random.Range(1, 10);
                buttonString = "+" + value.ToString() + "% 闪避";
                return () => Debug.Log("增加闪避率：" + value);

            case PlayerState.LifeSteal:
                value = UnityEngine.Random.Range(1, 10);
                buttonString = "+" + value.ToString() + "% 吸血";
                return () => Debug.Log("增加吸血：" + value);

            default:
                buttonString = "未知";
                return () => Debug.LogWarning("未知状态：" + state);
        }
    }

}

