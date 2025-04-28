using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController: MonoBehaviour
{
    [Header("[必须选择]选择此目标平滑跟随")]
    [SerializeField]private Transform target;
    [Header("限制相机移动范围，为对称")]
    [SerializeField] private Vector2 minmaxXY;
    [Header("平滑程度(越短跟的越紧)")]
    [SerializeField]private float smoothTime = 1f;

    private Vector3 velocity=Vector3.zero;
    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("目标未分配在摄像机控制器中。请分配一个目标。");
            return;
        }
        Vector3 targetPosition = target.position;
        targetPosition.z = -10;
        targetPosition.x=Mathf.Clamp(targetPosition.x, -minmaxXY.x , minmaxXY.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -minmaxXY.y, minmaxXY.y);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

}
