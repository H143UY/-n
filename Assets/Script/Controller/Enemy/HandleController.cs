using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleController : MoveController
{
    [Header("Enemy follow ")]
    protected bool isHitBullet = false;
    private float timeCooldown;
    public Animator animator;
    private Vector3 direc;
    public Transform check;
    public LayerMask layermask;
    //attack
    [Header(" Attack")]
    public Transform checkAttack;
    public float distanceAttack;
    private bool CheckAttack;
    public bool run;

    [Header(" Speed")]
    public bool SpeedEnemy;
    private bool CheckPlayer_Right;
    private bool CheckPlayer_Left;
    public float distanceRight;
    public float distanceLeft;

    private float TimeAttack;
    public bool CanAttack;
    // patrolling
    [Header(" Patrolling")]
    private float distance;
    public GameObject DiemA;
    private void Start()
    {
        CanAttack = true;
        SpeedEnemy = false;
        run = true;
        animator = GetComponent<Animator>();
        direc = new Vector3(1, 0, 0);
    }
    void Update()
    {
        SetAnim();
        Flip();
        //move
        CheckMove();
        Move(direc);
        if (CheckPlayer_Left || CheckPlayer_Right)
        {
            SpeedEnemy = true;
        }
        else
        {
            SpeedEnemy = false;
        }

        if ((CheckPlayer_Left || CheckPlayer_Right) && SpeedEnemy == true && CheckAttack == false) //thấy player và player trong tầm đánh
        {
            direc = MegamanController.Instance.transform.position - this.gameObject.transform.position;
            speed = 4;
            run = false;
        }
        else if ((!CheckPlayer_Left || !CheckPlayer_Right) && SpeedEnemy == false && CheckAttack == false && run == false) // không thấy player và run = false
        {
            direc = DiemA.transform.position - this.gameObject.transform.position;
            speed =     2;
        }
        else if (CheckAttack == true) // player trong tầm đánh thì dừng lại
        {
            direc = new Vector3(0, 0, 0);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(check.position, new Vector3((check.position.x + distanceRight * this.gameObject.transform.localScale.x),
                                                    check.position.y,
                                                    check.position.z));
        Gizmos.DrawLine(check.position, new Vector3((check.position.x + distanceLeft * this.gameObject.transform.localScale.x),
                                                    check.position.y,
                                                    check.position.z));

        Gizmos.DrawWireSphere(checkAttack.position, distanceAttack);

    }
    public void Flip()
    {
        if (direc.x != 0)
        {
            if (direc.x < 0)
            {
                this.gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (direc.x > 0)
            {
                this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bulletplayer")
        {
            isHitBullet = true;
        }
        if (collision.gameObject.tag == "DiemA")
        {
            if (SpeedEnemy == false)
            {
                this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
                direc = new Vector3(1, 0, 0);
                run = true;
            }
        }
        if (collision.gameObject.tag == "DiemB")
        {
            if (SpeedEnemy == false)
            {
                this.gameObject.transform.localScale = new Vector3(1, 1, 1);
                direc = new Vector3(-1, 0, 0);
                run = true;
            }
        }
    }
    public void SetAnim()
    {
        animator.SetFloat("walk", Mathf.Abs(direc.x));
        animator.SetBool("danh", CheckAttack);
        animator.SetBool("speed", SpeedEnemy);
        animator.SetBool("DuocPhepDanh", CanAttack);
    }
    public void AttackCooldown()
    {
        CanAttack = false;
    }

    private void CheckMove()
    {
        CheckPlayer_Right = Physics2D.Raycast(check.position, Vector2.right, distanceRight, layermask);
        CheckPlayer_Left = Physics2D.Raycast(check.position, Vector2.right, distanceLeft, layermask);
        if (CanAttack == true)
        {
            CheckAttack = Physics2D.OverlapCircle(checkAttack.position, distanceAttack, layermask);
        }
        else
        {
            TimeAttack += Time.deltaTime;
            if (TimeAttack >= 5)
            {
                CanAttack = true;
                TimeAttack = 0;
            }
        }

    }
    public void StopEnemy()
    {
        if (isHitBullet == true || CheckAttack == true)
        {
            direc = new Vector3(0, 0, 0);
            Move(direc);
        }
    }
}
