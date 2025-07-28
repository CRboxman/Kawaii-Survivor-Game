using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : DropableCurrency
{
    [Header("Objects")]
    [SerializeField] public static Action<Candy> onCollected;
    //protected override string GetCollectAnimationName()
    //{
    //    return "Candy_Collect_Anim"; // 自己的动画名
    //}

    //protected override float GetCollectAnimationDelay()
    //{
    //    return 0.2f; // Candy 动画短一点
    //}

    protected override void Collected()
    {
        // Candy 收集完后行为
        onCollected?.Invoke(this);
    }
}
