using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpEnemyController : SetHpManaController
{
    public float MaxHealth;
    public float CurrentHp;
    public GameObject[] Item;
    public GameObject Death;
    void Start()
    {
        CurrentHp = MaxHealth;
        SetMaxIndex(MaxHealth);
    }

  
    void Update()
    {
        if(CurrentHp <= 0)
        {
            CurrentHp = 0;
            Die();
        }
    }
    public void TakeDamage(int damage)
    {
        CurrentHp -= damage;
        SetIndex(CurrentHp);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "bulletplayer")
        {
            TakeDamage(MegamanController.Instance.damage);
        }
    }
    private void Die()
    {       
        SmartPool.Instance.Despawn(this.gameObject);
        SmartPool.Instance.Spawn(Death, transform.position, transform.rotation);
        int x = Random.Range(0, Item.Length);
        SmartPool.Instance.Spawn(Item[x], transform.position, transform.rotation);

    }
}
