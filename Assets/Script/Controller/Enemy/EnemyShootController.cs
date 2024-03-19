    using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShootController : ObjecController
{
    private Animator anim;
    public Transform check;
    public LayerMask layermask;
    public float distance = -10.48f;
    public bool CheckPlayer;
    public float Timer;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        Check();
        AttackPlayer();
        Flip();
    }
    private void Flip()
    {
        if (MegamanController.Instance != null)
        {
            if (transform.position.x < MegamanController.Instance.transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
    private void Check()
    {
        CheckPlayer = Physics2D.Raycast(check.position, Vector2.right, distance * this.gameObject.transform.localScale.x, layermask);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(check.position, new Vector3((check.position.x + distance * this.gameObject.transform.localScale.x),
                                                    check.position.y,
                                                    check.position.z));
    }
    private void AttackPlayer()
    {
        if (CheckPlayer == true)
        {
            Timer += Time.deltaTime;
            if (Timer >= 0.3f)
            {
                anim.Play("shoot");
                CreateBulletEnemy();
                Timer = 0;
            }
        }
        else
        {
            anim.Play("idle");
        }
    }
}
