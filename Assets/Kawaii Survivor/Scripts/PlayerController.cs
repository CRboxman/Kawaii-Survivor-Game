using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;


public class PlayerController : MonoBehaviour
{

    [SerializeField] private MobileJoystick playerJoystick;
    [SerializeField]private float moveSpeed = 0.1f;
    private Rigidbody2D rig;
    public EventTrigger et;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        //创建 EventTrigger.Entry 条目,每个 EventTrigger.Entry 对象代表一种事件及其处理逻辑。需要为每种事件类型创建一个 EventTrigger.Entry 对象。
        EventTrigger.Entry entry = new EventTrigger.Entry();
        //声明这个事件的类型为PointerDown
        entry.eventID = EventTriggerType.PointerDown;
      //创建一个新的EventTrigger.Entry对象，设置事件类型为PointerDown
        entry.callback.AddListener((data) =>
        {
            //当事件被触发时，调用printHello方法
            printHello();
        });
   
        //给这个EventTrigger添加一个回调函数，当触发PointerDown事件时调用printHello方法
        et.triggers.Add(entry);

    }

    void FixedUpdate()
    {

        rig.velocity = playerJoystick.GetMoveVector()*moveSpeed*Time.deltaTime;
    }
    private void printHello()
    {
        Debug.Log("Hello");
    }

}
