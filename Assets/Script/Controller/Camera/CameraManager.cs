using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();
    public static CinemachineVirtualCamera ActiveCamera = null;
    public static bool IsActiveCamera(CinemachineVirtualCamera camera)
    {
        return camera == ActiveCamera;
    }
    public static void SwichCamera(CinemachineVirtualCamera newcamera)
    {
        newcamera.Priority = 10;
        ActiveCamera = newcamera;
        foreach (CinemachineVirtualCamera cam in cameras)
        {
            if (cam != newcamera)
            {
                cam.Priority = 0;
            }
        }
    }
    public static void Register(CinemachineVirtualCamera camera)
    {
        cameras.Add(camera);
    }
    public static void UnRegister(CinemachineVirtualCamera camera)
    {
        cameras.Remove(camera);
    }
}
