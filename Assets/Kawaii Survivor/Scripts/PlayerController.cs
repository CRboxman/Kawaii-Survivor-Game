using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private MobileJoystick playerJoystick;
    private Rigidbody2D rig;


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        
        rig.velocity = playerJoystick.GetMoveVector() ;
    }
}
