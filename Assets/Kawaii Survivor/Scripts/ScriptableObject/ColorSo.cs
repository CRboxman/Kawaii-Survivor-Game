using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Color Data", menuName = "ScriptableObject/new Color Data")]

public class ColorSo : ScriptableObject
{

    [field: SerializeField] public Color[] LevelColors { get; private set; }
    [field: SerializeField] public Color[] LevelOutlineColors { get; private set; }
}
