using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwichCamera : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;
    private void Awake()
    {    
        this.RegisterListener(EventID.SwichCamera1, (sender, param) =>
        {
            CameraManager.SwichCamera(cam1);
        });
        this.RegisterListener(EventID.SwichCamera2, (sender, param) =>
        {
            CameraManager.SwichCamera(cam2);
        });
    }
}
