using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour
{

    [SerializeField] private MobileJoystick playerJoystick;
    [SerializeField]private float moveSpeed = 0.1f;
    private Rigidbody2D rig;


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate()
    {

        rig.velocity = playerJoystick.GetMoveVector()*moveSpeed*Time.deltaTime;
    }


}
