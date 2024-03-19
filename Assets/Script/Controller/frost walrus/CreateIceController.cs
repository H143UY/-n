using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateIceController : MonoBehaviour
{
    public GameObject BulletIce;
    public Transform ViTri1;
    public Transform ViTri2;
    public Transform ViTri3;
    public Transform ViTri4;
    public Transform ViTri5;
    private bool Roi = false;
    private void Awake()
    {
        this.RegisterListener(EventID.bang_roi, (sender, param) =>
        {
            Roi = true;
        });
    }

    // Update is called once per frame
    void Update()
    {
        if(Roi)
        {
            SmartPool.Instance.Spawn(BulletIce, ViTri1.position, ViTri1.rotation);
            SmartPool.Instance.Spawn(BulletIce, ViTri2.position, ViTri2.rotation);
            SmartPool.Instance.Spawn(BulletIce, ViTri3.position, ViTri3.rotation);
            SmartPool.Instance.Spawn(BulletIce, ViTri4.position, ViTri4.rotation);
            SmartPool.Instance.Spawn(BulletIce, ViTri5.position, ViTri5.rotation);
            Roi = false;
        }
    }
}
