using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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
