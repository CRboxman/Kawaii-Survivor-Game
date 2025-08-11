using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionManager : MonoBehaviour,IGameStateListener
{
    [Header("Objects")]
    [SerializeField] private Transform containersParent;
    [SerializeField] private WeaponSelectionContainer_UI weaponSelectionContainerPrefab;
    [SerializeField] private WeaponDataSO [] weaponDatas;
    [SerializeField] private WeaponDataSO selectedWeapon;
    [SerializeField] private PlayerWeapons playerWeapons;
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
        for (int i = 0; i <weaponDatas.Length; i++)
        {
            GenerateWeaponContainer();
        }
    }
    private void GenerateWeaponContainer()
    {
        WeaponSelectionContainer_UI container= Instantiate(weaponSelectionContainerPrefab, containersParent);
        WeaponDataSO randomWeaponData= weaponDatas[Random.Range(0, weaponDatas.Length)];

        int level=UnityEngine.Random.Range(0,2);
        startWeaponLevel = level;
        container.Configure(randomWeaponData.WeaponSprite,randomWeaponData.WeaponName, level);
        container.Button.onClick.RemoveAllListeners();
        container.Button.onClick.AddListener(() =>
        {
            WeaponSelectedCallBack(container,randomWeaponData);
        });
    }
    private void WeaponSelectedCallBack(WeaponSelectionContainer_UI container, WeaponDataSO randomWeaponData)
    {
        selectedWeapon = randomWeaponData;
        foreach (WeaponSelectionContainer_UI containerS in containersParent.GetComponentsInChildren<WeaponSelectionContainer_UI>())
        {
            if (containerS == container)
                containerS.Select();
            else
                containerS.Deselect();
        }
    }
}
