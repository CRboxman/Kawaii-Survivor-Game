using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// (带有此接口的脚本能被玩家碰撞触发)要求需要实现收集功能的类实现此接口，最后通过接口注入原则，通过PlayerDetection检测到后调用Collect方法。
/// DropableCurrency类继承了这个接口，需要被玩家拾取后追踪并延迟收集可以继承DropableCurrency类
/// </summary>

//DropableCurrency类继承了这个接口，
//可以被玩家收集的货币类，
//向玩家移动并在最后执行收集方法，
//留下了动画和收集逻辑的抽象方法供子类实现。
//总结：需要单纯被收集执行一些简单收集逻辑的类可以实现这个接口，
//          而需要被玩家拾取后追踪并延迟收集可以继承DropableCurrency类。
public interface ICollectable 
{
    void Collect(Player player);
}
