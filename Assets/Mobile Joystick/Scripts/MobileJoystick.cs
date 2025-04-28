using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileJoystick : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private RectTransform joystickOutline;
    [SerializeField] private RectTransform joystickKnob;

    [Header(" Settings ")]
    [SerializeField] private float moveFactor;
    private Vector3 clickedPosition;
    private Vector3 move;
    private bool canControl;

    // Start is called before the first frame update
    void Start()
    {
        HideJoystick();
    }
    // Update is called once per frame
    void Update()
    {
        if(canControl)
            ControlJoystick();
    }

    public void ClickedOnJoystickZoneCallback()
    {
        clickedPosition = Input.mousePosition;
        joystickOutline.position = clickedPosition;

        ShowJoystick();
    }
    public Vector3 GetMoveVector()
    {
        float canvasScale = GetComponentInParent<Canvas>().GetComponent<RectTransform>().localScale.x;
        return move / canvasScale;
    }
    private void OnDisable()
    {
        HideJoystick();
    }
    private void ShowJoystick()
    {
        joystickOutline.gameObject.SetActive(true);
        canControl = true;
    }

    private void HideJoystick()
    {
        joystickOutline.gameObject.SetActive(false);
        canControl = false;

        move = Vector3.zero;
    }

    private void ControlJoystick()
    {
        //��ǰλ��
        Vector3 currentPosition = Input.mousePosition;
        //�ƶ�����
        Vector3 direction = currentPosition - clickedPosition;
        //������x���ű���
        float canvasScale = GetComponentInParent<Canvas>().GetComponent<RectTransform>().localScale.x;
        //�����ƶ��Ĵ�С���϶��Ĵ�С��
        //ע�⣺�����moveFactor��һ���������ӣ����ڵ���ҡ�˵�������
        //�����Ҫ��������ҡ�ˣ������������ֵ
        //�����Ҫ����������ҡ�ˣ����Լ�С���ֵ
        //canvasScale��������Ϊ��ȷ���ڲ�ͬ�ֱ��ʺ�������ҡ�˵��ƶ�����һ��
        float moveMagnitude = direction.magnitude * moveFactor * canvasScale;
        //��������ƶ�����Ϊҡ�˵İ뾶
        float absoluteWidth = joystickOutline.rect.width / 2;
        //����ʵ�ʵĿ�ȣ����ǻ������ţ�
        float realWidth = absoluteWidth * canvasScale;
        //�����ƶ����벻����ʵ�ʿ��
        moveMagnitude = Mathf.Min(moveMagnitude, realWidth);
        //�����ƶ�����
        move = direction.normalized * moveMagnitude;
        //����Ŀ��λ��
        Vector3 targetPosition = clickedPosition + move;
        //����Ŀ��λ����ҡ��������
        joystickKnob.position = targetPosition;

        if (Input.GetMouseButtonUp(0))
            HideJoystick();
    }


}
