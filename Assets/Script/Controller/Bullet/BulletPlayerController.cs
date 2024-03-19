using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayerController : BulletController
{
    private Vector3 directionBullet;   
    private void Update()
    {
        this.gameObject.transform.position += directionBullet * speed * Time.deltaTime;
        BulletEx();
    }
    public void RotateBullet()
    {
        if (MegamanController.Instance.transform.localScale.x > 0)
        {
            this.gameObject.transform.Rotate(0, 0, 0);
            directionBullet = new Vector3(1, 0, 0);

        }
        else if (MegamanController.Instance.transform.localScale.x < 0)
        {
            this.gameObject.transform.Rotate(0, 0, 180);
            directionBullet = new Vector3(-1, 0, 0);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Destroy(this.gameObject);
            SmartPool.Instance.Spawn(HieuUngNo.gameObject, transform.position, transform.rotation);
        }
    }
}
