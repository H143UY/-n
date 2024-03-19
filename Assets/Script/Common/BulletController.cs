using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MoveController
{
    private float timer;
    public GameObject HieuUngNo;
    protected override void Move(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            this.gameObject.transform.position += direction;
        }
        base.Move(direction);
    }    
    protected virtual void BulletEx()
    {
        timer += Time.deltaTime;
        if (timer >= 10)
        {
            SmartPool.Instance.Despawn(this.gameObject);
            timer = 0;
            SmartPool.Instance.Spawn(HieuUngNo.gameObject, transform.position, transform.rotation);
        }
    }
   
}
