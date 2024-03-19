using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBallController : MonoBehaviour
{ 
    [Header("Chuyen Trang Thai")]
    public bool Boom = false;
    private Animator animator;
    public float TimeBorn;
    private int CountDie;
    [Header("Instiate ThornyBall")]
    public GameObject CauGai;
    public Transform Transhoot1;
    public Transform Transhoot2;
    public Transform Transhoot3;
    public Transform Transhoot4;
  
    void Start()
    {
        TimeBorn = 0.6f;
        animator = GetComponent<Animator>();
    }
    private void Awake()
    {
        this.RegisterListener(EventID.punch, (sender, param) =>
        {
            Boom = true;
        });
        this.RegisterListener(EventID.quaybenphai, (sender, param) =>
        {
            transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, 1);
        });
        this.RegisterListener(EventID.quaybentrai, (sender, param) =>
        {
            transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, 1);
        });
    }
    void Update()
    {
        animator.SetBool("boom", Boom);
        InstatiaBall();
    }
    private void InstatiaBall()
    {
        if (Boom == true)
        {
            TimeBorn += Time.deltaTime;
            if (TimeBorn >= 0.8f)
            {
                SmartPool.Instance.Spawn(CauGai, Transhoot1.position, Transhoot1.rotation);
                SmartPool.Instance.Spawn(CauGai, Transhoot2.position, Transhoot2.rotation);
                SmartPool.Instance.Spawn(CauGai, Transhoot3.position, Transhoot3.rotation);
                SmartPool.Instance.Spawn(CauGai, Transhoot4.position, Transhoot4.rotation);
                CountDie++;
                TimeBorn = 0;
            }
            if (CountDie == 2)
            {
                Boom = false;
                SmartPool.Instance.Despawn(this.gameObject);
                this.PostEvent(EventID.CompleteIceBall);
            }
        }
    }
    private void ChuyenGiao()
    {
        this.PostEvent(EventID.ChuyenGiaoTrangThai);
    }
}
