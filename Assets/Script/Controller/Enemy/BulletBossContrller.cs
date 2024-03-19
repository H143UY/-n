using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBossContrller : BulletController
{

    void Start()
    {
        
    }
    void Update()
    {
        Move(transform.up);
        BulletEx();   
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != this.gameObject.tag)
        {
            Destroy(this.gameObject);
        }
    }
}
