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
    [Header("Debug")]
    [SerializeField] private bool detectGizmos;
    [SerializeField] private Vector2 detectGizmos_offset;
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

        // 以OnDrawGizmos绘制的区域为限制范围
        float minX = -minmaxXY.x / 2f + detectGizmos_offset.x;
        float maxX = minmaxXY.x / 2f + detectGizmos_offset.x;
        float minY = -minmaxXY.y / 2f + detectGizmos_offset.y;
        float maxY = minmaxXY.y / 2f + detectGizmos_offset.y;

        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
    private void OnDrawGizmos()
    {
        if (detectGizmos)
        {
            Gizmos.color = Color.yellow;
            // 绘制以(0,0)为中心，宽高为minmaxXY的矩形区域
            Vector3 center = new Vector3(detectGizmos_offset.x, detectGizmos_offset.y, -10);
            Vector3 size = new Vector3(minmaxXY.x, minmaxXY.y , 0);
            Gizmos.DrawWireCube(center, size);
        }
    }
}
