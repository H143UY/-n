using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFlyController : ObjecController
{
    private Vector3 Direction;
    private float TimeToDie;
    void Start()
    {
        Direction = MegamanController.Instance.transform.position - this.gameObject.transform.position;
    }
    void Update()
    {
        Move(Direction);
        TimeToDie += Time.deltaTime;
        if(TimeToDie > 13 )
        {
            SmartPool.Instance.Despawn(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
        if(collision.gameObject.tag != this.gameObject.tag)
        {
            Destroy(this.gameObject);
        }
    }
}
