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
        //当前位置
        Vector3 currentPosition = Input.mousePosition;
        //移动方向
        Vector3 direction = currentPosition - clickedPosition;
        //画布的x缩放比例
        float canvasScale = GetComponentInParent<Canvas>().GetComponent<RectTransform>().localScale.x;
        //计算移动的大小（拖动的大小）
        //注意：这里的moveFactor是一个缩放因子，用于调整摇杆的灵敏度
        //如果需要更灵敏的摇杆，可以增大这个值
        //如果需要更不灵敏的摇杆，可以减小这个值
        //canvasScale在这里是为了确保在不同分辨率和缩放下摇杆的移动距离一致
        float moveMagnitude = direction.magnitude * moveFactor * canvasScale;
        //限制最大移动距离为摇杆的半径
        float absoluteWidth = joystickOutline.rect.width / 2;
        //计算实际的宽度（考虑画布缩放）
        float realWidth = absoluteWidth * canvasScale;
        //限制移动距离不超过实际宽度
        moveMagnitude = Mathf.Min(moveMagnitude, realWidth);
        //计算移动方向
        move = direction.normalized * moveMagnitude;
        //计算目标位置
        Vector3 targetPosition = clickedPosition + move;
        //限制目标位置在摇杆轮廓内
        joystickKnob.position = targetPosition;

        if (Input.GetMouseButtonUp(0))
            HideJoystick();
    }


}
