using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void AddWeapon(WeaponDataSO selectedWeapons, int startWeaponLevel)
    {
        weaponPositions[UnityEngine.Random.Range(0, weaponPositions.Length)].AssignWeapon(selectedWeapons.weaponPref);
    }
}
