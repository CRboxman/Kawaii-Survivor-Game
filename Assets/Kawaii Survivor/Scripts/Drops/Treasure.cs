using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour,ICollectable
{
    [Header("Objects")]
    [SerializeField] public static Action onCollected;
    [SerializeField] public Animator treasureAnimator;
    public void Collect(Player player)
    {
        onCollected?.Invoke();
        treasureAnimator.Play("Collect_Anim");
        Destroy(gameObject,0.5f);
    }
}
