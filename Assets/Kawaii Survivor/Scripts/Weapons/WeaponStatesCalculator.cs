using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WeaponStatesCalculator
{
    public static Dictionary<PlayerState,float> GetStates(WeaponDataSO weaponDataSO,int level)
    {
        float multiplier = 1 + (float)level / 3;

        Dictionary<PlayerState, float> calculatedStates = new Dictionary<PlayerState, float>();
        
        foreach(KeyValuePair<PlayerState, float> kvp in weaponDataSO.BaseStats)
        {
            if(weaponDataSO.weaponPref.GetType() != typeof(MeleeWeapon) && kvp.Key == PlayerState.Range)
            {
                calculatedStates.Add(kvp.Key, kvp.Value);
            }
            else
            {
                calculatedStates.Add(kvp.Key, kvp.Value*multiplier);
            }
        }
            return calculatedStates;
    }
}
