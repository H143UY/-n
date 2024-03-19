using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class FollowPlayerController : ObjecController
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
    private bool stop = false;
    private void Start()
    {
        CanAttack = true;
        SpeedEnemy = false;
        run = true;
        animator = GetComponent<Animator>(); // khi khởi động game thì sẽ gán luộn vào trong inspector
        direc = new Vector3(1, 0, 0); // hướng đi 
    }
    void Update() // là nó cập nhập liên tục khi chạy game , 1 frame sẽ là lặp 60 lần
    {
        if(stop == true)
        {
            direc = new Vector3(0, 0, 0);
        }
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

        if ( SpeedEnemy == true && CheckAttack == false) //thấy player và player trong tầm đánh
        {
            if (CheckPlayer_Left == true)
            {
                direc = new Vector3(-1, 0, 0);
            }
            else
            {
                direc = new Vector3(1, 0, 0);
            }
            speed = 13;
            run = false; // tắt trạng thái đi bộ, để chuyển sang trạng thái chạy nhanh
        }
        else if ((!CheckPlayer_Left || !CheckPlayer_Right)  && CheckAttack == false && run == false) // không thấy player và run = false
        {
            run = true;
            float kc = this.gameObject.transform.position.x - DiemA.transform.position.x; // tình khoảng cách về điểm A
            if (kc > 0)
            {
                direc = new Vector3(-1, 0, 0); 
            }
            else
            {
                direc = new Vector3(1, 0, 0);
            }
            speed = 5;
        }
        else if (CheckAttack == true || isHitBullet) // player trong tầm đánh thì dừng lại
        {
            stop = true; 
        }
    }
    // rayscast là phóng tia ảo không hiện trên màn hình game để xác định vật va chạm ( là 1 cái unity hỗ trợ sẵn)

    private void OnDrawGizmos()  // nó vẽ cái tia do rayscast
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
        if (direc.x != 0) // khi direce ! 0, khi di chuyển
        {
            if (direc.x < 0)
            {
                this.gameObject.transform.localScale = new Vector3(1, 1, 1); // hướng quay mặt
            }
            else if (direc.x > 0)
            {
                this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision) // phát hiện va chạm với 1 vật thể cũng gắn boxx
    {
        if (collision.gameObject.tag == "bulletplayer")
        {
            isHitBullet = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Diem 1") // collision là vật mà mình va chạm , khi va chạm vào điểm sẽ quay mặt và đổi hướng
        {
            if (SpeedEnemy == false) // vì khi đuổi theo player thì speed = true 
            {
                this.gameObject.transform.localScale = new Vector3(-1, 1, 1); 
                direc = new Vector3(1, 0, 0);
                run = true;
            }
        }
        if (collision.gameObject.tag == "Diem 2")
        {
            if (SpeedEnemy == false)
            {
                this.gameObject.transform.localScale = new Vector3(1, 1, 1);
                direc = new Vector3(-1, 0, 0); //hướng quay của nhân vật
                run = true;
            }
        }
    }
    public void SetAnim()
    {
        animator.SetFloat("run", Mathf.Abs(direc.x));
        animator.SetBool("attack", CheckAttack);
        animator.SetBool("speed", SpeedEnemy);
        animator.SetBool("DuocPhepDanh", CanAttack);
    }
    public void AttackCooldown()
    {
        CanAttack = false;
        CheckAttack = false;
        CheckPlayer_Right = false;
        CheckPlayer_Left = false;
        direc = new Vector3(0,0,0);
    }

    private void CheckMove()
    {
        if (CanAttack == true)
        {
            CheckPlayer_Right = Physics2D.Raycast(check.position, Vector2.right, distanceRight, layermask);  //vị trí check,hướng check,khoảng cách check, 
            CheckPlayer_Left = Physics2D.Raycast(check.position, Vector2.right, distanceLeft, layermask);
        }
        if (CanAttack == true)
        {
            CheckAttack = Physics2D.OverlapCircle(checkAttack.position, distanceAttack, layermask); // check theo kiểu hình tròn
        }
        else // khi đánh xong rồi sẽ nhảy vào chỗ dưới
        {
            TimeAttack += Time.deltaTime; //thời gian 1 frame cộng liên tục
            if (TimeAttack >= 3)
            {
                CanAttack = true;
                TimeAttack = 0; // tránh tình trạng nó cộng liên tục thì sau khi đánh lần 2 sẽ k nhảy vào đây được nữa
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
