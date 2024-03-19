using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrollingController : ObjecController
{
    private Vector3 dir;
    private Animator animator;
    [Header(" patrolling")]
    public bool CheckPlayer;
    public bool run;
    private bool IsBullet;

    [Header("Phát hiện Player")]
    public Transform check;
    public LayerMask layermask;
    public float distance = -1.81f;
    private float timeCooldown;
    public float TimeStop;
    public bool CanAttack;
    private float AttackCooldown;
    public float TimeAttack;
    private void Start()
    {
        CanAttack = true;
        AttackCooldown = 0;
        CheckPlayer = false;
        run = true;
        animator = GetComponent<Animator>();
        dir = new Vector3(-1, 0, 0);
    }
    private void Update()
    {
        if (run == true && IsBullet == false && CheckPlayer == false)
        {
            Move(dir);
            this.PostEvent(EventID.OffHitbox);
        }
        else if (run == false || CheckPlayer == true || IsBullet == true)
        {
            Vector3 Direc = new Vector3(0, 0, 0);
            Move(Direc);
        }
        AnimEnemy();
        Check();
        CheckStatus();
    }
    private void Check()
    {
        if (CanAttack == true)
        {
            CheckPlayer = Physics2D.Raycast(check.position, Vector2.right, distance * this.gameObject.transform.localScale.x, layermask);
        }
        else
        {
            AttackCooldown += Time.deltaTime;
            if (AttackCooldown >= TimeAttack) // sau bao laau danh tiep
            {
                CanAttack = true;
                AttackCooldown = 0;
            }
        }
        if (CheckPlayer == true)
        {
            run = false;
        }
    }
    private void CheckStatus()
    {
        if (run == false && CheckPlayer == false) 
        {
            timeCooldown += Time.deltaTime; // thời gian di chuyển lại
            if (timeCooldown >= TimeStop)
            {
                run = true;
                IsBullet = false;
                timeCooldown = 0;
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(check.position, new Vector3((check.position.x + distance * this.gameObject.transform.localScale.x),
                                                    check.position.y,
                                                    check.position.z));
    }
    void AnimEnemy()
    {
            animator.SetBool("run", run);
            animator.SetBool("isAttack", CheckPlayer);
            animator.SetBool("CanAttack", CanAttack);      
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bulletplayer")
        {
            IsBullet = true;
            run = false;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DiemA")
        {
            this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            dir = new Vector3(1, 0, 0);
        }
        if (collision.gameObject.tag == "DiemB")
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
            dir = new Vector3(-1, 0, 0);
        }
    }
    public void AttackTime()
    {
        CanAttack = false;  
    }
}