using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    [SerializeField]private float moveSpeed = 0.1f;
    private Rigidbody2D rig;
    public bool canMove=false;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        if (!canMove)
            return;
        rig.velocity = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"))* moveSpeed ;
    }
}
