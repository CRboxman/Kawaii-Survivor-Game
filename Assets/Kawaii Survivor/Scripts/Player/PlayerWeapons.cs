using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 保留玩家武器位置，并在随机位置生成武器
/// </summary>
public class PlayerWeapons : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private WeaponPosition[] weaponPositions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 通过传入的武器So以及等级，在随机位置生成武器并实例化，同时根据传入的起始武器等级对武器进行升级。
    /// </summary>
    /// <param name="selectedWeapons"></param>
    /// <param name="startWeaponLevel"></param>
    public void AddWeapon(WeaponDataSO selectedWeapons, int startWeaponLevel)
    {
        weaponPositions[UnityEngine.Random.Range(0, weaponPositions.Length)].AssignWeapon(selectedWeapons.weaponPref,startWeaponLevel);
    }
}
