using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpGrateContainer_UI : MonoBehaviour
{
    [Header("Objects")]
    [field:SerializeField] public Button Button { get; private set; }
    [SerializeField]private Image image;
    [SerializeField]private TextMeshProUGUI upgradeNameText;
    [SerializeField]private TextMeshProUGUI upgradeValueText;
    public void Configure(Sprite icon,string upgradeName,string upgradeValue)
    {
        if (icon != null)
        {
            image.sprite = icon;
        }
        upgradeNameText.text=upgradeName;
        upgradeValueText.text=upgradeValue;
    }

}
