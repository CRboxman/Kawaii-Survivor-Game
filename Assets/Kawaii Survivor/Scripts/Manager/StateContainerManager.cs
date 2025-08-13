using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 负责生成状态容器UI的管理器
/// </summary>
public class StateContainerManager : MonoBehaviour
{
    public static StateContainerManager instance;


    [Header("Objects")]
    [SerializeField]private StateContainer_UI statContainerPrefab;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    /// <summary>
    /// 初始化状态容器UI
    /// </summary>
    /// <param name="stateDictionary"></param>
    /// <param name="parent"></param>
    private void GenerateStatesContainers(Dictionary<PlayerState, float> stateDictionary, Transform parent)
    {
        List<StateContainer_UI> existingContainers = new List<StateContainer_UI>();
        foreach (KeyValuePair<PlayerState, float> kvp in stateDictionary)
        {
            StateContainer_UI statContainer = Instantiate(statContainerPrefab, parent);
            existingContainers.Add(statContainer);

            Sprite icon = ResourceManager.GetStatIcon(kvp.Key);
            string statName = Enums.GetPlayerStateName(kvp.Key) + ":";
            string statValue = kvp.Value.ToString();
            switch (kvp.Key)
            {
                case PlayerState.Attack:
                     statValue = kvp.Value.ToString("F1");
                    break;
                case PlayerState.AttackSpeed:
                    statValue = kvp.Value.ToString("F1")+"/s";
                    break;
                case PlayerState.CriticalChance:
                    statValue = kvp.Value.ToString("F1") + "%";
                    break;
                case PlayerState.CriticalPercent:
                    statValue = kvp.Value.ToString("F1") + "%";
                    break;
                case PlayerState.Range:
                    statValue = kvp.Value.ToString("F1") ;
                    break;
            }
            statContainer.Configure(icon, statName, statValue);


        }
        LeanTween.delayedCall(Time.deltaTime * 2, () => ResizeTexts(existingContainers));
    }

    private void ResizeTexts(List<StateContainer_UI> existingContainers)
    {
        float minFontSize = float.MaxValue;
        foreach (StateContainer_UI container in existingContainers)
        {
            float fontSize = container.GetFontSize();
            if (fontSize < minFontSize)
                minFontSize = fontSize;
            container.SetFontSize(minFontSize);
        }
    }
    /// <summary>
    /// 初始化状态容器UI
    /// </summary>
    /// <param name="stateDictionart"></param>
    /// <param name="parent"></param>
    public static void GenerateStateContainers(Dictionary<PlayerState, float> stateDictionart,Transform parent)
    {
        instance.GenerateStatesContainers(stateDictionart, parent);
    }
}
