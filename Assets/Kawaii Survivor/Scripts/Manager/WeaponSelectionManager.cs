using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionManager : MonoBehaviour,IGameStateListener
{
    [Header("Objects（其中weaponDatas表示会有几个选择的有等级的武器，并随机选择并随机分配）")]
    [SerializeField] private Transform containersParent;
    [SerializeField] private WeaponSelectionContainer_UI weaponSelectionContainerPrefab;
    [SerializeField] private PlayerWeapons playerWeapons;
    [HorizontalLine]
    [SerializeField] private int weaponCount;
    [SerializeField] private WeaponDataSO [] weaponDatas;
    [SerializeField] private WeaponDataSO selectedWeapon;
    private int startWeaponLevel;

    public void GameStateChangedCallBack(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GAME:
                if (selectedWeapon == null)
                    return;
                    playerWeapons.AddWeapon(selectedWeapon, startWeaponLevel);
                selectedWeapon = null;
                startWeaponLevel = 0;
                break;
            case GameState.WEAPON_SELECT:
                ConfigureWeaponSelectionContainers();
                break;
        }
    }
    [Button]
    private void ConfigureWeaponSelectionContainers()
    {
        containersParent.ClearChild();
        for (int i = 0; i <weaponCount; i++)
        {
            GenerateWeaponContainer();
        }
    }
    private void GenerateWeaponContainer()
    {
        WeaponSelectionContainer_UI container= Instantiate(weaponSelectionContainerPrefab, containersParent);
        WeaponDataSO randomWeaponData= weaponDatas[Random.Range(0, weaponDatas.Length)];
        int level=UnityEngine.Random.Range(0,4);

        
        container.ConfigureWeaponSelection(randomWeaponData.WeaponSprite,randomWeaponData.WeaponName, level,randomWeaponData);

        container.Button.onClick.RemoveAllListeners();
        container.Button.onClick.AddListener(() =>
        {
            WeaponSelectedCallBack(container,randomWeaponData, level);
        });
    }
    private void WeaponSelectedCallBack(WeaponSelectionContainer_UI container, WeaponDataSO randomWeaponData,int level)
    {
        selectedWeapon = randomWeaponData;
        startWeaponLevel = level;
        foreach (WeaponSelectionContainer_UI containerS in containersParent.GetComponentsInChildren<WeaponSelectionContainer_UI>())
        {
            if (containerS == container)
                containerS.Select();
            else
                containerS.Deselect();
        }
    }
}
