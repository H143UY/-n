using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSungController : BulletController
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BulletEx();
        this.transform.position += -transform.up* Time.deltaTime * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != this.gameObject.tag)
        {
            SmartPool.Instance.Despawn(this.gameObject);
        }
    }
}
