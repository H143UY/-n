using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class BulletEnemyController : BulletController
{
    private Vector3 direcBullet;
    private void Update()
    {
        Move(transform.up);
        BulletEx();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != this.gameObject.tag)
        {
            Instantiate(HieuUngNo, transform.position, transform.rotation);
            SmartPool.Instance.Despawn(this.gameObject);
        }
    }

}
