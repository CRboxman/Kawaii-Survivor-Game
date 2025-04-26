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
        //���� EventTrigger.Entry ��Ŀ,ÿ�� EventTrigger.Entry �������һ���¼����䴦���߼�����ҪΪÿ���¼����ʹ���һ�� EventTrigger.Entry ����
        EventTrigger.Entry entry = new EventTrigger.Entry();
        //��������¼�������ΪPointerDown
        entry.eventID = EventTriggerType.PointerDown;
      //����һ���µ�EventTrigger.Entry���������¼�����ΪPointerDown
        entry.callback.AddListener((data) =>
        {
            //���¼�������ʱ������printHello����
            printHello();
        });
   
        //�����EventTrigger���һ���ص�������������PointerDown�¼�ʱ����printHello����
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
