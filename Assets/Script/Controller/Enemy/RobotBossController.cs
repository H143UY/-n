    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBossController : ObjecController
{
    private bool CanShoot = false;
    public float TimeToShoot;
    public float Shoot;
    public bool Shooting;
    private Animator an;
    void Start()
    {
        Shooting = false;
        Shoot = 100;
        an = GetComponent<Animator>();
    }
    void Update()
    {
        if (CanShoot)
        {
            Shoot += Time.deltaTime;
            if (Shoot >= TimeToShoot)
            {
                Shooting = true;
                Shoot = 0;
            }
        }
        an.SetBool("shoot", Shooting);
        Flip();
    }
    public void SmartBullet()
    {
        CreateBulletEnemy();
    }
    public void OffShoot()
    {
        Shooting = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "robot laze")
        {
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag == "san")
        {
            CanShoot = true;
        }
    }
    private void Flip()
    {
        if (MegamanController.Instance != null)
        {
            if (transform.position.x < MegamanController.Instance.transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
