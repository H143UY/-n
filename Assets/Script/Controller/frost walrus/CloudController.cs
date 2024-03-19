using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MoveController
{

    void Start()
    {
        
    }
    private void Update()
    {
        Move(transform.up);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "san")
        {
            SmartPool.Instance.Despawn(this.gameObject);
        }
    }
}
