using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController: MonoBehaviour
{
    [Header("[����ѡ��]ѡ���Ŀ��ƽ������")]
    [SerializeField]private Transform target;
    [Header("��������ƶ���Χ��Ϊ�Գ�")]
    [SerializeField] private Vector2 minmaxXY;
    [Header("ƽ���̶�(Խ�̸���Խ��)")]
    [SerializeField]private float smoothTime = 1f;

    private Vector3 velocity=Vector3.zero;
    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Ŀ��δ������������������С������һ��Ŀ�ꡣ");
            return;
        }
        Vector3 targetPosition = target.position;
        targetPosition.z = -10;
        targetPosition.x=Mathf.Clamp(targetPosition.x, -minmaxXY.x , minmaxXY.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -minmaxXY.y, minmaxXY.y);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

}
