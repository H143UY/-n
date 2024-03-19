using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class ObjecController : MoveController
{
    public GameObject bullet;
    public GameObject transhoot;
    protected override void Move(Vector3 direction)
    {
        base.Move(direction);
    }
    protected void CreateBulletPlayer()
    {
        var bulletPlayer = SmartPool.Instance.Spawn(bullet.gameObject, transhoot.transform.position, transhoot.transform.rotation);
        bulletPlayer.GetComponent<BulletPlayerController>().RotateBullet();
    }
    protected void CreateBulletEnemy()
    {
        SmartPool.Instance.Spawn(bullet.gameObject, transhoot.transform.position, transhoot.transform.rotation);
    }
}
