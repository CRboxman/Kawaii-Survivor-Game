using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StateIconData", menuName = "ScriptableObject/new StateIcon Data")]
public class StateIconSO : ScriptableObject
{
    [field:SerializeField]public StateIconPair[] stateIconPairs { get; private set; }
}
[System.Serializable]
public struct StateIconPair
{
    public PlayerState playerState;
    public Sprite icon;
}
