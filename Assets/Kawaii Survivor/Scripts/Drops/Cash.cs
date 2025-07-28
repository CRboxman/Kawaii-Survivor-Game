using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cash : DropableCurrency
{
    [Header("Objects")]
    [SerializeField] public static Action<Cash> onCollected;
    //protected override string GetCollectAnimationName()
    //{
    //    return "Cash_Collect_Anim"; // 自己的动画名
    //}

    //protected override float GetCollectAnimationDelay()
    //{
    //    return 0.5f; // Cash 动画长一点
    //}

    protected override void Collected()
    {
        // Cash 收集完后行为
        onCollected?.Invoke(this);
    }
}
