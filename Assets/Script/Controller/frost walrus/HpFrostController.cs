using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpFrostController : SetHpManaController
{
    public float MaxHealth;
    public float CurrentHp;
    void Start()
    {
        CurrentHp = MaxHealth;
        SetMaxIndex(MaxHealth);
    }

    void Update()
    {
        if(CurrentHp <= 0)
        {
            this.PostEvent(EventID.Die);
        }
    }
    public void TakeDamage(int damage)
    {
        CurrentHp -= damage;
        SetIndex(CurrentHp);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bulletplayer")
        {
            TakeDamage(100);
        }
    }
}
