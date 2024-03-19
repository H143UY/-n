using Model.Core.Sound;
using Plugins.Scripts.Core.Common.Sound;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class MegamanController : ObjecController
{
    private bool Dialogue = false;
    //dame player
    [Header("Sức mạnh của player")]
    public int damage;
    public static MegamanController Instance;
    public Animator anim;
    private Rigidbody2D rig;
    private float horizontal;
    public bool isGround;
    private float Vertical;
    public float JumpForce;
    [Header("Wall slide")]
    //wall slide,jump
    private CapsuleCollider2D capsul;

    private bool canWallSlide;
    [Header("Shoot")]
    //Shoot
    private float TimeShoot;
    private bool Ishooting;
    private bool JumpShoot = false;
    private SetManaController setmana;

    [Header("Hit")]
    // hit
    private bool HitDame = false;

    [Header("CheckGround")]
    //ground 
    public float GroundCheck;

    [Header("Attack")]
    //ATTACK
    private bool isAttack;
    private bool DuocPhepDanh;
    [SerializeField] private Transform attackpoin;
    public float attackrange;
    public LayerMask enemylayer;

    // Wall Slide
    [Header("WAlL Slide")]
    public Transform WallCheck;
    public LayerMask WallLayer;
    public float WallDistance;
    public bool IsTochingWall;
    public bool isWallSlide;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        this.RegisterListener(EventID.GetHit, (sender, param) =>
        {
            DuocPhepDanh = false;
            HitDame = true;
        });
        this.RegisterListener(EventID.Isdialogue, (sender, param) =>
        {
            horizontal = 0;
            Dialogue = true;
        });
        this.RegisterListener(EventID.Enddialogue, (sender, param) =>
        {
            Dialogue = false;
        });
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        setmana = GetComponent<SetManaController>();
        capsul = GetComponent<CapsuleCollider2D>();
    }
    void Update()
    {
        Vector3 direction = new Vector3(horizontal, 0, 0);
        if (Ishooting == false && HitDame == false && !Dialogue)
        {
            horizontal = Input.GetAxis("Horizontal");
            Move(direction);
        }

        if (Input.GetKeyDown(KeyCode.W) && isGround == true && isAttack == false && HitDame == false)
        {
            if (Dialogue == false)
            {
                Jump();
            }
        }
        Flip();
        Anim();
        PlayerShoot();
        AttackPlayer();
        SetAnim();
        CheckWall();
        WallSlide();
        if (HitDame == true)
        {
            horizontal = 0;
            Ishooting = false;
            JumpShoot = false;
            DuocPhepDanh = false;
        }
        Checkground();
        DieuKienNho();
    }
    public void Jump()
    {
        DuocPhepDanh = false;
        rig.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        anim.SetTrigger("jump");
        isGround = false;
    }
    public void Flip()
    {
        if (horizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    public void Anim()
    {
        if (isGround == true && !isAttack && !Ishooting)
        {
            if (horizontal != 0)
            {
                DuocPhepDanh = false;
            }
            else if (horizontal == 0)
            {
                DuocPhepDanh = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "san")
        {
            JumpShoot = false;
            isWallSlide = false;
            isAttack = false;
            this.PostEvent(EventID.NoFindPlayer);
        }
        if (collision.gameObject.tag == "Boss")
        {
            if(isGround == true)
            {
                rig.AddForce(Vector2.up * 30, ForceMode2D.Impulse);
            }        
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bos")
        {
            rig.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
        }
    }
    //attack
    public void AttackPlayer()
    {
        if (Input.GetKeyDown(KeyCode.J) && isGround && DuocPhepDanh == true)
        {
            DuocPhepDanh = false;
            if (isAttack == false)
            {
                isAttack = true;
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoin.position, attackrange, enemylayer);
                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<HpEnemyController>().TakeDamage(70);
                    this.PostEvent(EventID.HoiManaVaMau);
                }
            }
        }
    }
    public void AttackComplete()
    {
        isAttack = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackpoin.position, attackrange);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(WallCheck.position, new Vector3((WallCheck.position.x + WallDistance * this.gameObject.transform.localScale.x),
                                                  WallCheck.position.y,
                                                  WallCheck.position.z));

    }
    //Ban   
    public void PlayerShoot()
    {
        TimeShoot += Time.deltaTime;
        if (TimeShoot >= 0)
        {
            if (setmana.CurrentMana > 30)
            {
                if (Input.GetKeyDown(KeyCode.U) && DuocPhepDanh == true && horizontal == 0 && HitDame == false)
                {
                    if (isGround == true)
                    {
                        Ishooting = true;
                    }
                    else
                    {
                        JumpShoot = true;
                    }
                    SoundManager.Instance.PlaySound(SoundType.bluefireshooting);
                    CreateBulletPlayer();
                    this.PostEvent(EventID.TruMana);
                    TimeShoot = 0;
                }
            }
            else
            {
                Debug.Log("khong du mana de ban");
            }
        }
    }
    //OnWall
    private void Checkground()
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
    //wallSlide
    private void CheckWall()
    {
        if(HitDame == false)
        {
            IsTochingWall = Physics2D.Raycast(WallCheck.position, Vector2.right, WallDistance * this.gameObject.transform.localScale.x, WallLayer);
        }     
    }
    public void WallSlide()
    {
        if (IsTochingWall == true)
        {
            isWallSlide = true;
            isGround = false;
            DuocPhepDanh = false;
            rig.gravityScale = 0;
            rig.velocity = Vector2.zero;
            if (Input.GetKey(KeyCode.S))
            {
                rig.gravityScale = 60;
                rig.velocity = new Vector2(rig.velocity.x, rig.velocity.y * 50f);
            }
            else if (horizontal != 0 )
            {
                IsTochingWall = false;
            }
            else if(HitDame == true)
            {
                IsTochingWall = false;
                rig.AddForce(new Vector2(-transform.localScale.x * 5, 2)  , ForceMode2D.Impulse);
                this.transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y);
            }
        }
        else 
        {
            DuocPhepDanh = true;
            rig.gravityScale = 3;
            isWallSlide = false;
        }
    }
    public void SetAnim()
    {
            anim.SetFloat("speed", Mathf.Abs(horizontal));
            anim.SetBool("isground", isGround);
            anim.SetBool("Shoot", Ishooting);
            anim.SetBool("jumShoot", JumpShoot);
           anim.SetBool("Chem", isAttack);                 
           anim.SetBool("isWallSlide", isWallSlide);
        if(HitDame == true)
        {
            anim.SetTrigger("hit 0");
        }        
    }
    public void CompleteShoot()
    {
        Ishooting = false;
        JumpShoot = false;
    }
    public void CompleteHit()
    {
        HitDame = false;
        anim.ResetTrigger("hit 0");
    }
    public void DieuKienNho()
    {
        if (isGround == true)
        {
            JumpShoot = false;
        }
        if (isGround == false)
        {
            Ishooting = false;
        }
    }
}

