using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHoloder : MonoBehaviour
{
    public static ColorHoloder instance;
    [Header(" Objects ")]
    [SerializeField] private ColorSo colorSo;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public static Color GetColor(int level)
    {
        level = Mathf.Clamp(level, 0, instance.colorSo.LevelColors.Length);
        return instance.colorSo.LevelColors[level];
    }
    public static Color GetOutlineColor(int level)
    {
        level = Mathf.Clamp(level, 0, instance.colorSo.LevelOutlineColors.Length);
        return instance.colorSo.LevelOutlineColors[level];
    }
}
