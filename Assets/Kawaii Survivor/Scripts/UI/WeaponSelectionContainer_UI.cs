using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 整个武器选择的容器UI，首先初始化武器图标，名字，等级，随后是状态容器
/// </summary>
public class WeaponSelectionContainer_UI : MonoBehaviour
{
    [Header("Objecrs")]
    [field: SerializeField] public Button Button { get; private set; }
    [SerializeField] private Image weaponIcon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Transform statContainersParent;
    [SerializeField] private Image[] weaponLevelImage;
    private WeaponDataSO WeaponDataSO;
    /// <summary>
    /// 生成的是武器选择的UI的配置，包括图标，名字，等级，最后是状态容器UI的初始化
    /// </summary>
    /// <param name="iconSprite"></param>
    /// <param name="weaponName"></param>
    /// <param name="level"></param>
    /// <param name="weaponDataSO"></param>
    public void ConfigureWeaponSelection(Sprite iconSprite, string weaponName, int level, WeaponDataSO weaponDataSO)
    {
        weaponIcon.sprite = iconSprite;
        nameText.text = weaponName+$"(Lv {level+1})";
        Color imageColor = ColorHoloder.GetColor(level);
        nameText.color = imageColor;
        foreach (Image image in weaponLevelImage)
            image.color = imageColor;
        Dictionary<PlayerState, float> calculatedStates = WeaponStatesCalculator.GetStates(weaponDataSO, level);
        COnfigureStateContainer(calculatedStates);
    }
    /// <summary>
    /// 生成的是武器的状态容器
    /// </summary>
    /// <param name="weaponDataSO"></param>
    private void COnfigureStateContainer(Dictionary<PlayerState,float> calculatedStates)
    {
        StateContainerManager.GenerateStateContainers(calculatedStates, statContainersParent);

    }

    public void Select()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one * 1.06f, .13f).setEase(LeanTweenType.easeInOutSine);
    }

    public void Deselect()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one, .13f);
    }
}
