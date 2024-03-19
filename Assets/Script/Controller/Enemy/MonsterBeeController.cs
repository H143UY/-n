using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBeeController : ObjecController
{
    public float TimeBornEnemy;
    public GameObject[] EnemyRandom;
    public Transform birthplace;
    public float TimeShoot;   
    private Rigidbody2D rb;
    private Animator anim;
    //run
    private Vector3 Direc;
    public float TimeFly;
    public bool CanFly;
    //Die
    public float TimeDieCoolDown;
    public float TimeDie;
    private void Awake()
    {
       
    }
    void Start()
    {
        CanFly = false;
        TimeBornEnemy = 5;
        TimeDieCoolDown = 0;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (CanFly)
        {
            Move(Direc);
        }
        else
        {
            TimeFly += Time.deltaTime;
            if (TimeFly >= 3)
            {
                CanFly = true;
                TimeFly = 0;
            }
        }
        Die();
        ShootBee();
        SinhQuai();
    }
    public void ShootBee()
    {
        TimeShoot += Time.deltaTime;
        if (TimeShoot >= 3)
        {
            CreateBulletEnemy();
            TimeShoot = 0;
        }
    }
    private void Die()
    {
        TimeDieCoolDown += Time.deltaTime;
        if (TimeDieCoolDown >= TimeDie)
        {
            CanFly = false;
            rb.gravityScale = 7;
        }
        if (TimeDieCoolDown >= TimeDie + 3)
        {
            Destroy(this.gameObject);
            this.PostEvent(EventID.BeeDie);
            TimeDieCoolDown = 0;
        }
    }
    private void SinhQuai()
    {
        TimeBornEnemy += Time.deltaTime;
        int random = Random.Range(0, EnemyRandom.Length);
        if (TimeBornEnemy >= 6f)
        {
            TimeBornEnemy = 0;
            SmartPool.Instance.Spawn(EnemyRandom[random].gameObject, birthplace.position, birthplace.rotation);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "san")
        {
            anim.SetTrigger("die");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Diem 1")
        {
            CanFly = false;
            Direc = new Vector3(1, 0, 0);
            this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        if (collision.gameObject.tag == "Diem 2")
        {
            CanFly = false;
            Direc = new Vector3(-1, 0, 0);
            this.gameObject.transform.localScale = new Vector3(1,1,1);
        }
    }

}
