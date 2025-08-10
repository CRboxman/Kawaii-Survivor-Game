using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour,IPlayerStatesDependency
{
    [Header("Settings")]
    [SerializeField]private float baseMoveSpeed = 8f;
    [SerializeField]private float moveSpeed = 8f;
    private Rigidbody2D rig;
    public bool canMove=false;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        moveSpeed=baseMoveSpeed;
    }

    private void FixedUpdate()
    {
        if (!canMove)
            return;
        rig.velocity = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"))* moveSpeed ;
    }

    public void UpdateStats(PlayerStateManager playerStateManager)
    {
        float addedMoveSpeed = playerStateManager.GetPlayerStateValue(PlayerState.MoveSpeed)/100;
        moveSpeed = baseMoveSpeed *(1+ addedMoveSpeed) ;
    }
}
