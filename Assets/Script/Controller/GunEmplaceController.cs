using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEmplaceControlelr : ObjecController
{
    public float timer;
    public float timecoolDown;
    void Start()
    {
        timecoolDown = timer;
    }
    void Update()
    {
        Vector3 direction = MegamanController.Instance.transform.position - this.gameObject.transform.position;
        timecoolDown -= Time.deltaTime;
        if (timecoolDown <= 0)
        {
            timecoolDown = timer;
            SmartPool.Instance.Spawn(bullet.gameObject, transform.position, transform.rotation);
        }
    }
}
