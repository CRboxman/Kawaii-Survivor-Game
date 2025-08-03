using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 要求需要实现收集功能的类实现此接口
/// </summary>
public interface ICollectable 
{
    void Collect(Player player);
}
