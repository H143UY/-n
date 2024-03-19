using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornyBallController : MoveController
{
    public bool Vo_Vun = false;
    private Animator Animator;
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(transform.up);
        if (Vo_Vun)
        {
            Animator.SetTrigger("vo vun");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != this.gameObject.tag)
        {
            Vo_Vun = true;
        }
    }
    private void DestroyBall()
    {
        SmartPool.Instance.Despawn(this.gameObject);
    }
}
