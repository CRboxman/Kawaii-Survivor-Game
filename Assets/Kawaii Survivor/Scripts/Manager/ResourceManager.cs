using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceManager
{
    const string characterPath = "Data/StateIconData";
    private static StateIconPair[] stateIconSO;
    public static Sprite GetStatIcon(PlayerState playerState)
    {
        if (stateIconSO == null)
        {
            StateIconSO stateIconData = Resources.Load<StateIconSO>(characterPath);
            stateIconSO = stateIconData.stateIconPairs;
        }
        foreach (StateIconPair pair in stateIconSO)
        {
            if (playerState == pair.playerState)
                return pair.icon;
        }
        return null;
    }
}
