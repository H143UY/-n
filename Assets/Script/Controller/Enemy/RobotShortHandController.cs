using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RobotShortHandController : MoveController
{
    //jump
    public bool CheckJump = false;
    public Transform checkjump;
    public float JumpRange;
    private bool Jump = false;
    //attack
    private bool CheckPlayer = false;
    public Transform check;
    public float distance;
    public LayerMask layermask;
    public Rigidbody2D rig;
    private float khoangcach;
    private Animator animator;
    private Vector3 dir;
    public float JumpForc;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        khoangcach = MegamanController.Instance.transform.position.y - this.gameObject.transform.position.y;
        Nhay();
        CanJump();
        AnimRobot();
        Danh();
    }
    private void Danh()
    {
        CheckPlayer = Physics2D.Raycast(check.position, Vector2.right, distance * this.gameObject.transform.localScale.x, layermask);

    }
    private void Nhay()
    {
        if (Jump)
        {
            dir = new Vector3(0, khoangcach, 0);
            Move(dir);
            if (CheckPlayer)
            {
                animator.Play("attack");
            }
            Jump = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "san")
        {
            CheckPlayer = false;
            Jump = false;
            animator.Play("idle");
            this.PostEvent(EventID.OffHitbox);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkjump.position, JumpRange);
        Gizmos.DrawLine(check.position, new Vector3((check.position.x + distance * this.gameObject.transform.localScale.x),
                                                    check.position.y,
                                                    check.position.z));
    }
    private void CanJump()
    {
        if(Jump == false)
        {
            CheckJump = Physics2D.OverlapCircle(checkjump.position, JumpRange, layermask);
        }
        if (CheckJump)
        {
            Jump = true;
        }
        else
        {
            Jump = false;
        }
    }
    private void AnimRobot()
    {
        if (Jump && CheckPlayer == false)
        {
            animator.Play("jump");
        }

    }
}
