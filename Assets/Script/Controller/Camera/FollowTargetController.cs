using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FollowTargetController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // Tham chiếu đến Cinemachine virtual camera
    public Transform target; // Đối tượng Megaman mà camera theo dõi
    public float offsetY ; // Độ chênh lệch tọa độ Y

    void Update()
    {
        if (virtualCamera != null && target != null)
        {
            
            Vector3 newPosition = virtualCamera.transform.position;

            newPosition = new Vector3(target.position.x, target.position.y + offsetY,-10);
            
            virtualCamera.transform.position = newPosition;
        }
    }
}