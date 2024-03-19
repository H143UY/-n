using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBossController : SetHpManaController
{
    [Header("Máu và giáp")]
    public float MaxHealth;
    public float CurrentHp;
    public float GiapBoss;
    private bool immortal;

    private bool GiaiDoan2Boss;
    private bool CongGiap;


    private void Awake()
    {
        this.RegisterListener(EventID.GiaiDoan2, (sender, param) =>
        {
            GiaiDoan2Boss = true; // không cho nhảy vào việc sẽ bắn lên trời gọi boss sau khi up level
        });
    }
    private void Start()
    {
        CongGiap = false;
        immortal = false;
        CurrentHp = MaxHealth;
        SetMaxIndex(MaxHealth);
        GiaiDoan2Boss = false;
    }
    private void Update()
    {
        if (GiaiDoan2Boss == true && CongGiap == false)
        {
            CongGiap = true;
            GiapBoss += 50;
            immortal = false;
        }
        if (CurrentHp <= 0)
        {
            CurrentHp = 0;
            this.PostEvent(EventID.EnemyDestroy);
        }
        transfer();
    }
    public void TakeDamage(int damage)
    {
        CurrentHp -= (damage - GiapBoss);
        SetIndex(CurrentHp);
    }
    private void transfer()
    {
        if (CurrentHp <= (0.5 * MaxHealth) && GiaiDoan2Boss == false)
        {
            this.PostEvent(EventID.ShootSky);
            immortal = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bulletplayer")
        {
            if (immortal == false)
            {
                TakeDamage(MegamanController.Instance.damage);
            }
        }
    }
}
