using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{


    [SerializeField]private float moveSpeed = 0.1f;
    private Rigidbody2D rig;


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();

    }



    private void FixedUpdate()
    {
        rig.velocity = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"))* moveSpeed ;
    }
}
