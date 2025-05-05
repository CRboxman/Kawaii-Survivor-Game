using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Player player;
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 1f;
    void Update()
    {
        if(player!=null)
        FollowPlayer();
    }
    public void StorePlayer(Player player)
    {
        this.player = player;
    }
    private void FollowPlayer()
    {
        Vector3 dir = (player.transform.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

}
