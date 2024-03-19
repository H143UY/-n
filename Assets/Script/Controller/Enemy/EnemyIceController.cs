using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyIceController : ObjecController
{
    public bool ChuyenTrangThai;
    public bool RoiBang;
    public float TimeChuyenTrangThai;
    private Animator anim;
    public float TimeCreateCoolDown;
    void Start()
    {
        anim = GetComponent<Animator>();
        ChuyenTrangThai = false;
        RoiBang = false;
    }
    void Update()
    {
        CreateIce();
        SetAnim();
        Change();

    }
    public void SetAnim()
    {
        anim.SetBool("ChuyenTrangThai", ChuyenTrangThai);
        anim.SetBool("RoiBang", RoiBang);
    }

    public void CreateIce()
    {
        if (RoiBang == true)
        {
            TimeCreateCoolDown += Time.deltaTime;
            if (TimeCreateCoolDown >= 2)
            {
                CreateBulletEnemy();
                RoiBang = false;
                ChuyenTrangThai = false;
                TimeCreateCoolDown = 0;
            }
        }

    }
    public void Change()
    {
        if (ChuyenTrangThai == false)
        {
            TimeChuyenTrangThai += Time.deltaTime;
        }
        if (TimeChuyenTrangThai >= 5)
        {
            ChuyenTrangThai = true;
            RoiBang = true;
            TimeChuyenTrangThai = 0;
        }

    }
}
