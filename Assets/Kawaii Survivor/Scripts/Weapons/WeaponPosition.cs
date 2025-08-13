using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

/// <summary>
/// 挂载到了场景预留的武器位置对象上，用于实例化武器，并将其放置在指定位置，同时根据传入的起始武器等级对武器进行升级。
/// </summary>
public class WeaponPosition : MonoBehaviour
{
    [Header("Objects")]
    public Weapon weapon { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AssignWeapon(Weapon weapon, int startWeaponLevel)
    {
        weapon = Instantiate(weapon, transform);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;

        weapon.UpgrateTo(startWeaponLevel);
    }
}
