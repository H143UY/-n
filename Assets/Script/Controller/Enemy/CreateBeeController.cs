using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBeeController : MonoBehaviour
{
    public GameObject BeeBoss;
    public Transform NoiSinh;
    private void Awake()
    {
        this.RegisterListener(EventID.LazeDestroy, (sender, pamram) =>
        {
            SmartPool.Instance.Spawn(BeeBoss, NoiSinh.position,NoiSinh.rotation);
        });
    }
}
