using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectionContainer_UI : MonoBehaviour
{
    [Header("Objecrs")]
    [field: SerializeField] public Button Button { get; private set; }
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image[] weaponLevelImage;
    public void Configure(Sprite iconSprite, string weaponName, int level)
    {
        icon.sprite = iconSprite;
        nameText.text = weaponName;
        Color imageColor=ColorHoloder.GetColor(level);
        foreach (Image image in weaponLevelImage)
            image.color = imageColor;
    }

    public void Select()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one * 1.075f, .3f).setEase(LeanTweenType.easeInOutSine);
    }

    public void Deselect()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one, .3f);
    }
}
