using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : ObjecController
{
    public bool Action;
    public GameObject StartGame;
    private bool IsDialogue = false;
    public GameObject HitBoxRun;
    private int level;
    public GameObject HitBox;
    private Animator Anim;
    //shoot
    [Header(" Shoot")]
    private float TimeShoot;
    public int Count;
    public bool Shoot;
    //run
    [Header(" Run")]
    private Vector3 Dir;
    public bool RunToRight;
    public bool RunToLeft;
    public float TimeToRun;

    // attack player
    [Header(" Attack")]
    public bool FindPlayer;
    public Transform CheckAttack;
    public float DistanceAttack;
    public LayerMask layerplayer;
    private bool isAttack;

    // ground
    [Header(" Ground")]
    public bool isGround = true;
    public float GroundCheck;

    //jump
    [Header(" Jump")]
    private float TimeJump;
    private Rigidbody2D rg;
    public bool DuocPhepNhay;

    //ban len troi
    [Header("Shoot Sky")]
    public GameObject BulletSky;
    public Transform transhootUp;
    public bool ShootSky;
    public GameObject PosJump;
    // die
    [Header("Die")]
    public bool Loser = false;
    public float TimeDestroy;
    public GameObject KeyNextScene;


    private void Awake()
    {
        this.RegisterListener(EventID.BeeDie, (sender, pamram) =>
        {
            rg.gravityScale = 5;
            level += 1;
        });
        this.RegisterListener(EventID.ShootSky, (sender, param) =>    //khi máu nhỏ hơn 70 %
        {
            if (RunToLeft)
            {
                float distance = this.gameObject.transform.position.y - PosJump.transform.position.y;
                if (distance < 30)
                {
                    DuocPhepNhay = true;
                }
            }
            float DisToJump = PosJump.transform.position.x - this.gameObject.transform.position.x;
            if (DisToJump <= 2)
            {
                if (isGround)
                {
                    ShootSky = true;
                }
            }
            else
            {
                Debug.Log("di chuyen ve a");
                RunToRight = true;
                RunToLeft = false;
            }
        });
        this.RegisterListener(EventID.EnemyDestroy, (sender, param) =>
        {
            Loser = true;
        });
        this.RegisterListener(EventID.Enddialogue, (sender, param) =>
        {
            DoneDialogue();
            StartGame.SetActive(true);
        });
    }

    void Start()
    {
        Action = false;
        RunToRight = true;
        RunToLeft = true;
        ShootSky = false;
        DuocPhepNhay = false;
        Shoot = false;
        Anim = GetComponent<Animator>();
        rg = GetComponent<Rigidbody2D>();
        HitBoxRun.SetActive(false);
    }

    void Update()
    {
        Dialoguee();
        if (Action == true && Loser == false)
        {
            if (Mathf.Abs(Dir.x) > 0 && level == 1)
            {
                HitBoxRun.SetActive(true);
            }
            else
            {
                HitBoxRun.SetActive(false);
            }
            CacDieuKienNho();
            Run();
            ShootGun();
            AttackPlayer();
            Jump();
        }
        SetAnim();
        CheckGround();
        Flip();
        NextLevel();
    }
    public void Flip()
    {
        if (RunToRight && RunToLeft == false)
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (RunToLeft == true && RunToRight == false)
        {
            this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bulletplayer")
        {
            if (DuocPhepNhay == false && ShootSky == false && Dir.x == 0) // khong o trang thai chuyen lv va dung im thi moi nhan + count
            {
                Count++;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "start pos" && RunToLeft == true)
        {
           
            TimeToRun = 0;
            RunToRight = true;
            RunToLeft = false;
            HitBox.SetActive(false);
        }
        if (collision.gameObject.tag == "end pos" && RunToRight == true)
        {
            this.PostEvent(EventID.SwichCamera1);
            TimeToRun = 0;
            RunToLeft = true;
            RunToRight = false;
            HitBox.SetActive(false);
        }
        if (collision.gameObject.tag == "NPC")
        {
            IsDialogue = true;
        }
    }
    private void SetAnim()
    {
        if (Loser == false)
        {
            Anim.SetBool("shoot", Shoot);
            Anim.SetBool("isground", isGround);
            Anim.SetBool("ShootSky", ShootSky);
            if (isGround == false)
            {
                Anim.SetTrigger("jump");
            }
            if (level == 0)
            {
                Anim.SetFloat("run", Mathf.Abs(Dir.x));
            }
            else
            {
                Anim.SetFloat("run lvl1", Mathf.Abs(Dir.x));
            }
            Anim.SetBool("IsAttack", isAttack);
            if (isAttack == true)
            {
                Anim.SetTrigger("attack");
            }
        }
        else
        {
            Anim.SetTrigger("Loss");
        }

    }
    private void ShootGun()
    {
        if (Shoot == true)
        {
            TimeShoot += Time.deltaTime;
            if (TimeShoot > 1.5f)
            {
                CreateBulletEnemy();
                TimeShoot = 0;
            }
        }
    }
    private void Run()
    {
        if (RunToRight == true && RunToLeft == false)
        {
            if (TimeToRun >= 5.5f)
            {
                Shoot = false;
                Dir = new Vector3(1, 0, 0);
            }
        }
        if (RunToLeft == true && RunToRight == false)
        {
            if (TimeToRun >= 5.5f)
            {
                {
                    Shoot = false;
                    Dir = new Vector3(-1, 0, 0);
                }
            }
        }
        if (TimeToRun < 5.5f)
        {
            Dir = new Vector3(0, 0, 0);
        }
    }
    private void AttackPlayer()
    {
        if (level == 0)
        {
            FindPlayer = Physics2D.Raycast(CheckAttack.position, Vector2.right, DistanceAttack * this.gameObject.transform.localScale.x, layerplayer);
        }

        if (FindPlayer == true)
        {
            isAttack = true;
            speed = 0;
        }
        else
        {
            isAttack = false;
            speed = 20;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(CheckAttack.position, new Vector3((CheckAttack.position.x + DistanceAttack * this.gameObject.transform.localScale.x),
                                                  CheckAttack.position.y,
                                                  CheckAttack.position.z));
    }
    public void CheckGround()
    {

        RaycastHit2D[] hit = Physics2D.RaycastAll(this.gameObject.transform.position, -Vector2.up, GroundCheck);
        foreach (var item in hit)
        {
            if (item.transform.tag == "san")
            {
                isGround = true;
            }
            else
            {
                isGround = false;
            }
        }
    }
    private void Jump()
    {
        if (DuocPhepNhay == true)
        {
            TimeJump += Time.deltaTime;
            if (TimeJump >= 3)
            {
                ShootSky = false;
                rg.AddForce(new Vector2(0, 1) * 5f, ForceMode2D.Impulse);
                Anim.SetTrigger("jump");
            }
            if (TimeJump >= 3.9)
            {
                rg.velocity = new Vector2(0, 0);
                rg.gravityScale = 0;
                DuocPhepNhay = false;
                TimeToRun = 0;
                TimeJump = 0;
            }
        }
    }
    private void ShootToSky()
    {
        SmartPool.Instance.Spawn(BulletSky, transhootUp.position, transhootUp.rotation);
    }
    private void CacDieuKienNho()
    {
        if (ShootSky == true)
        {
            Shoot = false;
        }
        if (DuocPhepNhay == false && isGround == true) // khi mau -70% thì sẽ k chạy tiếp mà khi đó sẽ nhảy
        {
            TimeToRun += Time.deltaTime;
        }
        if (FindPlayer == false)
        {
            Move(Dir);
        }
        if (level == 1)
        {
            this.PostEvent(EventID.GiaiDoan2);
        }
        if (Dir.x > 0)
        {
            Count = 0;
            TimeShoot = 0;
            Shoot = false;
        }
        if (Dir.x == 0 && ShootSky == false && DuocPhepNhay == false && Count >= 1)
        {
            Shoot = true;
        }
        if (isGround == false)
        {
            Shoot = false;
        }
    }
    private void NextLevel()
    {
        if (Loser == true)
        {
            TimeDestroy += Time.deltaTime;
            if (TimeDestroy >= 4)
            {
                Destroy(this.gameObject);
                SmartPool.Instance.Spawn(KeyNextScene, gameObject.transform.position, transform.rotation);
            }
        }
    }
    private void Dialoguee()
    {
        if (Action == false)
        {
            float khoangcach = this.gameObject.transform.position.x - MegamanController.Instance.transform.position.x;
            if (khoangcach <= 84)
            {
                this.PostEvent(EventID.Isdialogue);
                this.PostEvent(EventID.SwichCamera2);
                if (IsDialogue == false)
                {
                    TimeToRun += Time.deltaTime;
                    if (TimeToRun >= 2.5f)
                    {
                        Dir = new Vector3(-1, 0, 0);
                        Move(Dir);
                    }
                }
                else
                {
                    Dir = new Vector3(0, 0, 0);
                }
            }
        }
    }
    private void DoneDialogue()
    {
        Action = true;
        IsDialogue = false;
        RunToRight = true;
        RunToLeft = false;
        TimeToRun = 6f;
    }
}
