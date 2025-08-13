using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ÎäÆ÷µÄ×´Ì¬ÈÝÆ÷UI£¬°üÀ¨Í¼±ê£¬×´Ì¬Ãû×Ö£¬×´Ì¬ÊýÖµ
/// </summary>
public class StateContainer_UI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Image statImage;
    [SerializeField] private TextMeshProUGUI statText;
    [SerializeField] private TextMeshProUGUI statValueText;
    public void Configure(Sprite icon, string statName, string statValue)
    {
        statImage.sprite = icon;
        statText.text = statName;
        statValueText.text = statValue;
    }
    public float GetFontSize()
    {
        return statText.fontSize;
    }
    public void SetFontSize(float fontSize)
    {
        statText.fontSize = fontSize;
        statValueText.fontSize = fontSize;
    }
}
